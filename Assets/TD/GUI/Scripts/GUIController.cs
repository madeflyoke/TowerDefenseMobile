using TD.GUI.Screens;
using TD.GUI.Screens.EndGame;
using TD.GUI.Screens.GamePlay;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TD.GamePlay.Managers;
using TD.GUI.Screens.MainMenu;
using TD.Services.Firebase;

namespace TD.GUI
{
    public class GUIController : MonoBehaviour
    {
        [Inject] private GameManager gameManager;
        [Inject] private AnalyticsManager analyticsManager;

        [SerializeField] private MainMenuScreen mainMenuScreen;
        [SerializeField] private GamePlayScreen gamePlayScreen;
        [SerializeField] private EndGameScreen endGameScreen;
        
        public MainMenuScreen MainMenuScreen { get => mainMenuScreen; }
        public GamePlayScreen GamePlayScreen { get=>gamePlayScreen;}
        public EndGameScreen EndGameScreen { get=>endGameScreen;}
        private List<BaseScreen> screens;

        private void Awake()
        {
            screens = new List<BaseScreen>();
            screens.Add(MainMenuScreen);
            screens.Add(GamePlayScreen);
            screens.Add(EndGameScreen);
        }

        private void OnEnable()
        {
            gameManager.launchGameStateEvent += LaunchGameUI;
            gameManager.startLevelEvent += StartGameUI;
            gameManager.endGameEvent += EndGameUI;
            gameManager.restartLevelEvent += RestartLevelUI;
        }
        private void OnDisable()
        {
            gameManager.launchGameStateEvent -= LaunchGameUI;
            gameManager.startLevelEvent -= StartGameUI;
            gameManager.endGameEvent -= EndGameUI;
            gameManager.restartLevelEvent -= RestartLevelUI;
        }

        private void LaunchGameUI()
        {
            ShowScreen(MainMenuScreen);
        }

        private void StartGameUI()
        {
            ShowScreen(GamePlayScreen);
            GamePlayScreen.Initialize();
        }

        private void EndGameUI()
        {
            GamePlayScreen.ResetScreen();
            ShowScreen(EndGameScreen);
        }

        private void RestartLevelUI()
        {
            if (endGameScreen.gameObject.activeInHierarchy==false) //analytics logic
            {
                analyticsManager.SendEvent(LogEventName.SettingsRetryButton);
            }
            GamePlayScreen.ResetScreen();
            GamePlayScreen.Initialize();
            ShowScreen(GamePlayScreen);
        }

        private void ShowScreen(BaseScreen screen)
        {
            foreach (var item in screens)
            {
                item.Hide();
                if (item == screen)
                {
                    item.Show();
                }
            }
        }
    }

}

