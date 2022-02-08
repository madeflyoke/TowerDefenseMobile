using TD.Inputs;
using TD.GUI.Screens.GamePlay.BuildMenu;
using UnityEngine;
using TD.GUI.Screens.GamePlay.HUD;
using TD.GUI.Screens.GamePlay.Menu;
using Zenject;

namespace TD.GUI.Screens.GamePlay
{
    public class GamePlayScreen : BaseScreen
    {
        [Inject] private InputsController inputsController;

        [SerializeField] private BuildMenuController buildMenuController;
        [SerializeField] private HUDController hudController;
        [SerializeField] private GamePlayMenu gamePlayMenu;
        public BuildMenuController BuildMenuController { get=>buildMenuController;}

        public void Initialize()
        {
            hudController.Initialize();
            BuildMenuController.Initialize();
            ResetScreen();
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
        }

        public void ResetScreen()
        {
            BuildMenuController.HideMenu();
            hudController.ResetHUD();
            gamePlayMenu.Hide();
        }

        private void SortSelectedObject(GameObject selected)
        {
            BuildMenuController.HideMenu();
            gamePlayMenu.Hide();
            if (selected.layer == 9) //towerSpots
            {
                BuildMenuController.SetMenu(selected);
            }
        }

    }
}

