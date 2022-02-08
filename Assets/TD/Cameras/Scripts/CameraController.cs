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

            if (isLock == false)
            {
                Vector2 direction = inputs.General.Camera.ReadValue<Vector2>();
                if (direction.x != 0 || direction.y != 0)
                {
                    MoveCamera(direction);
                }

                float zoom = inputs.General.CameraZoom.ReadValue<float>();
                if (zoom != 0)
                {
                    ZoomCamera(zoom);
                }
            }
        }

        private void MoveCamera(Vector2 direction)
        {
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
            cam.transform.position = Vector3.Lerp(cam.transform.position, newPos, Time.deltaTime * panSpeed);
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

                cameraViewCollider.size += cameraViewCollider.size * (-zoomValue / 100);
                if (cameraViewCollider.size.x > standardViewColliderSize.x)
                {
                    cameraViewCollider.size = standardViewColliderSize;
                }
                float zoomRecomposer = Mathf.Clamp(camRecomposer.m_Tilt +
                    (-zoomValue * zoomSensitivity * Time.deltaTime), minZoomTilt, maxZoomTilt);
                camRecomposer.m_Tilt = zoomRecomposer;
            }
            else if (zoomValue > 0)
            {
                cameraViewCollider.size -= cameraViewCollider.size * (zoomValue / 100);
                if (cameraViewCollider.size.x < (standardViewColliderSize.x / 3))
                {
                    cameraViewCollider.size = standardViewColliderSize / 3;
                }
                if (camOffset.m_Offset.z > maxZoomHeight * startTilting)
                {
                    float zoomRecomposer = Mathf.Clamp(camRecomposer.m_Tilt +
                    (-zoomValue * zoomSensitivity * Time.deltaTime), minZoomTilt, maxZoomTilt);
                    camRecomposer.m_Tilt = zoomRecomposer;
                }
            }
        }

        private async void HomeBaseDestroyCamera()
        {
            isLock = true;
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

