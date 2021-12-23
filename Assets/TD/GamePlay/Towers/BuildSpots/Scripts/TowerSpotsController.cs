using System.Collections.Generic;
using UnityEngine;

namespace TD.GamePlay.Towers.BuildSpots
{
    public class TowerSpotsController : MonoBehaviour
    {
        public Dictionary<GameObject, BaseTower> SpotTowers { get; private set; }

        private void Awake()
        {
            SpotTowers = new Dictionary<GameObject, BaseTower>();
        }

        public void AddTowerToSpot(GameObject spot, BaseTower tower)
        {
            if (tower==null)
            {
                Debug.Log("Tower is null");
                return;
            }
            SpotTowers.Add(spot, tower);
        }

        public void RemoveTowerFromSpot(GameObject spot)
        {
            SpotTowers.Remove(spot);
        }
    }
}

   

