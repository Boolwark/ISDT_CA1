using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyAI : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Animator animator;

    public float chaseRange = 5.0f;
    public float attackRange = 1.5f;
    public float fovAngle = 60f;
    public float patrolRadius = 10f;
    public Vector3 offset = new Vector3(1f, 0, 1f);
    public float rotationSpeed = 30f; // Degrees per second
    private float rotationTimer = 0f;
    public float rotationDuration = 3f;
    private Vector3 lastPosition;
    private float stationaryTimeThreshold = 3f;
    private float timeSinceLastMove;


    private enum State
    {
        Idle,
        Patrol,
        Chase,
        Attack
    }

    private State state;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        lastPosition = transform.position;
        timeSinceLastMove = 0f;

        state = State.Idle;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                Idle();
                break;
            case State.Patrol:
                animator.SetBool("isWalking",true);
                Patrol();
                CheckIfStuck();
                break;
            case State.Chase:
                ChasePlayer();
                animator.SetBool("isWalking",true);
                break;
            case State.Attack:
                AttackPlayer();
                break;
        }
    }

    private void CheckIfStuck()
    {
        // Check if the AI has moved since the last frame
        if (Vector3.Distance(transform.position, lastPosition) > 0.1f) // Use a small threshold to account for minor movements
        {
            // The AI has moved, reset the timer
            timeSinceLastMove = 0f;
        }
        else
        {
            // The AI hasn't moved, increment the timer
            timeSinceLastMove += Time.deltaTime;

            // If the AI has been stationary for longer than the threshold, change the destination
            if (timeSinceLastMove > stationaryTimeThreshold)
            {
                timeSinceLastMove = 0f;
                ChangePatrolDestination();
            }
        }

        // Update the last position for the next frame
        lastPosition = transform.position;
    }

    private void ChangePatrolDestination()
    {
        // Your logic to change the patrol destination goes here
        // Similar to what's in your Patrol() method when the agent.remainingDistance < 1f
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, 1))
        {
            Vector3 newPatrolPosition = hit.position;
            agent.SetDestination(newPatrolPosition);
        }
    }


    private void Idle()
    {
        // Transition to Patrol state after a short delay
        Invoke("SetPatrolState", 2f);
    }

    private void SetPatrolState()
    {
        state = State.Patrol;
    }



    private void Patrol()
    {
        if (agent.remainingDistance < 1f)
        {
            Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, 1);
            Vector3 patrolPosition = hit.position;

            agent.SetDestination(patrolPosition);
        
            // Reset rotation timer when a new destination is set
            rotationTimer = 0f;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= chaseRange && IsPlayerInFOV())
        {
            state = State.Chase;
        }
        else
        {
            // Rotate the enemy while patrolling
            RotateWhilePatrolling();
        }
    }

    private void RotateWhilePatrolling()
    {
        // Increment timer
        rotationTimer += Time.deltaTime;

        // Rotate for a certain duration
        if (rotationTimer <= rotationDuration)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        else if (rotationTimer > rotationDuration * 2) // Wait for double the duration before resetting
        {
            // Reset timer after rotation has completed and a pause has elapsed
            rotationTimer = 0f;
        }
    }


    private void ChasePlayer()
    {
        Vector3 chasePosition = player.position - offset;
        agent.SetDestination(chasePosition);
        FacePlayer();

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            state = State.Attack;
        }
        else if (distanceToPlayer > chaseRange || !IsPlayerInFOV())
        {
            state = State.Patrol;
        }
    }

    private void AttackPlayer()
    {
        agent.ResetPath();
        animator.SetTrigger("attack");

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > attackRange)
        {
            state = State.Chase;
        }
    }

    private void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private bool IsPlayerInFOV()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angleBetweenEnemyAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        return angleBetweenEnemyAndPlayer < fovAngle / 2f;
    }
}
