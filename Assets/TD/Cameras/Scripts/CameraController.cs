using UnityEngine;
using Cinemachine;
using Zenject;
using TD.GUI;
using Cysharp.Threading.Tasks;
using TD.GamePlay.Managers;

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

        private void Awake()
        {
            cam = GetComponent<CinemachineVirtualCamera>();
            camOffset = GetComponent<CinemachineCameraOffset>();
            camRecomposer = GetComponent<CinemachineRecomposer>();
            inputs = new PlayerInputs();
            centerPosition = transform.position;
            standardViewColliderSize = cameraViewCollider.size;
        }

        private void OnEnable()
        {
            inputs.Enable();
            gameManager.endGameEvent += HomeBaseDestroyCamera;
            inputs.TouchInput.CameraZoomStart.started += ctx => CalculateZoomTouch();
            inputs.TouchInput.CameraMovementStart.started += ctx => MoveCamera();
        }
        private void OnDisable()
        {
            inputs.Disable();
            gameManager.endGameEvent -= HomeBaseDestroyCamera;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                HomeBaseDestroyCamera();
            }

            Debug.Log("PHASE MOVEMENT: " + inputs.TouchInput.CameraMovement.phase);
            Debug.Log("PHASE ZOOM FIRST: " + inputs.TouchInput.CameraZoomFirstPosition.phase);
            Debug.Log("PHASE ZOOM SECOND: " + inputs.TouchInput.CameraZoomSecondPosition.phase);
            Debug.Log("PHASE START: " + inputs.TouchInput.CameraZoomStart.phase);

        }

        private async void MoveCamera()
        {
            if (isLock)
            {
                return;
            }
            Debug.Log("CAMERA MOVE START PHASE " + inputs.TouchInput.CameraMovementStart.phase);
            Debug.Log("CAMERA MOVE PHASE IN: " + inputs.TouchInput.CameraMovement.phase);
            while (inputs.TouchInput.CameraMovementStart.phase != UnityEngine.InputSystem.InputActionPhase.Waiting)
            {
                if (isLock)
                {
                    return;
                }
                Vector2 direction = inputs.TouchInput.CameraMovement.ReadValue<Vector2>();
                Debug.Log("Direction: " + direction);
                direction *= 0.3f;
                guiController.GamePlayScreen.BuildMenuController.HideMenu();
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

                Vector3 newPos = cam.transform.position + new Vector3(direction.x, 0f, direction.y) * panSpeed * Time.deltaTime;
                cam.transform.position = Vector3.Slerp(cam.transform.position, newPos, Time.deltaTime * panSpeed);
                await UniTask.Yield();
            }

        }

        private async void CalculateZoomTouch()
        {
            Debug.Log("calculate");
            Debug.Log("PHASE CALCULATE " + inputs.TouchInput.CameraZoomStart.phase);
            float distance = 0f;
            isLock = true;
            while (inputs.TouchInput.CameraZoomStart.phase != UnityEngine.InputSystem.InputActionPhase.Waiting)
            {
                float currentDistance = Vector2.Distance(inputs.TouchInput.CameraZoomFirstPosition.ReadValue<Vector2>(),
                    inputs.TouchInput.CameraZoomSecondPosition.ReadValue<Vector2>());
                if (currentDistance > distance)
                {
                    ZoomCamera(1f);
                }
                else if (currentDistance < distance)
                {
                    ZoomCamera(-1f);
                }
                distance = currentDistance;

                Debug.Log("CURRENT " + currentDistance);
                Debug.Log("PREV " + distance);

                await UniTask.Yield();
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
                await UniTask.Yield();
            }
        }
    }
}

