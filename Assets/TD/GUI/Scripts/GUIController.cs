using TD.GUI.Screens.GamePlay;

using UnityEngine;

namespace TD.GUI
{
    public class GUIController : MonoBehaviour
    {
        public GamePlayScreen gamePlayScreen { get;private set; }

        private void Awake()
        {
            gamePlayScreen = GetComponentInChildren<GamePlayScreen>();
        }


    }

}

