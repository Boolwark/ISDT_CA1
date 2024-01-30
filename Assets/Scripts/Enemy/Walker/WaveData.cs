using UnityEngine;

namespace Enemy.Walker
{
    [CreateAssetMenu(menuName = "Wave Data",fileName = "wave data")]
    public class WaveData : ScriptableObject
    {
        public float healthScale;
        public float attackScale;
        public int minPerCluster, maxPerCluster;
    }
}