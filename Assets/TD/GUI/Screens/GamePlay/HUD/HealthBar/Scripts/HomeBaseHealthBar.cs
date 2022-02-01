using TD.GamePlay.HomeBuilding;
using UnityEngine;
using UnityEngine.UI;

namespace TD.GUI.Screens.GamePlay.HUD
{
    public class HomeBaseHealthBar : MonoBehaviour
    {
        [SerializeField] private Image fill;
        private HomeBase homeBase;
        private void Awake()
        {
            ResetHealthBar();
            homeBase = FindObjectOfType<HomeBase>();
        }

        private void OnEnable()
        {
            homeBase.homeBaseChangedHealthEvent += ChangeValue;
        }
        private void OnDisable()
        {
            homeBase.homeBaseChangedHealthEvent -= ChangeValue;
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

