using CodeMonkey.Utils;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Classes.References;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;
using DefaultNamespace.ObjectPooling;
using GOAP.Behaviours;
using GOAP.config;
using UnityEngine;

namespace GOAP.Actions
{
    public class HealAction : ActionBase<HealAction.Data>,IInjectable
    {
        public HealingConfigSO HealingConfig;
        private Animator Animator;
        private static readonly int IS_HEALING = UnityEngine.Animator.StringToHash("isHealing");
        private bool spawnedEffect = false;
        public class Data : CommonData
        {
            [GetComponent] public Animator Animator { get; set; }
            [GetComponent]
            public InjuredBehaviour InjuredBehaviour { get; set; }
        }

        public override void Created()
        {
       
        }

        public override void Start(IMonoAgent agent, Data data)
        {
            data.InjuredBehaviour.enabled = false;
            data.Timer = 1f;
        }

        public override ActionRunState Perform(IMonoAgent agent, Data data, ActionContext context)
        {
            data.Timer -= context.DeltaTime;
            data.InjuredBehaviour.StatsManager.TakeHeal(HealingConfig.HPRestorationRate);
           
            if (!spawnedEffect)
            {
                data.Animator.StopPlayback();
                data.Animator.SetBool(IS_HEALING, true);
                var effect = ObjectPoolManager.SpawnObject(HealingConfig.HealEffectPrefab, agent.transform.position,
                    Quaternion.identity);
                spawnedEffect = true;
                FunctionTimer.Create(() =>
                {
                    ObjectPoolManager.ReturnObjectToPool(effect);
                    spawnedEffect = false;
                },HealingConfig.EffectDuration);
            }
       
            if (data.Target == null || data.InjuredBehaviour.StatsManager.GetCurrentHealth() >= HealingConfig.AcceptableHPLimit)
            {
                return ActionRunState.Stop;
            }

            return ActionRunState.Continue;
        }
        

        public override void End(IMonoAgent agent, Data data)
        {
            data.Animator.SetBool(IS_HEALING, false);
        }

        public void Inject(DependencyInjector injector)
        {
            HealingConfig = injector.HealConfig;
        }
    }
}