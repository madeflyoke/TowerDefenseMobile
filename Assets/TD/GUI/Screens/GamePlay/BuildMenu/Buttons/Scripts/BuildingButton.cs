using UnityEngine;
using TD.GamePlay.Towers;

namespace TD.GUI.Screens.GamePlay.BuildMenu.Buttons
{
    public class BuildingButton : BaseButton
    {
        [SerializeField] private BaseTower towerToBuild;
        private BuildMenuController buildMenuController;
        protected override void Awake()
        {
            base.Awake();
            buildMenuController = GetComponentInParent<BuildMenuController>();
        }

        protected override void Listeners()
        {
            BuildTower();
        }

        private void BuildTower()
        {
            BaseTower tower = Instantiate(
                towerToBuild, buildMenuController.CurrentTowerSpot.transform.position, Quaternion.identity,
                buildMenuController.towerSpotsController.transform);
            buildMenuController.towerSpotsController.AddTowerToSpot(buildMenuController.CurrentTowerSpot, tower);
            buildMenuController.HideMenu();
        }

    }
}

