using UnityEngine;
using UnityEngine.AI;

namespace Enemy.Flyer
{
    public class FlyerEnemy : ShooterEnemy
    {
        public FlyerEnemyConfig flyerEnemyConfig;
        public DemoAnimationSelector DemoAnimationSelector;
        private new void Awake()
        {
            base.Awake();
            projectile = flyerEnemyConfig.bullet;
            hasKinematicProjectile = flyerEnemyConfig.hasKinematicProjectile;
            sightRange = flyerEnemyConfig.sightRange;
            attackRange = flyerEnemyConfig.attackRange;
            DemoAnimationSelector.SwitchAnimation(0);
            ConfigureFlyHeight();
        }

        private void ConfigureFlyHeight()
        {
            GetComponent<NavMeshAgent>().baseOffset = flyerEnemyConfig.baseOffset;
        }
    }
}