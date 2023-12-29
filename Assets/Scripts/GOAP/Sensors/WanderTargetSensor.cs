using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using UnityEngine;
using UnityEngine.AI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace GOAP.Sensors
{
    public class WanderTargetSensor : LocalTargetSensorBase
    {
        public override void Created()
        {
            
        }

        public override void Update()
        {
           
        }

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            Vector3 position = GetRandomPosition(agent);
            return new PositionTarget(position);
        }

        private Vector3 GetRandomPosition(IMonoAgent agent)
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
    }
}