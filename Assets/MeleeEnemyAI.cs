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
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= chaseRange && IsPlayerInFOV())
        {
            state = State.Chase;
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
