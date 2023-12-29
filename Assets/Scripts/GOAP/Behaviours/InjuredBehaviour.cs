using CrashKonijn.Goap.Behaviours;
using GOAP.config;
using Stats;
using UnityEngine;

namespace GOAP.Behaviours
{
    [RequireComponent(typeof(AgentBehaviour))]
    public class InjuredBehaviour : MonoBehaviour
    {
        [field:SerializeField] public StatsManager StatsManager { get; private set; }

        [SerializeField]
        private HealingConfigSO HealingConfig;
        private AgentBehaviour AgentBehaviour;
      

        private void Awake()
        {
            AgentBehaviour = GetComponent<AgentBehaviour>();
        }
    }
}