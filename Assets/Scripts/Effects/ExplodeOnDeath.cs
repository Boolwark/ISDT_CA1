using UnityEngine;

namespace Effects
{
    public class ExplodeOnDeath : MonoBehaviour
    {
        public void Activate()
        {
            ExplosionManager.Instance.SpawnExplosion(transform.position,Quaternion.identity);
        }
    }
}