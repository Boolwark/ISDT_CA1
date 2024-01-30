using System;
using DG.Tweening;
using UnityEngine;

namespace NPC
{
    public class MechAlly : MonoBehaviour
    {
        public Transform player;
        public float followSpeed = 5f;
        public float rotationSpeed = 10f;
        public float detectionRadius = 10f;
        public float meleeRange = 2f;
        public ElectroSpear Spear;
        private bool inMeleeRange;
        private bool usingShield;
        private int shotsFired;
        public LayerMask whatIsEnemy;
        private Transform currentTarget;

        private void Start()
        {
            player = FindObjectOfType<Player.Player>().transform;
        }

        void Update()
        {
            FollowPlayer();
            CheckForEnemies();
            AttackLogic();
            ShieldLogic();
        }

        private void FollowPlayer()
        {
            Vector3 offset = new Vector3(0f, 0f, -5f);
            Vector3 targetPosition = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);

            Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        private void CheckForEnemies()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, whatIsEnemy);
            float closestDistance = float.MaxValue;

            inMeleeRange = false;

            foreach (var collider in hitColliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, collider.transform.position);

                    if (distanceToEnemy <= meleeRange)
                    {
                        inMeleeRange = true;
                        break;
                    }

                    if (distanceToEnemy < closestDistance)
                    {
                        closestDistance = distanceToEnemy;
                        currentTarget = collider.transform;
                    }
                }
            }
        }


        private void AttackLogic()
        {
            if (inMeleeRange && currentTarget != null)
            {
                MeleeAttack();
            }
            else
            {
                ShootingAttack();
            }
        }

        private void MeleeAttack()
        {
            Spear.MeleeAttack();
        }

        private void ShootingAttack()
        {
            if (shotsFired < 5)
            {
                Spear.RangedAttack();
                shotsFired++;
            }
            else
            {
                CloseInAndMelee();
            }
        }

        private void CloseInAndMelee()
        {
            if (currentTarget != null)
            {
                transform.DOMove(currentTarget.transform.position, 2f).SetEase(Ease.InBounce);
                MeleeAttack();
                shotsFired = 0;     
            }
           
        }

        private void ShieldLogic()
        {
            if (inMeleeRange && !usingShield)
            {
                ActivateShield();
            }
            else if (!inMeleeRange && usingShield)
            {
                DeactivateShield();
            }
        }

        private void ActivateShield()
        {
        
            usingShield = true;
            Spear.ActivateShield();
        }

        private void DeactivateShield()
        {
   
            usingShield = false;
        }
    }
}
