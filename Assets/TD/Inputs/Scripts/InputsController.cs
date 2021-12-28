using UnityEngine;
using System;
using UnityEngine.EventSystems;

namespace TD.Inputs
{
    public class InputsController : MonoBehaviour
    {
        public event Action<GameObject> selectObjectEvent;

        private PlayerInputs inputs;
        private Camera cam;

        private void Awake()
        {
            inputs = new PlayerInputs();
            cam = Camera.main;   
        }

        private void OnEnable()
        {
            inputs.Enable();
            inputs.General.Select.performed += ctx => CheckSelectedPosition();
        }
        private void OnDisable()
        {
            inputs.Disable();
        }

        private void CheckSelectedPosition()
        {
            Ray ray = cam.ScreenPointToRay(inputs.General.SelectPosition.ReadValue<Vector2>());
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                selectObjectEvent?.Invoke(hitInfo.collider.gameObject);
            }
        }
    }
}

