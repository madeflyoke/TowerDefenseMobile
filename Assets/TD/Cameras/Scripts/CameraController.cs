using UnityEngine;
using Cinemachine;
using Cysharp.Threading.Tasks;

namespace TD.Cameras
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float panSpeed;
        [SerializeField] private float maxZoom;
        [SerializeField] private float minZoom;
        [SerializeField] private float zoomSensitivity;
        [SerializeField] private BoxCollider cameraViewCollider;
        [SerializeField] private BoxCollider cameraBoundsCollider;
        private PlayerInputs inputs;
        private CinemachineVirtualCamera cam;
        private Vector3 centerPosition;

        private void Awake()
        {
            cam = GetComponent<CinemachineVirtualCamera>();
            inputs = new PlayerInputs();
            centerPosition = transform.position;
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
            float prevZoom = cam.m_Lens.FieldOfView;
            cam.m_Lens.FieldOfView = Mathf.Clamp(cam.m_Lens.FieldOfView + (-zoomValue * zoomSensitivity * Time.deltaTime),
                minZoom, maxZoom);
            float newZoom = cam.m_Lens.FieldOfView;
            if (zoomValue < 0 && prevZoom != newZoom)
            {
                transform.position = Vector3.Lerp(transform.position, centerPosition, zoomSensitivity/10 * Time.deltaTime); /*Vector3.MoveTowards(transform.position, centerPosition, zoomValue * zoomSensitivity * Time.deltaTime);*/
            }
            cameraViewCollider.size *= newZoom / prevZoom;
        }
    }
}

