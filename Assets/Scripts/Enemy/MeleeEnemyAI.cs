
using Stats;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

/// <summary>
/// This Enemy AI chases both the player and NPC characters. (Tagged with Player and NPC respectively)
/// </summary>
public class MeleeEnemyAI : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent;
    private Animator animator;
   
    public bool isCorePresent = true;
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
    private StatsManager _statsManager;
    public UnityEvent OnPlayerDetected;
    private Transform playerTr;
    

    private enum State
    {
        Idle,
        Patrol,
        Chase,
        Attack
    }

    private State state;

     protected void Start()
    {
        playerTr = FindObjectOfType<Player.Player>().transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Debug.Log("Animator is null" + animator==null);
        _statsManager = GetComponent<StatsManager>();


        lastPosition = transform.position;
        timeSinceLastMove = 0f;

        state = State.Idle;
    }

    private void Update()
    {
        Debug.Log("Current state is" + state);
        switch (state)
        {
            case State.Idle:
                Idle();
                break;
            case State.Patrol:
                animator.SetBool("isWalking", true);
                Patrol();
                CheckIfStuck();
                break;
            case State.Chase:
                ChaseTarget();
                animator.SetBool("isWalking", true);
                break;
            case State.Attack:
                AttackTarget();
                break;
        }
    }

    public void OnAttacked()
    {
        // immedieltly attack the player when attacked. 
        target = playerTr;
        state = State.Chase;
    }
     private void ChaseTarget()
    {
        if (target != null && agent != null)
        {
            Vector3 chasePosition = target.position - offset;
            agent.SetDestination(chasePosition);
            FaceTarget(target);

            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget <= attackRange)
            {
                state = State.Attack;
            }
            else if (distanceToTarget > chaseRange || !IsTargetInFOV(target))
            {
                state = State.Patrol;
            }
        }
        else
        {
            state = State.Patrol;
        }
    }

    private void AttackTarget()
    {
        if (target != null && target != transform)
        {
            agent.ResetPath();
            animator.SetTrigger("attack");

            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget > attackRange)
            {
                state = State.Chase;
            }
            else
            {
                if (target.TryGetComponent(out StatsManager targetStatsManager))
                {
                    print(transform.name + "Is attacking" + target.name);
                    targetStatsManager.TakeDamage(_statsManager.Attack);
                }
            }
        }
        else
        {
            state = State.Patrol;
        }
    }

    private void FaceTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private bool IsTargetInFOV(Transform target)
    {
        if (target==null ) return false;
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        float angleBetweenEnemyAndTarget = Vector3.Angle(transform.forward, directionToTarget);

        
        return angleBetweenEnemyAndTarget < fovAngle / 2f;
    }

    private void FindTargets()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, chaseRange);
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("NPC") || hitCollider.CompareTag("Player"))
            {
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (distance < closestDistance && IsTargetInFOV(hitCollider.transform))
                {
                    closestDistance = distance;
                    closestTarget = hitCollider.transform;
                }
            }
        }

        target = closestTarget;
        if (target != null)
        {
            OnPlayerDetected?.Invoke();
            state = State.Chase;
        }
        else
        {
            target = transform;
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

        float distanceToPlayer = Vector3.Distance(transform.position, transform.position);
        if (distanceToPlayer <= chaseRange && IsTargetInFOV(target))
        {
            state = State.Chase;
        }
        else
        {
            // Rotate the enemy while patrolling
            RotateWhilePatrolling();
        }
        FindTargets();
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
    

}
