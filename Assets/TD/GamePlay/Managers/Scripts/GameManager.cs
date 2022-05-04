using UnityEngine;
using System;
using TD.GamePlay.HomeBuilding;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using TD.GUI.Buttons;
using TD.GUI.Screens.EndGame.Buttons;
using TD.GUI.Screens.GamePlay.HUD;
using TD.GUI.Screens.MainMenu.Buttons;
using TD.Ad;
using Zenject;

namespace TD.GamePlay.Managers
{
    public class GameManager : MonoBehaviour
    {
        [Inject] public AdManager adManager { get; private set; }
        
        public event Action launchGameStateEvent;
        public event Action startLevelEvent;
        public event Action endGameEvent;
        public event Action restartLevelEvent;
        public event Action<int> currencyChangedEvent;

        [SerializeField] private int targetFPS;
        [SerializeField] private int startCurrencyAmount;
        [SerializeField] private float sellMultiplier;

        public int StartCurrencyAmount { get => startCurrencyAmount; }
        public int CurrencyAmount { get; private set; }
        public float SellMultiplier { get => sellMultiplier; }
        public HomeBase homeBase { get; private set; }
        private WavesSpawner wavesSpawner;

        private void Awake()
        {
            Application.targetFrameRate = targetFPS;
        }

        private void Start()
        {
            adManager.Initialize();
            LaunchGameState();
        }

        private void GamePlayInitialize()
        {
            homeBase = FindObjectOfType<HomeBase>();
            wavesSpawner = FindObjectOfType<WavesSpawner>();
            homeBase.homeBaseDestroyedEvent -= EndGameLogic;
            wavesSpawner.wavesEndEvent -= EndGameLogic;
            CurrencyAmount = startCurrencyAmount;
            homeBase.homeBaseDestroyedEvent += EndGameLogic;
            wavesSpawner.wavesEndEvent += EndGameLogic;
        }

        public void CheckButtonCall(BaseButton button)
        {
            if (button is QuitButton)
            {
                LaunchGameState();
            }
            else if (button is RetryButton)
            {
                ResetLevel();
            }
            else if (button is StartGameButton)
            {
                StartLevel(1);
            }
        }

        private async void LaunchGameState()
        {
            if (SceneManager.GetActiveScene().buildIndex!=0)
            {
                await SceneManager.LoadSceneAsync(0);
            }
            launchGameStateEvent?.Invoke();
        }

        private async void ResetLevel()
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                await SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                restartLevelEvent?.Invoke();
                GamePlayInitialize();
                adManager.ShowInterstitial();
            }
        }

        private async void StartLevel(int index)
        {
            if (index != 0)
            {
                await SceneManager.LoadSceneAsync(index);
                startLevelEvent?.Invoke();
                GamePlayInitialize();
            }
        }

        private void EndGameLogic()
        {
            endGameEvent?.Invoke();
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
