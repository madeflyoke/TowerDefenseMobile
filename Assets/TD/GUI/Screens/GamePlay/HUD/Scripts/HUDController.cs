using TD.GamePlay.Managers;
using UnityEngine;
using Zenject;
using TMPro;
using TD.GUI.Screens.GamePlay.HUD.Buttons;

namespace TD.GUI.Screens.GamePlay.HUD
{
    public class HUDController : MonoBehaviour
    {
        [Inject] private GameManager gameManager;

        [SerializeField] private TMP_Text currencyField;
        [SerializeField] private StartWavesButton startWavesButton;
        [SerializeField] private HomeBaseHealthBar homeBaseHealthBar;

        public void Initialize()
        {
            homeBaseHealthBar.Initialize();
        }

        private void OnEnable()
        {
            gameManager.currencyChangedEvent += ChangeCurrencyUI;
        }

        private void OnDisable()
        {
            gameManager.currencyChangedEvent -= ChangeCurrencyUI;
        }

        public void HideHUD()
        {
            gameObject.SetActive(false);
        }
        public void ShowHUD()
        {
            gameObject.SetActive(true);
        }

        public void ResetHUD()
        {
            startWavesButton.gameObject.SetActive(false);
            startWavesButton.gameObject.SetActive(true);
            currencyField.text = gameManager.StartCurrencyAmount.ToString();          
        }

        private void ChangeCurrencyUI(int amount)
        {
            currencyField.text = amount.ToString();
        }
    }
}

