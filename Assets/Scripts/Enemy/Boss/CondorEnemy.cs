using DefaultNamespace.ObjectPooling;
using DG.Tweening;
using Enemy.Boss;
using UnityEngine;

namespace Enemy
{
    public class CondorEnemy : MonoBehaviour
    {
        private Transform player;
        public GameObject projectile;
        public float shootingInterval = 2f;
        public float diveBombDistance = 10f;
        public float speed = 5f;
        public float rotationSpeed = 10f;
        public float moveTowardsPlayerSpeed = 3f;
        public float barrelRollInterval = 5f;
        public float strafingDistance = 10f;
        public Transform fireRocketPoint;
        public float offset = 5f;

        private float timeSinceLastShot = 0f;
        private float timeSinceLastBarrelRoll = 0f;
        private bool isStrafing = false;
        private float strafeDirection = 1f;  // 1 or -1 for direction


     
        public float homingMissileCooldown = 5f;
        private float timeSinceLastHomingMissile = 0f;
        
        private void Start()
        {
            player = GameObject.FindWithTag("Player").transform;
        }

        void Update()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer > offset)
            {
                MoveTowardsPlayer();
                isStrafing = false;
            }
            else if (distanceToPlayer > offset)
            {
                if (!isStrafing)
                {
                    StartStrafing();
                }
                StrafeAroundPlayer(distanceToPlayer);
            }
            else
            {
                isStrafing = false;
            }

            HandleShooting(distanceToPlayer);
            HandleDiveBomb(distanceToPlayer);
            HandleHomingMissile();
            HandleBarrelRoll();
        }

        void MoveTowardsPlayer()
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveTowardsPlayerSpeed * Time.deltaTime);
        }

        void StartStrafing()
        {
            isStrafing = true;
            strafeDirection = Random.value > 0.5f ? 1f : -1f;  // Randomize strafing direction
        }

        void StrafeAroundPlayer(float distance)
        {
            transform.RotateAround(player.position, Vector3.up, rotationSpeed * strafeDirection * Time.deltaTime);

            // Randomize strafing direction occasionally
            if (Random.value < 0.1f)  // 10% chance to change direction
            {
                strafeDirection *= -1f;
            }
        }

        void HandleShooting(float distance)
        {
            if (distance < shootingInterval && timeSinceLastShot > shootingInterval)
            {
                ShootAtPlayer();
                timeSinceLastShot = 0f;
            }
            timeSinceLastShot += Time.deltaTime;
        }

        void ShootAtPlayer()
        {
            Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

            GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * speed;
        }

        void HandleDiveBomb(float distance)
        {
            if (distance < diveBombDistance)
            {
                DiveBomb();
            }
        }

        void DiveBomb()
        {
            Vector3 diveDirection = (player.position - transform.position).normalized;
            transform.position += diveDirection * speed * Time.deltaTime;
        }
        void HandleHomingMissile()
        {
            if (timeSinceLastHomingMissile > homingMissileCooldown)
            {
                HomingRocketManager.Instance.FireRocket(fireRocketPoint.position,fireRocketPoint.rotation);
                timeSinceLastHomingMissile = 0f;
            }
            timeSinceLastHomingMissile += Time.deltaTime;
        }
      

        void HandleBarrelRoll()
        {
            if (timeSinceLastBarrelRoll > barrelRollInterval)
            {
                DoBarrelRoll();
                timeSinceLastBarrelRoll = 0f;
            }
            timeSinceLastBarrelRoll += Time.deltaTime;
        }

        void DoBarrelRoll()
        {
            transform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360)
                .SetRelative()
                .SetEase(Ease.Linear);
        }
    }
}
