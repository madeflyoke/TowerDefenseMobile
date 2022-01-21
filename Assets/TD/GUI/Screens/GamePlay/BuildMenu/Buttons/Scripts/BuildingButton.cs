using UnityEngine;
using TD.GamePlay.Towers;
using UnityEngine.UI;
using Zenject;

namespace TD.GUI.Screens.GamePlay.BuildMenu.Buttons
{
    public class BuildingButton : BaseButton
    {
        [SerializeField] private BaseTower towerToBuild;
        [SerializeField] private Text costField;
        private BuildMenuController buildMenu;
        private bool canBuild;

        protected override void Awake()
        {
            base.Awake();
            buildMenu = GetComponentInParent<BuildMenuController>();
            costField.text = towerToBuild.Cost.ToString();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            buildMenu.PlayerInfo.currencyChangedEvent += CheckForEnoughCurrency;
            if (buildMenu.PlayerInfo.CurrencyAmount < towerToBuild.Cost)
            {
                canBuild = false;
                costField.color = buildMenu.DisableCostColor;
            }
            else
            {
                canBuild=true;
                costField.color = buildMenu.EnableCostColor;
            }
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            buildMenu.PlayerInfo.currencyChangedEvent -= CheckForEnoughCurrency;
        }

        protected override void Listeners()
        {
            BuildTower();
        }

        private void CheckForEnoughCurrency(int currentCurrency)
        {
            if (currentCurrency >= towerToBuild.Cost)
            {
                costField.color = buildMenu.EnableCostColor;
                canBuild = true;
            }
        }

        private void BuildTower()
        {
            if (canBuild==false)
            {
                return;
            }
            BaseTower tower = buildMenu.Container.InstantiatePrefabForComponent<BaseTower>(
                towerToBuild, buildMenu.CurrentTowerSpot.transform.position,
                Quaternion.identity, buildMenu.CurrentTowerSpot.transform);
            buildMenu.TowerSpotsContainer.AddTowerToSpot(buildMenu.CurrentTowerSpot, tower);
            buildMenu.PlayerInfo.RemoveCurrency(tower.Cost);

            buildMenu.HideMenu();
        }
    }
}

