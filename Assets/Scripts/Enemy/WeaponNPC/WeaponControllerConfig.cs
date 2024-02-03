using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(menuName = "Weapon Controller Config",fileName = "Weapon Controller")]
    public class WeaponControllerConfig : ScriptableObject
    {
        public float snapRadius;
        public float range;
        public LayerMask whatIsWeaponController;
        public LayerMask whatIsEnemy;
    }
}