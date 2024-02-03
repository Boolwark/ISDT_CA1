using UnityEngine;

namespace NPC
{
    [CreateAssetMenu(menuName = "Mech Ally",fileName = "Mech Ally Config")]
    public class MechAllyConfig : ScriptableObject
    {
        public float followSpeed = 5f;
        public float rotationSpeed = 10f;
        public float detectionRadius = 10f;
        public float meleeRange = 2f;
        public Vector3 followOffset;
    }
}