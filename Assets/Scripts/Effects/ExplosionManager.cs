using DefaultNamespace.ObjectPooling;
using DG.Tweening;
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
        public float explosionDuration;

        public void SpawnExplosion(Vector3 position, Quaternion rotation)
        {
            var spawnedExplosion = ObjectPoolManager.SpawnObject(explosionPrefab, position, rotation);
            spawnedExplosion.transform.DOScale(spawnedExplosion.transform.localScale * 3f, 0.5f).SetEase(Ease.OutExpo);
            AudioManager.Instance.PlaySFX("Explosion");
            Destroy(spawnedExplosion,explosionDuration);
        }
    }
}