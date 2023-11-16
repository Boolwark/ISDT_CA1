using DefaultNamespace.ObjectPooling;
using UnityEngine;
using UnityEngine.Events;

namespace Stats
{


    public class StatsManager : MonoBehaviour
    {
        // Attributes
        [SerializeField]private float HP = 100f;
        [SerializeField]public float Attack= 10f;
        [SerializeField]private float Speed  = 5f;
        public float killPoints;
        
        public UnityEvent OnDamageTaken;

        public UnityEvent OnKilled;
        // Method to take damage
        public void TakeDamage(float damageAmount)
        {
            OnDamageTaken?.Invoke();
            HP -= damageAmount;

            // Ensure HP doesn't go below 0
            if (HP < 0)
            {
                HP = 0;
                // You can add any death logic here if needed
                Debug.Log($"{gameObject.name} has died!");
                LeaderboardManager.Instance.IncrementScore(killPoints);
                ObjectPoolManager.ReturnObjectToPool(gameObject);
            }
        }

        // Method to change speed
        public void ChangeSpeed(float speedChange)
        {
            Speed += speedChange;

            // Ensure Speed doesn't go below a certain threshold, e.g., 0
            if (Speed < 0)
            {
                Speed = 0;
            }
        }

        // Method to change attack
        public void ChangeAttack(float attackChange)
        {
            Attack += attackChange;

            // Ensure Attack doesn't go below 0
            if (Attack < 0)
            {
                Attack = 0;
            }
        }
    }

}