using System.Collections.Generic;
using TD.GamePlay.Level;
using TD.GamePlay.Units;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;

namespace TD.GamePlay.Managers
{
    public class WavesSpawner : MonoBehaviour
    {
        [Inject] private Pooler pooler;

        [SerializeField] private List<Wave> waves;
        private int currentWaveIndex;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                PushEnemy();
                Debug.Log("push");
            }
        }

        private async void PushEnemy()
        {
            for (int i = 0; i < waves[currentWaveIndex].Enemies.Count; i++)
            {
                var enemyConfig = waves[currentWaveIndex].Enemies[i];
                await UniTask.Delay((int)enemyConfig.spawnDelay * 1000);
                Debug.Log(i + " index");
                GameObject enemy = pooler.GetObjectFromPool(enemyConfig.prefab, enemyConfig.spawnPosition.position);
                enemy.GetComponent<BaseUnit>().Move(); ///!!!
            }
            Debug.Log("end enemies wave");
            NextWaveSpawn();
        }

        private async void NextWaveSpawn()
        {
            currentWaveIndex++;
            if (currentWaveIndex>waves.Count-1)
            {              
                return;
            }
            await UniTask.Delay((int)waves[currentWaveIndex].WaveDelay * 1000);
            PushEnemy();
        }
    }
}

