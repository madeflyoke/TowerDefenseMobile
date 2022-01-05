using System.Collections.Generic;
using TD.GamePlay.Level;
using TD.GamePlay.Units;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;
using System.Threading;

namespace TD.GamePlay.Managers
{
    public class WavesSpawner : MonoBehaviour
    {
        [Inject] private GameManager gameManager;
        [Inject] private Pooler pooler;

        [SerializeField] private List<Wave> waves;
        private int currentWaveIndex;
        private CancellationTokenSource cancellationToken;

        private void Awake()
        {
            cancellationToken = new CancellationTokenSource();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                PushEnemy();
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                cancellationToken.Cancel();
            }
        }

        private void OnEnable()
        {
            gameManager.endGameEvent += cancellationToken.Cancel;
        }
        private void OnDisable()
        {
            gameManager.endGameEvent -= cancellationToken.Cancel;
        }

        private async void PushEnemy()
        {
            for (int i = 0; i < waves[currentWaveIndex].Enemies.Count; i++)
            {
                var enemyConfig = waves[currentWaveIndex].Enemies[i];
                await UniTask.Delay((int)enemyConfig.spawnDelay * 1000,cancellationToken: cancellationToken.Token);
                GameObject enemy = pooler.GetObjectFromPool(enemyConfig.prefab, enemyConfig.spawnPosition.position);
                enemy.GetComponent<BaseUnit>().Move(); ///!!!
            }
            NextWaveSpawn();
        }

        private async void NextWaveSpawn()
        {
            currentWaveIndex++;
            if (currentWaveIndex > waves.Count - 1)
            {
                Debug.Log("END WAVES");
                return;
            }
            await UniTask.Delay((int)waves[currentWaveIndex].WaveDelay * 1000,cancellationToken:cancellationToken.Token);
            PushEnemy();
        }
    }
}

