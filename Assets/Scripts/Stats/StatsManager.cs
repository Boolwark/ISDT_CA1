using System;
using System.Collections;
using CodeMonkey.Utils;
using DefaultNamespace.ObjectPooling;
using Effects;
using UnityEngine;
using UnityEngine.Events;
using Util;

namespace Stats
{


    public class StatsManager : MonoBehaviour
    {
        // Attributes
        private float initHP;
        public bool isPlayer = false;
        [SerializeField]private float HP = 100f;
        [SerializeField]public float Attack= 10f;
        [SerializeField]private float Speed  = 5f;
        private bool isDead;
        public float killPoints; //kill points are proportional to the amount of mana the player gets.
        public float manaMultiplier = 1 / 10f;
        private void Start()
        {
            initHP = HP;
        }

        public UnityEvent OnDamageTaken;
        public UnityEvent OnHealTaken;
        public UnityEvent OnKilled;
        // Method to take damage
        public void TakeDamage(float damageAmount)
        {
            Debug.Log(name + "has taken damage");
            OnDamageTaken?.Invoke();
            HP -= damageAmount;
            DamagePopupManager.Instance.CreatePopUp(damageAmount.ToString(),transform.position,Quaternion.identity);

            // Ensure HP doesn't go below 0
            if (HP < 0 && !isDead)
            {
                isDead = true;
                HP = 0;
                // You can add any death logic here if needed
                Debug.Log($"{gameObject.name} has died!");
                LeaderboardManager.Instance.IncrementScore(killPoints);
                var enemyType = GetEnemyType(gameObject.name);
                ExportToCSV.Instance.RecordKill(enemyType);
                if (TryGetComponent(out MeshRenderer meshRenderer))
                {
                    meshRenderer.enabled = false;
                }
                if (TryGetComponent(out SkinnedMeshRenderer skinnedMeshRenderer))
                {
                    skinnedMeshRenderer.enabled = false;
                }
                OnKilled?.Invoke();
                StartCoroutine(ReturnObjectToPool());
                if (isPlayer)
                {
                    ExportToCSV.Instance.Record();
                }

            }
        }
        private string GetEnemyType(string enemyName)
        {
            var name = gameObject.name.Split(" ")[0];
            if (name.Contains("("))
            {
                return name.Substring(0, name.IndexOf("("));
            }

            return name;
        }

        private IEnumerator ReturnObjectToPool()
        {
            if (!isPlayer)
            {
                yield return new WaitForSeconds(2f);
                ObjectPoolManager.ReturnObjectToPool(gameObject);
            }
            else
            {
                HP = initHP;
                isDead = false;
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

        public void TakeHeal(float healAmount)
        {
            Debug.Log(transform.name + "Is being healed");
            HP += healAmount;
            OnHealTaken?.Invoke();
        }

        public float GetCurrentHealth()
        {
            return HP;
        }
        public void ExplodeOnDeath()
        {
            ExplosionManager.Instance.SpawnExplosion(transform.position,transform.rotation);
        }

        public void GiveManaOnKilled()
        {
            ManaManager.Instance.ChangeMana(killPoints * manaMultiplier);
        }
    }

}