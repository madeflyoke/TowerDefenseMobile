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
        private StartWavesButton startWavesButton;

        private void Awake()
        {
            startWavesButton = GetComponentInChildren<StartWavesButton>();
            ResetHUD();
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
            currencyField.text = gameManager.StartCurrencyAmount.ToString();
            startWavesButton?.gameObject.SetActive(true);
        }

        private void ChangeCurrencyUI(int amount)
        {
            currencyField.text = amount.ToString();
        }
    }
}

