using TD.Inputs;
using TD.GUI.Screens.GamePlay.BuildMenu;
using TD.GamePlay.Towers.BuildSpots;
using UnityEngine;
using TD.GUI.Screens.GamePlay.HUD;

namespace TD.GUI.Screens.GamePlay
{
    public class GamePlayScreen : BaseScreen
    {
        private InputsController inputsController;
        private BuildMenuController buildMenuController;
        private TowerSpotsContainer towerSpotsController;
        private HUDController hudController;

        private void Awake()
        {
            buildMenuController = GetComponentInChildren<BuildMenuController>();
            hudController = GetComponentInChildren<HUDController>();
            inputsController = FindObjectOfType<InputsController>();
            towerSpotsController = FindObjectOfType<TowerSpotsContainer>();
            
            buildMenuController.Initialize(towerSpotsController);
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

