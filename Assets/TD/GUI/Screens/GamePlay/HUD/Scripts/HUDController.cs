using TD.GamePlay.Managers;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

namespace TD.GUI.Screens.GamePlay.HUD
{
    public class HUDController : MonoBehaviour
    {
        [Inject] private GameManager playerInfo;

        [SerializeField] private Text currencyField;

        private void Awake()
        {
            currencyField.text = playerInfo.StartCurrencyAmount.ToString();
        }

        private void OnEnable()
        {
            playerInfo.currencyChangedEvent += ChangeCurrencyUI;
        }
        private void OnDisable()
        {
            playerInfo.currencyChangedEvent -= ChangeCurrencyUI;
        }

        private void ChangeCurrencyUI(int amount)
        {
            currencyField.text = amount.ToString();
        }
    }
}

