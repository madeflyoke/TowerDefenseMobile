using PathCreation;
using System;
using System.Collections;
using System.Collections.Generic;
using TD.GamePlay.Units;
using UnityEngine;

namespace TD.GamePlay.Level
{
    public class Wave : MonoBehaviour
    {
        [SerializeField] private float waveDelay;
        [SerializeField] private List<EnemySpawnConfig> enemies;     
        public List<EnemySpawnConfig> Enemies { get => enemies; }
        public float WaveDelay { get=>waveDelay; }

        [Serializable]
        public struct EnemySpawnConfig
        {
            public GameObject prefab;
            public float spawnDelay;
        }
    }
}

