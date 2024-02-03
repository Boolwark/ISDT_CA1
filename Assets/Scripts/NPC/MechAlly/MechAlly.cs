using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace NPC
{
    public class MechAlly : MonoBehaviour
    {
        private Transform player;
        public MechAllyConfig MechAllyConfig;
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
          
            Vector3 targetPosition = player.position + MechAllyConfig.followOffset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * MechAllyConfig.followSpeed);

            Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * MechAllyConfig.rotationSpeed);
        }
        private void FaceTarget(Transform target)
        {
            if (target == null) return;
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        private void CheckForEnemies()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, MechAllyConfig.detectionRadius, whatIsEnemy);
            float closestDistance = float.MaxValue;

            inMeleeRange = false;

            foreach (var collider in hitColliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, collider.transform.position);

                    if (distanceToEnemy <= MechAllyConfig.meleeRange)
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
            FaceTarget(currentTarget);
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
