using System;
using System.Collections;
using System.Collections.Generic;
using TD.GamePlay.Units;
using UnityEngine;

namespace TD.GamePlay.Level
{
    public class Wave : MonoBehaviour
    {
        [SerializeField] private int waveIndex;
        [SerializeField] private List<EnemySpawnConfig> enemies;
        [SerializeField] private float waveDelay;
        public List<EnemySpawnConfig> Enemies { get => enemies; }
        public float WaveDelay { get=>waveDelay;  }

        [Serializable]
        public struct EnemySpawnConfig
        {
            public GameObject prefab;
            public float spawnDelay;
            public Transform spawnPosition;
        }
    }
}

