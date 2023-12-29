using System;
using CrashKonijn.Goap.Behaviours;
using GOAP.config;
using GOAP.Goals;
using GOAP.Sensors;
using UnityEngine;

namespace GOAP.Behaviours
{
    [RequireComponent(typeof(AgentBehaviour))]
    public class SoldierBrain : MonoBehaviour
    {
        [SerializeField] private PlayerSensor PlayerSensor;
        [SerializeField] private AttackConfigSO AttackConfig;
        private AgentBehaviour AgentBehaviour;

        private void Awake()
        {
            AgentBehaviour = GetComponent<AgentBehaviour>();
        }

        private void Start()
        {
            AgentBehaviour.SetGoal<WanderGoal>(false);
        }

        private void OnEnable()
        {
            PlayerSensor.OnPlayerEnter += PlayerSensorOnPlayerEnter;
            PlayerSensor.OnPlayerExit += PlayerSensorOnPlayerExit;
        }

        private void OnDisable()
        {
            PlayerSensor.OnPlayerEnter -= PlayerSensorOnPlayerEnter;
            PlayerSensor.OnPlayerExit -= PlayerSensorOnPlayerExit;
        }

        private void PlayerSensorOnPlayerEnter(Transform player)
        {
            AgentBehaviour.SetGoal<KillPlayer>(true);
        }
        private void PlayerSensorOnPlayerExit(Vector3 lastKnownPosition)
        {
            AgentBehaviour.SetGoal<WanderGoal>(true);
        }

       
    }
}