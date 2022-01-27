using UnityEngine;
using System;
using TD.GamePlay.HomeBuilding;
using PathCreation;

namespace TD.GamePlay.Managers
{
    public class GameManager : MonoBehaviour
    {
        public event Action<int> currencyChangedEvent;
        public event Action endGameEvent;

        [SerializeField] private int startCurrencyAmount;
        [SerializeField] private float sellMultiplier;
        public int StartCurrencyAmount { get=>startCurrencyAmount;}
        public int CurrencyAmount { get; private set; }
        public float SellMultiplier { get => sellMultiplier;}
        private HomeBase homeBase;

        private void Awake()
        {
            CurrencyAmount = startCurrencyAmount;
            homeBase = FindObjectOfType<HomeBase>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                EndGameLogic();
            }
            
        }

        private void OnEnable()
        {
            homeBase.homeBaseDestroyedEvent += EndGameLogic;
        }
        private void OnDisable()
        {
            homeBase.homeBaseDestroyedEvent -= EndGameLogic;
        }

        private void EndGameLogic()
        {
            endGameEvent?.Invoke();
            Debug.LogWarning("END GAME");          
        }

        public void AddCurrency(int amount)
        {
            CurrencyAmount += amount;
            currencyChangedEvent?.Invoke(CurrencyAmount);
        }

        public void RemoveCurrency(int amount)
        {
            CurrencyAmount -= amount;
            currencyChangedEvent?.Invoke(CurrencyAmount);
        }
    }
}
