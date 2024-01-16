using System;
using Stats;
using UnityEngine;

namespace Weapons
{
    public class Bullet : MonoBehaviour
    {
        public string enemyTag = "Enemy";
        public float damage = 100f;
        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag(enemyTag) &&
                collision.collider.TryGetComponent(out StatsManager statsManager))
            {
                print($"Bullet damaging {collision.collider.name}");
                statsManager.TakeDamage(damage);
            }
        }
    }
}