using UnityEngine;

namespace Enemy.EnemyStats
{
    [CreateAssetMenu(fileName = "EnemyStats", menuName = "Enemy/EnemyStats")]
    public class EnemyStats : ScriptableObject
    {
        public string enemyName;
        public int health;
        public int defense;
        // Add other relevant stats such as attack, movement speed, etc.
    }
}