using System;
using Stats;
using UnityEngine;

namespace Effects
{
    public class Fire : MonoBehaviour
    {
        public string enemyTag = "Player";
        public float damageRate = 100f;
        private void OnCollisionStay(Collision collision)
        {
            if (collision.collider.CompareTag(enemyTag))
            {
                if (collision.collider.TryGetComponent(out StatsManager sm))
                {
                    sm.TakeDamage(100f * Time.deltaTime);
                }
            }
        }
    }
}