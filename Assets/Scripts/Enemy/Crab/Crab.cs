using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemy.Crab
{
    public class Crab : MonoBehaviour
    {
        public float wanderRadius=5f;
        private NavMeshAgent agent;
        private Vector3 GetRandomPosition()
        {
            int count = 0;
         
            while (count < 5)
            {
                Vector2 random = Random.insideUnitCircle * 5;
                var agentPos = agent.transform.position;
                Vector3 position = agentPos + new Vector3(
                    random.x,
                    0,
                    random.y
                );
                if (NavMesh.SamplePosition(position, out NavMeshHit hit, 1, NavMesh.AllAreas))
                {
                    return hit.position;
                }

                count++;
            }
            Debug.Log("Cant find place to wander");

            return agent.transform.position;
        }

        private void Wander()
        {
            agent.SetDestination(GetRandomPosition());
        }
        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            InvokeRepeating(nameof(Wander),0f,5f);
        }
    }
}