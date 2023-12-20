using System;
using DefaultNamespace.ObjectPooling;
using UnityEngine;
using Util;
using System.Collections.Generic;
using CodeMonkey.Utils;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> enemyPrefabs;
        [SerializeField] private List<int> enemyCounts;
        [SerializeField] private float spawnRadius = 5f;
        [SerializeField] private float clusterOffset = 2f;
        public GameObject spawnEffect;
        public float yPos = -5.1f;
        public bool spawnOnStart = true;
        public bool spawnInfinitely = false;
        public float spawnCooldown = 20f;
        private float timeToNextSpawn = 0f;
        private int hordeSize;
        private void Start()
        {
            Debug.Log("Player chose difficulty: "+GameManager.Instance.chosenDifficulty);
            enemyCounts = new List<int>(new int[enemyPrefabs.Count]); // Initialize enemyCounts with zeros
            if (!spawnOnStart) return;
            switch (GameManager.Instance.chosenDifficulty)
            {
                case GameManager.Difficulty.EASY:
                    hordeSize = 3;
                
                    break;
                case GameManager.Difficulty.NORMAL:
                    hordeSize = 5;
                    break;
                case GameManager.Difficulty.NIGHTMARE:
                    hordeSize = 10;
                    break;
                
            }
            SpawnHorde(hordeSize);
        }

        public void SpawnHorde(int hordeSize)
        {
            timeToNextSpawn = spawnCooldown;
            for (int i = 0; i < hordeSize; i++)
            {
                int prefabIndex = Random.Range(0, enemyPrefabs.Count); // Select a random enemy prefab index
                GameObject enemyPrefab = enemyPrefabs[prefabIndex];
                Vector3 spawnPosition = GetRandomSpawnPosition();
                Quaternion spawnRotation = Quaternion.identity;
                PlaySpawnEffect();

                GameObject enemy = ObjectPoolManager.SpawnObject(enemyPrefab, spawnPosition, spawnRotation);
                enemyCounts[prefabIndex]++; // Increment the count for the spawned enemy type
                // Additional enemy setup can be done here if needed
            }
        }
        private void PlaySpawnEffect()
        {
            spawnEffect.SetActive(true);
            FunctionTimer.Create(() => { spawnEffect.SetActive(false); }, 1f);
        }
        private Vector3 GetRandomSpawnPosition()
        {
            Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
            randomDirection += transform.position;
            randomDirection.y = yPos;

            // Ensure the enemy is spawned on the ground
            if (Physics.Raycast(randomDirection + Vector3.up * 50f, Vector3.down, out RaycastHit hit, 100f, LayerMask.GetMask("Ground")))
            {
                return hit.point + Vector3.up * clusterOffset;
            }

            return randomDirection;
        }

        private void Update()
        {
            if (spawnInfinitely)
            {
                if (timeToNextSpawn <= 0f)
                {
                    SpawnHorde(hordeSize);
                }
                else
                {
                    timeToNextSpawn -= Time.deltaTime;
                }
            }
        }
    }
}
