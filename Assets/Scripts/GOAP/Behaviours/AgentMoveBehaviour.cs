using System;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace GOAP.Behaviours
{
    [RequireComponent(typeof(NavMeshAgent),typeof(Animator))]
    public class AgentMoveBehaviour : MonoBehaviour
    {
        private Animator Animator;
        private NavMeshAgent navMeshAgent;
        private AgentBehaviour _agentBehaviour;
        private ITarget currentTarget;
        private Vector3 lastPosition;
        [SerializeField] private float minMoveDistance = 0.25f;
        private static readonly int WALK = Animator.StringToHash("isWalking");
        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            _agentBehaviour = GetComponent<AgentBehaviour>();
            Animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
         
            _agentBehaviour.Events.OnTargetChanged += EventsOnTargetChanged;
            _agentBehaviour.Events.OnTargetOutOfRange += EventsOnTargetOutOfRange;
        }

        private void OnDisable()
        {
         
            _agentBehaviour.Events.OnTargetChanged -= EventsOnTargetChanged;
            _agentBehaviour.Events.OnTargetOutOfRange -= EventsOnTargetOutOfRange;
        }

   

        private void EventsOnTargetChanged(ITarget target,bool inRange)
        {
            currentTarget = target;
            lastPosition = currentTarget.Position;
            navMeshAgent.SetDestination(target.Position);
            Animator.SetBool(WALK,true);
        }

        private void EventsOnTargetOutOfRange(ITarget target)
        {
            Animator.SetBool(WALK,false);
        }

        private void Update()
        {
            if (currentTarget == null)
            {
                return;
            }

            if (minMoveDistance <= Vector3.Distance(currentTarget.Position, lastPosition))
            {
                lastPosition = currentTarget.Position;
                navMeshAgent.SetDestination(currentTarget.Position);
            }

            Animator.SetBool(WALK,navMeshAgent.velocity.magnitude > 0.1f);





        }
        
    }
   
}