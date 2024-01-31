using UnityEngine;

namespace Dragon
{
        [CreateAssetMenu(menuName = "Dragon",fileName ="Dragon Config")]
    public class DragonConfig : ScriptableObject
    {
        [Header("Basic Stats")]
        public float health = 100f;
        public float damage = 10f;

        [Header("Movement")]
        public float movementSpeed = 5f;
        public float rotationSpeed = 3f;

        [Header("Detection")]
        public float detectionRange = 10f;

        public float fovAngle=200f;
        [Header("Attack")]
        public float meleeRange = 5f;

        public float shootingDamage = 10f;
        public float meleeDamage = 10f;
        public float shootingRange = 20f;
        public float attackCooldown = 2f;
    }
}