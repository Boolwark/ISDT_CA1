using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using Stats;
using UnityEngine;

namespace GOAP.Sensors
{
    public class InjuredSensor : LocalWorldSensorBase
    {
        public override void Created()
        {
          
        }

        public override void Update()
        {
      
        }
        

        public override SenseValue  Sense(IMonoAgent agent, IComponentReference references)
        {
            return new SenseValue(Mathf.CeilToInt(references.GetCachedComponent<StatsManager>().GetCurrentHealth()));
        }
    }
}