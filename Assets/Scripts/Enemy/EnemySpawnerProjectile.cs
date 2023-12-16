using System;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawnerProjectile : MonoBehaviour
    {
        public EnemySpawner enemySpawner;
        public int hordeSize=5;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                enemySpawner.SpawnHorde(hordeSize);
            }
        }
    }
}