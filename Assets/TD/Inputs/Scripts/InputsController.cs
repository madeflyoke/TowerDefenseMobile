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
        private void Initialize()
        {
            enabled = false;
            enabled = true;
            cam = Camera.main;
            inputs.Enable();
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
           
            inputs.General.Select.performed += ctx => CheckSelectedPosition();
#else
            inputs.TouchInput.Select.performed += ctx => CheckSelectedPosition();
#endif
        }
        private void OnEnable()
        {
            inputs = new PlayerInputs();
            gameManager.launchGameStateEvent += inputs.Disable;
            gameManager.startLevelEvent += Initialize;
            gameManager.restartLevelEvent += Initialize;
            gameManager.endGameEvent += inputs.Disable;
        }

        private void OnDisable()
        {
            inputs.Disable();
            gameManager.launchGameStateEvent -= inputs.Disable;
            gameManager.startLevelEvent -= Initialize;
            gameManager.restartLevelEvent -= Initialize;
            gameManager.endGameEvent -= inputs.Disable;
        }

        private void CheckSelectedPosition()
        {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
            Ray ray = cam.ScreenPointToRay(inputs.General.SelectPosition.ReadValue<Vector2>());
#else
            Ray ray = cam.ScreenPointToRay(inputs.TouchInput.CameraFirstTouchPosition.ReadValue<Vector2>());
#endif
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

