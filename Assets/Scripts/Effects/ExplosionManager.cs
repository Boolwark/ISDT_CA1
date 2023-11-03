using DefaultNamespace.ObjectPooling;
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
            Destroy(spawnedExplosion,explosionDuration);
            
        }
    }
}