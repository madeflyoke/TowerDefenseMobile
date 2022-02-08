using System.Collections.Generic;
using TD.GamePlay.Level;
using TD.GamePlay.Units;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;
using System.Threading;
using System;
using PathCreation;

namespace TD.GamePlay.Managers
{
    public class WavesSpawner : MonoBehaviour
    {
        [Inject] private GameManager gameManager;
        [Inject] private Pooler pooler;

        [Serializable]
        public struct PathWaves
        {
            public int delay;
            public PathCreator path;
            public List<Wave> waves;
            public int currentWaveIndex { get; set; }
        }

        public event Action wavesEndEvent;
        [SerializeField] private List<PathWaves> pathWaves;
        private CancellationTokenSource cancellationToken;
        public List<PathWaves> PathWavesList { get => pathWaves; }
        private int enemiesNumber;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                cancellationToken.Cancel();
            }
        }

        private void OnEnable()
        {
            gameManager.startLevelEvent += Initialize;
            gameManager.endGameEvent += StopWaves;
            gameManager.restartLevelEvent += Initialize;
        }
        private void OnDisable()
        {
            gameManager.startLevelEvent -= Initialize;
            gameManager.endGameEvent -= StopWaves;
            gameManager.restartLevelEvent -= Initialize;
            if (cancellationToken!=null)
            {
                cancellationToken.Cancel();
                cancellationToken = null;
            }

        }

        public void StartWaves()
        {
            for (int i = 0; i < pathWaves.Count; i++)
            {
                PushEnemy(pathWaves[i]);
            }
        }
        private void StopWaves()
        {
            cancellationToken.Cancel();
        }

        private void Initialize()
        {
            cancellationToken = new CancellationTokenSource();
            CountEnemies();
        }

        private void CheckEnemiesDeath(BaseUnit unit)
        {
            enemiesNumber--;
            unit.enemyDieEvent -= CheckEnemiesDeath;
            if (enemiesNumber<=0)
            {
                wavesEndEvent?.Invoke();
            }
        }

        private async void PushEnemy(PathWaves pathWaves)
        {
            if (pathWaves.currentWaveIndex==0)
            {
                await UniTask.Delay(pathWaves.delay * 1000, cancellationToken: cancellationToken.Token);
            }
            for (int i = 0; i < pathWaves.waves[pathWaves.currentWaveIndex].Enemies.Count; i++)
            {
                var enemyConfig = pathWaves.waves[pathWaves.currentWaveIndex].Enemies[i];
                await UniTask.Delay((int)enemyConfig.spawnDelay * 1000,cancellationToken: cancellationToken.Token);
                GameObject enemy = pooler.GetObjectFromPool(enemyConfig.prefab, pathWaves.path.bezierPath.GetPoint(0));
                BaseUnit unit = enemy.GetComponent<BaseUnit>();
                unit.enemyDieEvent += CheckEnemiesDeath;
                unit.pathFollower.pathCreator = pathWaves.path;
                unit.Move(); 
            }
            NextWaveSpawn(pathWaves);
        }

        private async void NextWaveSpawn(PathWaves pathWaves)
        {
            pathWaves.currentWaveIndex++;
          
            if (pathWaves.currentWaveIndex > pathWaves.waves.Count - 1)
            {
                return;
            }
            await UniTask.Delay((int)pathWaves.waves[pathWaves.currentWaveIndex].WaveDelay * 1000,cancellationToken:cancellationToken.Token);
            PushEnemy(pathWaves);
        }

        private void CountEnemies()
        {
            foreach (var pathWave in pathWaves)
            {
                foreach (var enemies in pathWave.waves)
                {
                    if (enemies != null)
                    {
                        enemiesNumber += enemies.Enemies.Count;
                    }
                }
            }
        }
    }
}

