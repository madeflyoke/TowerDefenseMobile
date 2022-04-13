using UnityEngine;
using Cinemachine;
using Zenject;
using TD.GUI;
using Cysharp.Threading.Tasks;
using TD.GamePlay.Managers;
using System.Threading;

namespace TD.Cameras
{
    public class CameraController : MonoBehaviour
    {
        [Inject] private GUIController guiController;
        [Inject] private GameManager gameManager;

        [SerializeField] private Transform homeBaseDestroyPivot;
        [SerializeField] private float panSpeed;
        [SerializeField] private float maxZoomHeight;
        [SerializeField] private float minZoomHeight;
        [Range(0f, 1f)]
        [SerializeField] private float startTilting;
        [SerializeField] private float maxZoomTilt;
        [SerializeField] private float minZoomTilt;
        [SerializeField] private float zoomSensitivity;
        [SerializeField] private BoxCollider cameraViewCollider;
        [SerializeField] private BoxCollider cameraBoundsCollider;
        private PlayerInputs inputs;
        private CinemachineVirtualCamera cam;
        private Vector3 centerPosition;
        private CinemachineCameraOffset camOffset;
        private CinemachineRecomposer camRecomposer;
        private Vector3 standardViewColliderSize;
        private bool isLock;
        private CancellationTokenSource cancellationToken;

        private void Awake()
        {
            cam = GetComponent<CinemachineVirtualCamera>();
            camOffset = GetComponent<CinemachineCameraOffset>();
            camRecomposer = GetComponent<CinemachineRecomposer>();
            inputs = new PlayerInputs();
            centerPosition = transform.position;
            standardViewColliderSize = cameraViewCollider.size;
            cancellationToken = new CancellationTokenSource();
        }

        private void OnEnable()
        {
            inputs.Enable();
            gameManager.endGameEvent += HomeBaseDestroyCamera;
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
            inputs.General.Camera.started += ctx => MoveCameraWin();
            inputs.General.CameraZoom.started += ctx => ZoomWin();
#else
            inputs.TouchInput.CameraZoomStart.started += ctx => CalculateZoomTouch();
            inputs.TouchInput.CameraMovementStart.started += ctx => MoveCamera();
#endif
        }
        private void OnDisable()
        {
            inputs.Disable();
            gameManager.endGameEvent -= HomeBaseDestroyCamera;
            cancellationToken.Cancel();
        }

        private async void MoveCamera()
        {
            if (isLock)
            {
                return;
            }
            Vector2 startPos = inputs.TouchInput.CameraFirstTouchPosition.ReadValue<Vector2>();
            while (Vector2.Distance(inputs.TouchInput.CameraFirstTouchPosition.ReadValue<Vector2>(), startPos) <= 100f)
            {
                if (isLock || inputs.TouchInput.CameraMovementStart.phase == UnityEngine.InputSystem.InputActionPhase.Waiting)
                {
                    return;    //lock the camera before small (50f) distance will not be pass through
                }
                await UniTask.Yield(cancellationToken: cancellationToken.Token);
            }
            guiController.GamePlayScreen.BuildMenuController.HideMenu();
            while (inputs.TouchInput.CameraMovementStart.phase != UnityEngine.InputSystem.InputActionPhase.Waiting)
            {
                if (isLock)
                {
                    return;
                }
                Vector2 inputValue = -inputs.TouchInput.CameraMovement.ReadValue<Vector2>() / panSpeed;
                Vector2 direction = new Vector2(
                    Mathf.Clamp(inputValue.x, -5f, 5f),
                    Mathf.Clamp(inputValue.y, -5f, 5f));  //clamp touch deltas  
                if (direction.x < 0 && cameraViewCollider.bounds.min.x <= cameraBoundsCollider.bounds.min.x ||
            direction.x > 0 && cameraViewCollider.bounds.max.x >= cameraBoundsCollider.bounds.max.x)
                {
                    direction.x = 0;
                }
                if (direction.y < 0 && cameraViewCollider.bounds.min.z <= cameraBoundsCollider.bounds.min.z ||
                    direction.y > 0 && cameraViewCollider.bounds.max.z >= cameraBoundsCollider.bounds.max.z)
                {
                    direction.y = 0;
                }

                Vector3 newPos = cam.transform.position + new Vector3(direction.x, 0f, direction.y);
                cam.transform.position = Vector3.Slerp(cam.transform.position, newPos, Time.deltaTime * panSpeed);
                CheckCameraBounds();
                await UniTask.Yield(cancellationToken: cancellationToken.Token);
            }
            CheckCameraBounds(); //check before and after camera moves 
        }

