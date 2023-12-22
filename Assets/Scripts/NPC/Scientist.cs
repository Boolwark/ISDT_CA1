using System;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.NPC
{
    public class Scientist : MonoBehaviour
    {
        private Transform playerTr;
        public Vector3 offset;
        private NavMeshAgent agent;
        public float distanceOffset = 1f;
        private Animator animator;
        private int runHash = Animator.StringToHash("isRunning");
        private void Start()
        {
            playerTr = Camera.main.transform;
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            InvokeRepeating(nameof(MoveToPlayer),0,5f);
        }

        public void MoveToPlayer()
        {
            if (Vector3.Distance(playerTr.position, transform.position) <= distanceOffset) return;
            agent.SetDestination(playerTr.transform
                .position + offset);
            
            animator.SetBool(runHash,true);
        }

        private void Update()
        {
            if (agent.remainingDistance <= distanceOffset)
            {
                animator.SetBool(runHash,false);
            }
        }
        
    }
}