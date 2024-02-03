using System;
using DefaultNamespace.ObjectPooling;
using Stats;
using UnityEngine;
using UnityEngine.Events;
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
            nCurrentEnemiesInWave = nEnemiesToSpawn;
            Vector2 offset = Random.insideUnitCircle;
            for (int i = 0; i < nEnemiesToSpawn; i++)
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