        private async void CheckCameraBounds()
        {
            if (cameraViewCollider.bounds.min.x < cameraBoundsCollider.bounds.min.x - 5f ||
                cameraViewCollider.bounds.max.x > cameraBoundsCollider.bounds.max.x + 5f ||
                cameraViewCollider.bounds.min.z < cameraBoundsCollider.bounds.min.z - 5f ||
                    cameraViewCollider.bounds.max.z > cameraBoundsCollider.bounds.max.z + 5f)
            {
                isLock = true;
                while (Vector2.Distance(cam.transform.position, centerPosition) >= 2f) //return camera to center
                {
                    cam.transform.position = Vector3.Lerp(transform.position, centerPosition, zoomSensitivity / 10 * Time.deltaTime);
                    await UniTask.Yield(cancellationToken: cancellationToken.Token);
                }
                isLock = false;
                return;
            }
        }

        private async void CalculateZoomTouch()
        {
            float distance = 0f;
            isLock = true;
            while (inputs.TouchInput.CameraZoomStart.phase != UnityEngine.InputSystem.InputActionPhase.Waiting &&
                inputs.TouchInput.CameraMovementStart.phase != UnityEngine.InputSystem.InputActionPhase.Waiting)
            {
                float currentDistance = Vector2.Distance(inputs.TouchInput.CameraFirstTouchPosition.ReadValue<Vector2>(),
                    inputs.TouchInput.CameraSecondTouchPosition.ReadValue<Vector2>());
                if (currentDistance > distance)
                {
                    ZoomCamera(1f);
                }
                else if (currentDistance < distance)
                {
                    ZoomCamera(-1f);
                }
                distance = currentDistance;
                await UniTask.Yield(cancellationToken: cancellationToken.Token);
            }
            isLock = false;
        }

        private void ZoomCamera(float zoomValue)
        {
            guiController.GamePlayScreen.BuildMenuController.HideMenu();
            float zoomOffsetZ = Mathf.Clamp(camOffset.m_Offset.z + (zoomValue * zoomSensitivity * Time.deltaTime), minZoomHeight, maxZoomHeight);

            camOffset.m_Offset = new Vector3(camOffset.m_Offset.x, camOffset.m_Offset.y, zoomOffsetZ);

            //float prevZoom = cam.m_Lens.FieldOfView;
            //cam.m_Lens.FieldOfView = Mathf.Clamp(cam.m_Lens.FieldOfView + (-zoomValue * zoomSensitivity * Time.deltaTime),
            //    minZoom, maxZoom);
            //float newZoom = cam.m_Lens.FieldOfView;    ////reserve method

            if (zoomValue < 0)
            {
                transform.position = Vector3.Lerp(transform.position, centerPosition, zoomSensitivity / 10 * Time.deltaTime);

                float zoomRecomposer = Mathf.Clamp(camRecomposer.m_Tilt +
                    (-zoomValue * zoomSensitivity * Time.deltaTime), minZoomTilt, maxZoomTilt);
                camRecomposer.m_Tilt = zoomRecomposer;

                cameraViewCollider.size += cameraViewCollider.size * (-zoomValue / zoomSensitivity);
                if (cameraViewCollider.size.x > standardViewColliderSize.x || camOffset.m_Offset.z == minZoomHeight)
                {
                    cameraViewCollider.size = standardViewColliderSize;
                }
            }
            else if (zoomValue > 0)
            {
                if (camOffset.m_Offset.z > maxZoomHeight * startTilting)
                {
                    float zoomRecomposer = Mathf.Clamp(camRecomposer.m_Tilt +
                    (-zoomValue * zoomSensitivity * Time.deltaTime), minZoomTilt, maxZoomTilt);
                    camRecomposer.m_Tilt = zoomRecomposer;
                }
                cameraViewCollider.size -= cameraViewCollider.size * (zoomValue / zoomSensitivity);
                if (cameraViewCollider.size.x < (standardViewColliderSize.x / 3) || camOffset.m_Offset.z == maxZoomHeight)
                {
                    cameraViewCollider.size = standardViewColliderSize / 3;
                }
            }
        }

        private async void HomeBaseDestroyCamera()
        {
            isLock = true;
            inputs.Disable();
            guiController.GamePlayScreen.BuildMenuController.HideMenu();
            while (Vector3.Distance(homeBaseDestroyPivot.position, transform.position) >= 0.3f || camOffset.m_Offset.z != minZoomHeight
                || camRecomposer.m_Tilt != maxZoomTilt)
            {

                float zoomOffsetZ = Mathf.Clamp(camOffset.m_Offset.z +
                    (-zoomSensitivity * Time.deltaTime), minZoomHeight, maxZoomHeight);

                camOffset.m_Offset = new Vector3(camOffset.m_Offset.x, camOffset.m_Offset.y, zoomOffsetZ);

                transform.position += Vector3.ClampMagnitude(homeBaseDestroyPivot.position - transform.position,
                    (homeBaseDestroyPivot.position - transform.position).magnitude) * (zoomSensitivity / 10 * Time.deltaTime);
                float zoomRecomposer = Mathf.Clamp(camRecomposer.m_Tilt +
                    (zoomSensitivity * Time.deltaTime), minZoomTilt, maxZoomTilt);
                camRecomposer.m_Tilt = zoomRecomposer;
                await UniTask.Yield(cancellationToken: cancellationToken.Token);
            }
        }


