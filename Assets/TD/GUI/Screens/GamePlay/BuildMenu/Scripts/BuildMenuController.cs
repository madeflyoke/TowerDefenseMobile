using TD.GamePlay.Managers;
using TD.GamePlay.Towers;
using TD.GamePlay.Towers.BuildSpots;
using TD.GUI.Screens.GamePlay.BuildMenu.Buttons;
using UnityEngine;
using Zenject;

namespace TD.GUI.Screens.GamePlay.BuildMenu
{
    public class BuildMenuController : MonoBehaviour
    {
        [Inject] public GameManager PlayerInfo { get; private set; }
        [Inject] public DiContainer Container { get; private set; }

        [SerializeField] private Color enableCostColor;
        [SerializeField] private Color disableCostColor;

        public Color EnableCostColor { get => enableCostColor; }
        public Color DisableCostColor { get => disableCostColor; }
        public GameObject CurrentTowerSpot { get; private set; }
        public BaseTower CurrentTower { get; private set; }
        public TowerSpotsContainer TowerSpotsContainer { get; private set; }
       
        private Camera cam;
        private BuildingButton buildingButton;
        private UpgradeButton upgradeButton;
        private SellButton sellButton;
        

        public void Initialize(TowerSpotsContainer towerSpots)
        {
            cam = Camera.main;
            TowerSpotsContainer = towerSpots;
            buildingButton = GetComponentInChildren<BuildingButton>();
            upgradeButton = GetComponentInChildren<UpgradeButton>();
            sellButton = GetComponentInChildren<SellButton>();
            HideMenu();
        }

        public void SetMenu(GameObject towerSpot)
        {
            CurrentTowerSpot = towerSpot;
            gameObject.transform.position = cam.WorldToScreenPoint(towerSpot.transform.position);
            if (TowerSpotsContainer.SpotTowers.ContainsKey(towerSpot))
            {
                TowerSpotsContainer.SpotTowers.TryGetValue(towerSpot, out BaseTower targetTower);
                if (targetTower != null)
                {
                    CurrentTower = targetTower;
                    SetUpgradeMenu();
                }
                else
                {
                    Debug.LogWarning("tower spot key's value was null reference, set control to build method");
                    TowerSpotsContainer.SpotTowers.Remove(towerSpot);
                    SetBuildMenu();
                }
            }
            else
            {
                SetBuildMenu();
            }
        }

        private void SetBuildMenu() //tower
        {                
            buildingButton.gameObject.SetActive(true);
            gameObject.SetActive(true);
            Debug.Log("buildmenu");
        }

        private void SetUpgradeMenu()
        {
            Debug.Log("TUT " + CurrentTower.NextTowerLevel);
            if (CurrentTower.NextTowerLevel!=null)
            {
                upgradeButton.gameObject.SetActive(true);
            }      
            sellButton.gameObject.SetActive(true);
            gameObject.SetActive(true);
            Debug.Log("upgrade menu");
        }

        public void HideMenu()
        {
            CurrentTowerSpot = null;
            CurrentTower = null;
            upgradeButton.gameObject.SetActive(false);
            buildingButton.gameObject.SetActive(false);
            sellButton.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}

