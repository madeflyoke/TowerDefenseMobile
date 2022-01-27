using UnityEngine;
using Cinemachine;
using Zenject;
using TD.GUI;

namespace TD.Cameras
{
    public class CameraController : MonoBehaviour
    {
        [Inject] private GUIController guiController;

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
        }

        private void Update()
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

        private void OnDisable()
        {
            inputs.Disable();
        }

        private void MoveCamera(Vector2 direction)
        {
            guiController.gamePlayScreen.buildMenuController.HideMenu();
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
            cam.transform.position = newPos;
        }

        private void ZoomCamera(float zoomValue)
        {
            guiController.gamePlayScreen.buildMenuController.HideMenu();
            float zoomOffsetZ = Mathf.Clamp(camOffset.m_Offset.z + (zoomValue * zoomSensitivity * Time.deltaTime), minZoomHeight, maxZoomHeight);

            camOffset.m_Offset = new Vector3(camOffset.m_Offset.x, camOffset.m_Offset.y, zoomOffsetZ);

            if (camOffset.m_Offset.z > maxZoomHeight * startTilting)
            {
                float zoomRecomposer = Mathf.Clamp(camRecomposer.m_Tilt +
                    (-zoomValue * zoomSensitivity * Time.deltaTime), minZoomTilt, maxZoomTilt);
                camRecomposer.m_Tilt = zoomRecomposer;
            }

            //float prevZoom = cam.m_Lens.FieldOfView;
            //cam.m_Lens.FieldOfView = Mathf.Clamp(cam.m_Lens.FieldOfView + (-zoomValue * zoomSensitivity * Time.deltaTime),
            //    minZoom, maxZoom);
            //float newZoom = cam.m_Lens.FieldOfView;    ////reserve method

            if (zoomValue < 0)
            {
                transform.position = Vector3.Lerp(transform.position, centerPosition, zoomSensitivity / 10 * Time.deltaTime);
                if (cameraViewCollider.size.x <= standardViewColliderSize.x)
                {
                    cameraViewCollider.size += (cameraViewCollider.size * (-zoomValue / 100));
                }
            }
            else if(zoomValue>0)
            {
                if (cameraViewCollider.size.x >= (standardViewColliderSize.x / 3))
                {
                    cameraViewCollider.size -= (cameraViewCollider.size * (zoomValue / 100));
                }
            }

        }
    }
}

