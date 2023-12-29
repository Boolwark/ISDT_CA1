using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;
using GOAP.config;
using UnityEngine;

namespace GOAP.Actions
{
    public class MeleeAttackAction : ActionBase<AttackData>,IInjectable
    {
        private AttackConfigSO AttackConfig;
        public override void Created()
        {
         
        }

        public override void Start(IMonoAgent agent, AttackData data)
        {
            data.Timer = AttackConfig.AttackDelay;
        }

        public override ActionRunState Perform(IMonoAgent agent, AttackData data, ActionContext context)
        {
            data.Timer -= context.DeltaTime;
            bool shouldAttack = data.Target != null &&  Vector3.Distance(data.Target.Position, agent.transform.position) <=
                AttackConfig.MeleeAttackRadius;
            Debug.Log("Player in Range: "+shouldAttack);
                                data.animator.SetBool(AttackData.ATTACK,shouldAttack);
            if (shouldAttack)
            {
                FaceTarget(agent, data.Target);
            }
            return data.Timer > 0 ? ActionRunState.Continue : ActionRunState.Stop;
        }

        public override void End(IMonoAgent agent, AttackData data)
        {
            data.animator.SetBool(AttackData.ATTACK,false);
        }

        public void Inject(DependencyInjector injector)
        {
            AttackConfig = injector.AttackConfig;
        }
        private void FaceTarget(IMonoAgent agent,ITarget target)
        {
            Vector3 direction = (target.Position - agent.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}