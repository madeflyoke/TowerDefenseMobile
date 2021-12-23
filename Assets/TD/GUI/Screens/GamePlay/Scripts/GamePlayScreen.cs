using TD.Inputs;
using UnityEngine;
using TD.GUI.Screens.GamePlay.BuildMenu;
using TD.GamePlay.Towers.BuildSpots;

namespace TD.GUI.Screens.GamePlay
{
    public class GamePlayScreen : BaseScreen
    {
        private InputsController inputsController;
        private BuildMenuController buildMenuController;
        private TowerSpotsController towerSpotsController;

        private void Awake()
        {
            towerSpotsController = FindObjectOfType<TowerSpotsController>(); //!!!!!!
            buildMenuController = GetComponentInChildren<BuildMenuController>();        
            inputsController = FindObjectOfType<InputsController>(); //!!!!!!         
        }

        private void Start()
        {
            buildMenuController.HideMenu();
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
                buildMenuController.SetBuildMenu(selected);
            }
            else if (selected.layer == 10) //tower
            {
                buildMenuController.SetUpgradeMenu();
            }
        }

    }
}

