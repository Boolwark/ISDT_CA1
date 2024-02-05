using System;
using DefaultNamespace.ObjectPooling;
using Stats;
using UnityEngine;
using UnityEngine.Events;
using Util;
using Random = UnityEngine.Random;

namespace Enemy.Walker
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        public bool isFirstEnemySpawner = false;
        public float offsetX,offsetY;
        public WaveData waveData;
        public GameObject walkerPf;
        public UnityEvent OnAllEnemiesKilled;
        private int nCurrentEnemiesInWave;

        private void Start()
        {
            if (isFirstEnemySpawner)
            {
                SpawnEnemies();
            }
        }

        public void SpawnEnemies()
        {
            int nEnemiesToSpawn = Random.Range(waveData.minPerCluster, waveData.maxPerCluster);
            int enemyMultiplier=1;
            switch (GameManager.Instance.chosenDifficulty)
            {
                case GameManager.Difficulty.EASY:
                    enemyMultiplier = 1;
                
                    break;
                case GameManager.Difficulty.NORMAL:
                    enemyMultiplier = 5;
                    break;
                case GameManager.Difficulty.NIGHTMARE:
                    enemyMultiplier = 10;
                    break;
                
            }
            nCurrentEnemiesInWave = (int)nEnemiesToSpawn * enemyMultiplier;
            Vector2 offset = Random.insideUnitCircle;
            for (int i = 0; i < nCurrentEnemiesInWave; i++)
            {
               var enemy  = ObjectPoolManager.SpawnObject(walkerPf, transform.position + new Vector3(offset.x * offsetX,0,offset.y*offsetY), Quaternion.identity)
                    ;
                enemy.GetComponent<StatsManager>().OnKilled.AddListener(DecrementCurrentEnemyCounter);
            }
        }

        public void DecrementCurrentEnemyCounter()
        {
            nCurrentEnemiesInWave--;
            if (nCurrentEnemiesInWave == 0)
            {
                OnAllEnemiesKilled?.Invoke();
            }
        }
    }
}