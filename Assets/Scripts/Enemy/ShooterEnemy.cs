using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Enemy
{
    /// <summary>
    /// This enemy AI is a basic shooter that chases the player and fires projectiles at it. It patrols randomly when
    /// the player goes out of sight. 
    /// </summary>
    public class ShooterEnemy : MonoBehaviour
    {
        private NavMeshAgent agent;
        public Transform player;

        public GameObject projectile;

        public LayerMask whatIsGround, whatIsPlayer;

        // Patroling
        public Vector3 walkPoint;
        bool walkPointSet;

        public float walkPointRange;

        //Attacking
        public float timeBetweenAttacks;
        private bool alreadyAttacked;

        //States
        public float sightRange, attackRange;
        public bool playerInSightRange, playerInAttackRange;

        void Awake()
        {
            player = GameObject.Find("Player").transform;
            agent = GetComponent<NavMeshAgent>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            if (!playerInSightRange && !playerInAttackRange)
            {
                Patroling();
            }

            if (playerInSightRange && !playerInAttackRange)
            {
                ChasePlayer();
            }

            if (playerInAttackRange && playerInSightRange)
            {
                AttackPlayer();
            }
        }

        private void SearchWalkPoint()
        {
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y,
                transform.position.z + randomZ);
            if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            {
                walkPointSet = true;
            }
        }


        private void Patroling()
        {
            if (!walkPointSet) SearchWalkPoint();
            if (walkPointSet)
            {
                agent.SetDestination(walkPoint);
            }

            Vector3 distanceToWalkPoint = walkPoint - transform.position;
            if (distanceToWalkPoint.magnitude <= 1f)
            {
                walkPointSet = false;
            }
        }

        private void ChasePlayer()
        {
            agent.SetDestination(player.transform.position);
        }

        private void AttackPlayer()
        {
            //make sure the enemy does not move
            agent.SetDestination(transform.position);
            transform.LookAt(player);
            if (!alreadyAttacked)
            {
                //attacking code here
                var rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                rb.AddForce(transform.up * 8f, ForceMode.Impulse);

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }

        private void ResetAttack()
        {
            alreadyAttacked = false;
        }

    }
}
