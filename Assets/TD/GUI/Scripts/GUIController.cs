using TD.GUI.Screens;
using TD.GUI.Screens.EndGame;
using TD.GUI.Screens.GamePlay;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Zenject;
using TD.GamePlay.Managers;

namespace TD.GUI
{
    public class GUIController : MonoBehaviour
    {
        [Inject] private GameManager gameManager;
        public GamePlayScreen gamePlayScreen { get;private set; }
        public EndGameScreen endGameScreen { get; private set; }
        private List<BaseScreen> screens;

        private void Awake()
        {
            screens = new List<BaseScreen>();
            gamePlayScreen = GetComponentInChildren<GamePlayScreen>();
            screens.Add(gamePlayScreen);
            endGameScreen = GetComponentInChildren<EndGameScreen>();
            screens.Add(endGameScreen);              
        }

        private void Start()
        {
            StartGameUI();
        }

        private void OnEnable()
        {
            gameManager.endGameEvent += EndGameUI;
            gameManager.restartLevelEvent += RestartLevelUI;
        }
        private void OnDisable()
        {
            gameManager.endGameEvent -= EndGameUI;
            gameManager.restartLevelEvent -= RestartLevelUI;
        }

        private void StartGameUI()
        {
            ShowScreen(gamePlayScreen);
        }

        private void EndGameUI()
        {
            ShowScreen(endGameScreen);
        }

        private void RestartLevelUI()
        {
            gamePlayScreen.ResetScreen();
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

