using System.Collections;
using System.Collections.Generic;
using TD.GamePlay.HomeBuilding;
using UnityEngine;
using DG.Tweening;

namespace TD.GUI.Screens.EndGame
{
    public class StarsAccrual : MonoBehaviour
    {
        [SerializeField] private GameObject firstStar;
        [SerializeField] private GameObject secondStar;
        [SerializeField] private GameObject thirdStar;

        public bool SetStars(HomeBase.DamageState damageState)
        {

            switch (damageState)
            {
                case HomeBase.DamageState.None:
                    firstStar.transform.DOPunchScale(Vector3.one * 0.1f, 0.3f).SetDelay(1f)
                        .OnStart(() => firstStar.SetActive(true));
                    secondStar.transform.DOPunchScale(Vector3.one * 0.1f, 0.3f).SetDelay(1.5f)
                        .OnStart(() => secondStar.SetActive(true));
                    thirdStar.transform.DOPunchScale(Vector3.one * 0.1f, 0.3f).SetDelay(2f)
                        .OnStart(() => thirdStar.SetActive(true));
                    return true;
                case HomeBase.DamageState.Damaged:
                    firstStar.transform.DOPunchScale(Vector3.one * 0.1f, 0.3f).SetDelay(1f)
                        .OnStart(() => firstStar.SetActive(true));
                    secondStar.transform.DOPunchScale(Vector3.one * 0.1f, 0.3f).SetDelay(1.5f)
                        .OnStart(() => secondStar.SetActive(true));
                    return true;
                case HomeBase.DamageState.First:
                case HomeBase.DamageState.Second:
                    firstStar.transform.DOPunchScale(Vector3.one * 0.1f, 0.3f).SetDelay(1f)
                        .OnStart(() => firstStar.SetActive(true));
                    return true;
                case HomeBase.DamageState.Destroyed:
                    return false;
            }
            return false;
        }

        public void ResetStars()
        {
            firstStar.transform.DOKill();
            firstStar.SetActive(false);

            secondStar.transform.DOKill();
            secondStar.SetActive(false);

            thirdStar.transform.DOKill();
            thirdStar.SetActive(false);
        }
    }
}

