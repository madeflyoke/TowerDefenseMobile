using TD.GamePlay.Towers.BuildSpots;
using UnityEngine;

namespace TD.GUI.Screens.GamePlay.BuildMenu
{
    public class BuildMenuController : MonoBehaviour
    {
        public GameObject CurrentTowerSpot { get; private set; }
        public TowerSpotsController towerSpotsController { get; private set; }
        private Camera cam;

        private void Awake()
        {
            cam = FindObjectOfType<Camera>(); //!!!!!
            towerSpotsController = FindObjectOfType<TowerSpotsController>(); //!!!!!!!!
        }

        public void SetBuildMenu(GameObject towerSpot) //tower
        {
            if (towerSpotsController.SpotTowers.ContainsKey(towerSpot))
            {
                SetUpgradeMenu();
                return;
            }
            CurrentTowerSpot = towerSpot;
            gameObject.transform.position = cam.WorldToScreenPoint(towerSpot.transform.position);
            gameObject.SetActive(true);
        }

        public void HideMenu()
        {
            CurrentTowerSpot = null;
            gameObject.SetActive(false);         
        }

        public void SetUpgradeMenu()
        {
            Debug.Log("TestUpgrade");
        }

    }
}

