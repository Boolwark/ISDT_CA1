using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes.Builders;
using CrashKonijn.Goap.Configs.Interfaces;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Resolver;
using GOAP.Actions;
using GOAP.Goals;
using GOAP.Sensors;
using GOAP.Targets;
using GOAP.WorldKeys;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace GOAP.Factories
{
    [RequireComponent(typeof(DependencyInjector))]
    public class GoapSetConfigFactory : GoapSetFactoryBase
    {
        private DependencyInjector Injector;
        public static GoapSetConfigFactory CreateInstance()
        {
            return new GoapSetConfigFactory();
        }

        public override IGoapSetConfig Create()
        {
            Injector = GetComponent<DependencyInjector>();
            GoapSetBuilder builder = new("SoldierSet");
            BuildGoals(builder);
            BuildActions(builder);
            BuildSensors(builder);
            return builder.Build();
        }

        private void BuildGoals(GoapSetBuilder builder)
        {
            builder.AddGoal<WanderGoal>()
                .AddCondition<IsWandering>(Comparison.GreaterThanOrEqual,1);
            builder.AddGoal<KillPlayer>().AddCondition<PlayerHealth>(Comparison.SmallerThanOrEqual, 0);
            builder.AddGoal<HealGoal>().AddCondition<IsInjured>(Comparison.SmallerThanOrEqual, 0)
                ;
        }

        private void BuildActions(GoapSetBuilder builder)
        {
            builder.AddAction<WanderAction>()
                .SetTarget<WanderTarget>()
                .AddEffect<IsWandering>(EffectType.Increase)
                .SetBaseCost(5)
                .SetInRange(10);
            builder.AddAction<MeleeAttackAction>().SetTarget<PlayerTarget>()
                .AddEffect<PlayerHealth>(EffectType.Decrease).SetBaseCost(Injector.AttackConfig.MeleeAttackCost)
                .SetInRange(Injector.AttackConfig.SensorRadius);
            builder.AddAction<HealAction>().SetTarget<HealTarget>().AddEffect<IsInjured>(EffectType.Decrease).SetBaseCost(8).SetInRange(1);
        }

   
        
        private void BuildSensors(GoapSetBuilder builder)
        {
            builder.AddTargetSensor<WanderTargetSensor>()
                .SetTarget<WanderTarget>();
            builder.AddTargetSensor<PlayerTargetSensor>()
                .SetTarget<PlayerTarget>();
            builder.AddWorldSensor<InjuredSensor>().SetKey<IsInjured>();
        }
    }
}