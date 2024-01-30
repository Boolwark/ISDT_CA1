using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.ObjectPooling;
using Effects;
using Stats;
using UnityEngine;

namespace Weapons
{
    public class Bullet : MonoBehaviour
    {
        public List<string> enemyTags=new();
        public float damage = 100f;
        public float liveTime = 5f;
        public GameObject effectPf = null;
        
        private void Start()
        {
            Invoke("SelfDestruct",liveTime);
        }
        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (HitValidTarget(collision.gameObject) &&
                collision.collider.TryGetComponent(out StatsManager statsManager))
            {
                print($"Bullet damaging {collision.collider.name}");
                statsManager.TakeDamage(damage);
            }
        }

        private void SelfDestruct()
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
            if (effectPf == null)
            {
                ExplosionManager.Instance.SpawnExplosion(transform.position,Quaternion.identity);
            }
            else
            {
                ExplosionManager.Instance.SpawnExplosionWithPrefab(transform.position,Quaternion.identity,effectPf);
            }
           
        }

        private bool HitValidTarget(GameObject target)
        {
            foreach (string validTag in enemyTags)
            {
                if (target.CompareTag(validTag)) return true;
            }

            return false;
        }
        
    }
}