using CrashKonijn.Goap.Behaviours;
using UnityEngine;

namespace GOAP.Behaviours
{
    [RequireComponent(typeof(AgentBehaviour))]
    public class GoapSetBinder : MonoBehaviour
    {
        [SerializeField] private GoapRunnerBehaviour GoapRunner;

        private void Awake()
        {
            AgentBehaviour agent = GetComponent<AgentBehaviour>();
            agent.GoapSet = GoapRunner.GetGoapSet("SoldierSet");
        }
    }
}