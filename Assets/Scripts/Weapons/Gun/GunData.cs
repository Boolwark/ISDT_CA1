using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "Weapons",menuName = "Gun")]
    public class GunData : ScriptableObject
    {
        public int magazineSize = 30;
        public float fireRate = 0.1f;
        public float muzzleDuration = 0.5f;
        public float reloadDuration = 2f;
        public float shotsPerFire = 5;
        public int nClips = 3;
        public float damage;
        public float recoilStrength = 1f;
        public int nVibrations = 2;
        public float shakeDuration = 2f;
        public GameObject bullet;
        
        public float fireSpeed = 20f;
        public float recoilRecoverySpeed = 0.8f;
        public float maxSpreadTime = 1f;
        [SerializeField] public Vector3 Spread = new Vector3(.1f, .1f, .1f); 
    
    }
}