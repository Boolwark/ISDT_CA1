using DefaultNamespace.ObjectPooling;
using UnityEngine;
using Util;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private int hordeSize = 10;
        [SerializeField] private float spawnRadius = 5f;
        [SerializeField] private float clusterOffset = 2f;
        public float yPos = -5.1f;

        private void Start()
        {
            Debug.Log("Player chose difficulty: "+GameManager.Instance.chosenDifficulty);
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
            SpawnHorde();
        }

        public void SpawnHorde()
        {
            for (int i = 0; i < hordeSize; i++)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                Quaternion spawnRotation = Quaternion.identity;

                GameObject enemy = ObjectPoolManager.SpawnObject(enemyPrefab, spawnPosition, spawnRotation);
                // Additional enemy setup can be done here if needed
            }
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
    }
}