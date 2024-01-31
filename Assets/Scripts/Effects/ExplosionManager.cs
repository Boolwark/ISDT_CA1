using DefaultNamespace.ObjectPooling;
using DG.Tweening;
using Stats;
using UnityEngine;
using Util;

namespace Effects
{
    /// <summary>
    /// A singleton manager for managing the explosions.
    /// </summary>
    public class ExplosionManager : Singleton<ExplosionManager>
    {
        public GameObject explosionPrefab;
        public float explosionDuration,blastRadius=5f,explosionDamage=100f;
        public LayerMask whatIsEnemy;

        public void SpawnExplosion(Vector3 position, Quaternion rotation)
        {
            var spawnedExplosion = ObjectPoolManager.SpawnObject(explosionPrefab, position, rotation);
            spawnedExplosion.transform.DOScale(spawnedExplosion.transform.localScale * 3f, 0.5f).SetEase(Ease.OutExpo);
            AudioManager.Instance.PlaySFX("Explosion");
           

            DamageEnemies();
            Destroy(spawnedExplosion,explosionDuration);
        }

        private void DamageEnemies()
        {
            foreach (var enemy in Physics.OverlapSphere(transform.position,blastRadius,whatIsEnemy))
            {
                if (enemy.TryGetComponent(out StatsManager sm))
                {
                    sm.TakeDamage(explosionDamage);
                }
            }
        }
        public void SpawnExplosionWithPrefab(Vector3 position, Quaternion rotation,GameObject effectPf)
        {
            var spawnedExplosion = ObjectPoolManager.SpawnObject(effectPf, position, rotation);
            spawnedExplosion.transform.DOScale(spawnedExplosion.transform.localScale * 3f, 0.5f).SetEase(Ease.OutExpo);
            AudioManager.Instance.PlaySFX("Explosion");
            DamageEnemies();
            Destroy(spawnedExplosion,explosionDuration);
        }
    }
}