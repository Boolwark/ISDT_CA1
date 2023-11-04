using UnityEngine;
using UnityEngine.AI;

public class FleeFromEnemy : MonoBehaviour
{
    public float checkRadius = 10f; // Radius to check for enemies
    public float fleeDistance = 15f; // Distance to flee from the enemy
    public float obstacleCheckDistance = 5f; // Distance to check for obstacles
    private NavMeshAgent agent;
    private Transform enemyTransform;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check for enemies within the radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);
        bool enemyFound = false;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                enemyTransform = hitCollider.transform;
                enemyFound = true;
                Flee();
                break;
            }
        }

        if (!enemyFound)
        {
            animator.SetBool("isRunning",false);
        }
    }

    void Flee()
    {
        if (enemyTransform != null)
        {
            Vector3 fleeDirection = transform.position - enemyTransform.position;
            Vector3 newFleePosition = transform.position + fleeDirection.normalized * fleeDistance;

            // Advanced Raycasting for obstacle detection
            if (IsObstacleInFleeDirection(fleeDirection))
            {
                // If an obstacle is detected, find a new flee direction
                fleeDirection = FindNewFleeDirection();
                newFleePosition = transform.position + fleeDirection.normalized * fleeDistance;
            }

            // Rotate the AI to face the flee direction
            Quaternion fleeRotation = Quaternion.LookRotation(fleeDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, fleeRotation, Time.deltaTime * agent.angularSpeed);

            NavMeshHit hit;
            NavMesh.SamplePosition(newFleePosition, out hit, fleeDistance, NavMesh.AllAreas);
            animator.SetBool("isRunning", true);
            agent.SetDestination(hit.position);
        }
    }

    bool IsObstacleInFleeDirection(Vector3 direction)
    {
        // Perform multiple raycasts in a fan-like spread
        float angleStep = 15f; // Angle step for raycasts
        for (float angle = -45f; angle <= 45f; angle += angleStep)
        {
            Vector3 rayDirection = Quaternion.Euler(0, angle, 0) * direction.normalized;
            if (Physics.Raycast(transform.position, rayDirection, obstacleCheckDistance))
            {
                return true; // Obstacle detected
            }
        }
        return false; // No obstacles detected
    }



    Vector3 FindNewFleeDirection()
    {
        // Attempt to find a clear direction by rotating the original flee direction
        for (int i = 0; i < 360; i += 45)
        {
            Vector3 newDirection = Quaternion.Euler(0, i, 0) * (transform.position - enemyTransform.position);
            if (!Physics.Raycast(transform.position, newDirection.normalized, obstacleCheckDistance))
            {
                return newDirection;
            }
        }

        // If no clear direction is found, return the original direction
        return Vector3.zero;
        
    }
}
