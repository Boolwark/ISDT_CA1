using UnityEngine;

namespace GOAP.config
{
    [CreateAssetMenu(menuName="AI/Attack Config",fileName="Attack Config",order = 1)]
    public class AttackConfigSO : ScriptableObject
    {
        public float SensorRadius = 10;
        public float MeleeAttackRadius = 1f;
        public int MeleeAttackCost = 1;
        public float AttackDelay = 1;
        public LayerMask AttackableLayerMask;

    }
}