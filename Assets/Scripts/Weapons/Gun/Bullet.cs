using System;
using Stats;
using UnityEngine;

namespace Weapons
{
    public class Bullet : MonoBehaviour
    {
        public float damage = 100f;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Enemy") &&
                collision.collider.TryGetComponent(out StatsManager statsManager))
            {
                print($"Bullet damaging {collision.collider.name}");
                statsManager.TakeDamage(damage);
            }
        }
    }
}