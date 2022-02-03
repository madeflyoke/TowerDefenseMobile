using UnityEngine;
using System;
using UnityEngine.EventSystems;
using TD.GamePlay.Managers;
using Zenject;

namespace TD.Inputs
{
    public class InputsController : MonoBehaviour
    {
        [Inject] private GameManager gameManager;

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
            gameManager.endGameEvent += inputs.Disable;
        }
        private void OnDisable()
        {
            inputs.Disable();
            gameManager.endGameEvent -= inputs.Disable;
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

