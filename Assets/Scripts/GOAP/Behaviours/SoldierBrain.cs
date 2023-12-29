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
        [SerializeField] private HealingConfigSO HealingConfig;
     
        [SerializeField] private InjuredBehaviour InjuredBehaviour;
        private AgentBehaviour AgentBehaviour;
        private bool IsPlayerInRange;

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
 
            IsPlayerInRange = true;
            SetGoal();
        }
        private void PlayerSensorOnPlayerExit(Vector3 lastKnownPosition)
        {
  
            IsPlayerInRange = false;
            SetGoal();
        }

        private void Update()
        {
           
                SetGoal();
            }

        private void SetGoal()
        {
            if (InjuredBehaviour.StatsManager.GetCurrentHealth() < HealingConfig.AcceptableHPLimit)
            {        AgentBehaviour.SetGoal<HealGoal>(false);   
            
            }
            else if (InjuredBehaviour.StatsManager.GetCurrentHealth() >= HealingConfig.AcceptableHPLimit &&  IsPlayerInRange)
            {
                // must kill player if HP is not below threshold
                AgentBehaviour.SetGoal<KillPlayer>(false);                
            }
            else if (InjuredBehaviour.StatsManager.GetCurrentHealth() >= HealingConfig.AcceptableHPLimit &&
                !IsPlayerInRange)
            {
                
                AgentBehaviour.SetGoal<WanderGoal>(false); 
            }
        }
    }
}