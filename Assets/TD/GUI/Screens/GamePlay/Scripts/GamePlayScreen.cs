using TD.Inputs;
using TD.GUI.Screens.GamePlay.BuildMenu;
using UnityEngine;
using TD.GUI.Screens.GamePlay.HUD;
using Zenject;
using TD.GamePlay.Managers;
using TD.GUI.Screens.GamePlay.Menu;

namespace TD.GUI.Screens.GamePlay
{
    public class GamePlayScreen : BaseScreen
    {
        private InputsController inputsController;
        public BuildMenuController buildMenuController { get; private set; }
        private HUDController hudController;
        private GamePlayMenu gamePlayMenu; 

        private void Awake()
        {
            buildMenuController = GetComponentInChildren<BuildMenuController>();
            hudController = GetComponentInChildren<HUDController>();
            gamePlayMenu = GetComponentInChildren<GamePlayMenu>();
            inputsController = FindObjectOfType<InputsController>();

            buildMenuController.Initialize();
        }

        private void OnEnable()
        {
            inputsController.selectObjectEvent += SortSelectedObject;
        }

        private void OnDisable()
        {
            inputsController.selectObjectEvent -= SortSelectedObject;
        }

        public override void Show()
        {
            gameObject.SetActive(true);
        }
        public override void Hide()
        {
            gameObject.SetActive(false);
            ResetScreen();
        }

        public void ResetScreen()
        {
            buildMenuController.HideMenu();
            hudController.ResetHUD();
            gamePlayMenu.Hide();
        }

        private void SortSelectedObject(GameObject selected)
        {
            buildMenuController.HideMenu();
            gamePlayMenu.Hide();
            if (selected.layer == 9) //towerSpots
            {
                buildMenuController.SetMenu(selected);
            }
        }

    }
}

