using Cinemachine;
using TD.GamePlay.Managers;
using TD.GamePlay.Towers;
using TD.GamePlay.Towers.BuildSpots;
using TD.GUI.Screens.GamePlay.BuildMenu.Buttons;
using UnityEngine;
using Zenject;
using DG.Tweening;

namespace TD.GUI.Screens.GamePlay.BuildMenu
{
    public class BuildMenuController : MonoBehaviour
    {
        [Inject] public GameManager PlayerInfo { get; private set; }
        [Inject] public DiContainer Container { get; private set; }

        [SerializeField] private Color enableCostColor;
        [SerializeField] private Color disableCostColor;
        [SerializeField] private float maxBuildMenuSize;
        [SerializeField] private BuildingButton buildingButton;
        [SerializeField] private UpgradeButton upgradeButton;
        [SerializeField] private SellButton sellButton;
        public Color EnableCostColor { get => enableCostColor; }
        public Color DisableCostColor { get => disableCostColor; }
        public GameObject CurrentTowerSpot { get; private set; }
        public BaseTower CurrentTower { get; private set; }
        public TowerSpotsContainer TowerSpotsContainer { get; private set; }

        private Vector3 standardScale;
        private Camera cam;
        private CinemachineCameraOffset virtualCamOffset;
        public Pooler pooler { get;private set; }

        private void Awake()
        {
            buildingButton = GetComponentInChildren<BuildingButton>();
            upgradeButton = GetComponentInChildren<UpgradeButton>();
            sellButton = GetComponentInChildren<SellButton>();
        }
        public void Initialize()
        {
            standardScale = transform.localScale;
            cam = Camera.main;
            virtualCamOffset = FindObjectOfType<CinemachineVirtualCamera>().gameObject
                .GetComponent<CinemachineCameraOffset>();
            TowerSpotsContainer = new TowerSpotsContainer();
            pooler = FindObjectOfType<Pooler>();
        }
     
        public void SetMenu(GameObject towerSpot)
        {             
            CurrentTowerSpot = towerSpot;
            transform.localScale = transform.localScale*((maxBuildMenuSize-1f)*(virtualCamOffset.m_Offset.z)/40f)+standardScale;
            transform.DOScale(transform.localScale, 0.1f).From(Vector3.zero);
            if (TowerSpotsContainer.SpotTowers.ContainsKey(towerSpot))
            {
                TowerSpotsContainer.SpotTowers.TryGetValue(towerSpot, out BaseTower targetTower);
                if (targetTower != null)
                {
                    CurrentTower = targetTower;
                    gameObject.transform.position = cam.WorldToScreenPoint(targetTower.transform.position + (Vector3.up * 2.5f));
                    CurrentTower.transform.DOPunchScale(Vector3.one * 0.2f, 0.2f);
                    CurrentTower.Targeter.AttackRangeCircle.SetActive(true);
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
                gameObject.transform.position = cam.WorldToScreenPoint(towerSpot.transform.position);
                SetBuildMenu();
            }
        }

        private void SetBuildMenu() //tower
        {
            buildingButton.gameObject.SetActive(true);
            gameObject.SetActive(true);
        }

        private void SetUpgradeMenu()
        {
            if (CurrentTower.NextTowerLevel != null)
            {
                upgradeButton.gameObject.SetActive(true);
            }
            sellButton.gameObject.SetActive(true);
            gameObject.SetActive(true);
        }

        public void HideMenu()
        {
            CurrentTowerSpot = null;
            if (CurrentTower!=null)
            {
                CurrentTower.Targeter.AttackRangeCircle.SetActive(false);
                CurrentTower = null;
            }      
            upgradeButton.gameObject.SetActive(false);
            buildingButton.gameObject.SetActive(false);
            sellButton.gameObject.SetActive(false);
            transform.localScale = standardScale;
            gameObject.SetActive(false);
        }
    }
}

