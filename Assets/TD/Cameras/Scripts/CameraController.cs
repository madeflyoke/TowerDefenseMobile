using UnityEngine;
using Cinemachine;

namespace TD.Cameras
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float panSpeed;
        [SerializeField] private BoxCollider boundsCollider;
        private PlayerInputs inputs;
        private CinemachineVirtualCamera cam;

        private void Awake()
        {
            cam = GetComponent<CinemachineVirtualCamera>();
            inputs = new PlayerInputs();
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
        }

        private void OnDisable()
        {
            inputs.Disable();
        }

        private void MoveCamera(Vector2 direction)
        {
            Vector3 newPos = cam.transform.position+new Vector3(direction.x, 0f, direction.y) * panSpeed * Time.deltaTime;
            float x = Mathf.Clamp(newPos.x, boundsCollider.bounds.min.x, boundsCollider.bounds.max.x);
            float z = Mathf.Clamp(newPos.z, boundsCollider.bounds.min.z, boundsCollider.bounds.max.z);
            Vector3 finalPos = new Vector3(x, cam.transform.position.y, z);
            cam.transform.position = finalPos;
        }
    }
}

