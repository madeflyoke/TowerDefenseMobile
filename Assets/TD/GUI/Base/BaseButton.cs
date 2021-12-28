using UnityEngine;
using UnityEngine.UI;

namespace TD.GUI.Screens.GamePlay.BuildMenu.Buttons
{
    public abstract class BaseButton : MonoBehaviour
    {
        protected Button button;
     
        protected virtual void Awake()
        {
            button = GetComponent<Button>();
        }

        protected virtual void OnEnable()
        {
            button.onClick.AddListener(Listeners);
        }

        protected virtual void OnDisable()
        {
            button.onClick.RemoveListener(Listeners);
        }

        protected virtual void Listeners() { }
    }
}

