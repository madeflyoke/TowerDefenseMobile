using TD.GamePlay.Towers;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TD.GUI.Screens.GamePlay.BuildMenu.Buttons
{
    public class UpgradeButton : BaseButton
    {
        [SerializeField] private TMP_Text costField;
        private BuildMenuController buildMenu;
        private bool canUpgrade;

        protected override void Awake()
        {
            base.Awake();
            buildMenu = GetComponentInParent<BuildMenuController>();
        }

        protected override void Listeners()
        {
            Upgrade();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            buildMenu.PlayerInfo.currencyChangedEvent += CheckForEnoughCurrency;
            if (buildMenu.CurrentTower==null)
            {
                return;
            }
            costField.text = buildMenu.CurrentTower.NextTowerLevel.Cost.ToString();
            if (buildMenu.PlayerInfo.CurrencyAmount < buildMenu.CurrentTower.NextTowerLevel.Cost)
            {
                canUpgrade = false;
                costField.color = buildMenu.DisableCostColor;
            }
            else
            {
                canUpgrade = true;
                costField.color = buildMenu.EnableCostColor;
            }
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            buildMenu.PlayerInfo.currencyChangedEvent -= CheckForEnoughCurrency;
        }

        private void CheckForEnoughCurrency(int currentCurrency)
        {
            if (currentCurrency >= buildMenu.CurrentTower.NextTowerLevel.Cost)
            {
                costField.color = buildMenu.EnableCostColor;
                canUpgrade = true;
            }
        }

        private void Upgrade()
        {
            if (canUpgrade == false)
            {
                Debug.Log("false");
                return;
            }
            buildMenu.PlayerInfo.RemoveCurrency(buildMenu.CurrentTower.NextTowerLevel.Cost);
            buildMenu.TowerSpotsContainer.RemoveTowerFromSpot(buildMenu.CurrentTowerSpot);
            BaseTower tower = buildMenu.Container.InstantiatePrefabForComponent<BaseTower>(
               buildMenu.CurrentTower.NextTowerLevel, buildMenu.CurrentTowerSpot.transform.position,
               Quaternion.identity, buildMenu.CurrentTowerSpot.transform);
            buildMenu.TowerSpotsContainer.AddTowerToSpot(buildMenu.CurrentTowerSpot, tower);
            buildMenu.CurrentTower.DestroyTower();
            buildMenu.HideMenu();
        }
    }

}
