using TD.GamePlay.HomeBuilding;
using UnityEngine;
using UnityEngine.UI;

namespace TD.GUI.Screens.GamePlay.HUD
{
    public class HomeBaseHealthBar : MonoBehaviour
    {
        [SerializeField] private Image fill;
        private HomeBase homeBase;

        public void Initialize()
        {
            homeBase = FindObjectOfType<HomeBase>();
            homeBase.homeBaseChangedHealthEvent -= ChangeValue;
            ResetHealthBar();
            homeBase.homeBaseChangedHealthEvent += ChangeValue;
        }

        private void ChangeValue(float homeBaseHealth)
        {
            fill.fillAmount = homeBaseHealth / homeBase.MaxHealthPoints;
        }

        private void ResetHealthBar()
        {
            fill.fillAmount = 1;
        }
    }
}

