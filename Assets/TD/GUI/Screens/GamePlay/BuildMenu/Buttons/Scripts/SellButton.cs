using UnityEngine;

namespace TD.GUI.Screens.GamePlay.BuildMenu.Buttons
{
    public class SellButton : BaseButton
    {
        private BuildMenuController buildMenu;

        protected override void Awake()
        {
            base.Awake();
            buildMenu = GetComponentInParent<BuildMenuController>();    
        }

        protected override void Listeners()
        {
            Sell();
        }

        private void Sell()
        {
            buildMenu.PlayerInfo.AddCurrency((int)(buildMenu.CurrentTower.Cost * buildMenu.PlayerInfo.SellMultiplier));
            buildMenu.TowerSpotsContainer.RemoveTowerFromSpot(buildMenu.CurrentTowerSpot);
            buildMenu.CurrentTower.DestroyTower();
            Debug.Log("SOLD");
            buildMenu.HideMenu();
        }
    }
}

