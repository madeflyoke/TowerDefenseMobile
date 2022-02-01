using UnityEngine;
using System;
using TD.GamePlay.HomeBuilding;
using UnityEngine.SceneManagement;
namespace TD.GamePlay.Managers
{
    public class GameManager : MonoBehaviour
    {
        public event Action<int> currencyChangedEvent;
        public event Action endGameEvent;
        public event Action restartLevelEvent;

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

        public void ResetLevel()
        {
            if (SceneManager.GetActiveScene().buildIndex!=10) //set 0 index to main menu!!!
            {            
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                restartLevelEvent?.Invoke();
            }        
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
