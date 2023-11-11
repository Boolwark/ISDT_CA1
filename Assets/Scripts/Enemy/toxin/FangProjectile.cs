using System;
using Stats;
using UnityEngine;

namespace Enemy.toxin
{
    public class FangProjectile : MonoBehaviour
    {
        public float damage;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (other.TryGetComponent(out StatsManager sm))
                {
                    sm.TakeDamage(damage);
                }
            }
        }
    }
}