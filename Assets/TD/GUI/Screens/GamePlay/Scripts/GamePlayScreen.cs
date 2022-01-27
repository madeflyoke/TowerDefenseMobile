using TD.Inputs;
using TD.GUI.Screens.GamePlay.BuildMenu;
using UnityEngine;
using TD.GUI.Screens.GamePlay.HUD;

namespace TD.GUI.Screens.GamePlay
{
    public class GamePlayScreen : BaseScreen
    {
        private InputsController inputsController;
        public BuildMenuController buildMenuController { get; private set; }
        private HUDController hudController;

        private void Awake()
        {
            buildMenuController = GetComponentInChildren<BuildMenuController>();
            hudController = GetComponentInChildren<HUDController>();
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

        private void SortSelectedObject(GameObject selected) 
        {
            buildMenuController.HideMenu();
            if (selected.layer == 9) //towerSpots
            {              
                buildMenuController.SetMenu(selected);
            }
        }

    }
}