        private async void ZoomWin()
        {
            guiController.GamePlayScreen.BuildMenuController.HideMenu();
            while (inputs.General.CameraZoom.phase == UnityEngine.InputSystem.InputActionPhase.Started
                || inputs.General.CameraZoom.phase == UnityEngine.InputSystem.InputActionPhase.Performed)
            {
                float zoomValue = inputs.General.CameraZoom.ReadValue<float>();
                float zoomOffsetZ = Mathf.Clamp(camOffset.m_Offset.z + (zoomValue * zoomSensitivity * Time.deltaTime), minZoomHeight, maxZoomHeight);
                camOffset.m_Offset = new Vector3(camOffset.m_Offset.x, camOffset.m_Offset.y, zoomOffsetZ);

                //float prevZoom = cam.m_Lens.FieldOfView;
                //cam.m_Lens.FieldOfView = Mathf.Clamp(cam.m_Lens.FieldOfView + (-zoomValue * zoomSensitivity * Time.deltaTime),
                //    minZoom, maxZoom);
                //float newZoom = cam.m_Lens.FieldOfView;    ////reserve method

                if (zoomValue < 0)
                {
                    transform.position = Vector3.Lerp(transform.position, centerPosition, zoomSensitivity / 10 * Time.deltaTime);

                    float zoomRecomposer = Mathf.Clamp(camRecomposer.m_Tilt +
                        (-zoomValue * zoomSensitivity * Time.deltaTime), minZoomTilt, maxZoomTilt);
                    camRecomposer.m_Tilt = zoomRecomposer;

                    cameraViewCollider.size += cameraViewCollider.size * (-zoomValue / zoomSensitivity);
                    if (cameraViewCollider.size.x > standardViewColliderSize.x || camOffset.m_Offset.z == minZoomHeight)
                    {
                        cameraViewCollider.size = standardViewColliderSize;
                    }
                }
                else if (zoomValue > 0)
                {
                    if (camOffset.m_Offset.z > maxZoomHeight * startTilting)
                    {
                        float zoomRecomposer = Mathf.Clamp(camRecomposer.m_Tilt +
                        (-zoomValue * zoomSensitivity * Time.deltaTime), minZoomTilt, maxZoomTilt);
                        camRecomposer.m_Tilt = zoomRecomposer;
                    }
                    cameraViewCollider.size -= cameraViewCollider.size * (zoomValue / zoomSensitivity);
                    if (cameraViewCollider.size.x < (standardViewColliderSize.x / 3) || camOffset.m_Offset.z == maxZoomHeight)
                    {
                        cameraViewCollider.size = standardViewColliderSize / 3;
                    }
                }
                await UniTask.Yield(cancellationToken: cancellationToken.Token);
            }
        }

        private async void MoveCameraWin()
        {
            if (isLock)
            {
                return;
            }
            cancellationToken.Cancel();
            cancellationToken = new CancellationTokenSource();
            guiController.GamePlayScreen.BuildMenuController.HideMenu();
            while (inputs.General.Camera.phase == UnityEngine.InputSystem.InputActionPhase.Started
                || inputs.General.Camera.phase == UnityEngine.InputSystem.InputActionPhase.Performed)
            {
                if (isLock)
                {
                    return;
                }
                Vector2 direction = inputs.General.Camera.ReadValue<Vector2>();
                if (direction.x < 0 && cameraViewCollider.bounds.min.x <= cameraBoundsCollider.bounds.min.x ||
             direction.x > 0 && cameraViewCollider.bounds.max.x >= cameraBoundsCollider.bounds.max.x)
                {
                    direction.x = 0;
                }
                if (direction.y < 0 && cameraViewCollider.bounds.min.z <= cameraBoundsCollider.bounds.min.z ||
                    direction.y > 0 && cameraViewCollider.bounds.max.z >= cameraBoundsCollider.bounds.max.z)
                {
                    direction.y = 0;
                }
                Vector3 newPos = cam.transform.position + new Vector3(direction.x, 0f,
                     direction.y);
                cam.transform.position = Vector3.Slerp(cam.transform.position, newPos, Time.deltaTime * panSpeed);
                CheckCameraBounds();
                await UniTask.Yield(cancellationToken: cancellationToken.Token);
            }
            CheckCameraBounds();
        }

    }
}

