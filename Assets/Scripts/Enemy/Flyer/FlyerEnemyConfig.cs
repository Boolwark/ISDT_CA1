using UnityEngine;

namespace Enemy.Flyer
{
    [CreateAssetMenu(menuName = "Enemy Config",fileName = "Flying Enemy Config")]
    public class FlyerEnemyConfig : ScriptableObject
    {
        public GameObject bullet;
        public bool hasKinematicProjectile;
        public float sightRange, attackRange;
        public float baseOffset;
    }
}