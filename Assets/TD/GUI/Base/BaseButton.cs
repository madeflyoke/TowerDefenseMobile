using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TD.GUI.Screens.GamePlay.BuildMenu.Buttons
{
    public class BaseButton : MonoBehaviour
    {
        private Button button;

        protected virtual void Awake()
        {
            button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(Listeners);
        }

        private void OnDisable()
        {
            button?.onClick.RemoveListener(Listeners);
        }

        protected virtual void Listeners() { }
    }
}

