using UnityEngine;

namespace GOAP.config
{
    [CreateAssetMenu(menuName="AI/Healing Config",fileName="Heal Config",order = 3)]
    public class HealingConfigSO : ScriptableObject
    {
        public GameObject HealEffectPrefab;
        public float EffectDuration=3f;
        public float HPRestorationRate = 1f;
        public float AcceptableHPLimit = 50f; // Once enemy is below or equal this HP, it will heal itself.
    }
}