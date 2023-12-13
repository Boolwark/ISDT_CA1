using DefaultNamespace.ObjectPooling;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Weapons.Projectiles;

namespace Weapons
{
    public class RocketLauncher : Gun
    {
       

        protected override void Start()
        {
            base.Start();
        }
        public override void FireBullet(ActivateEventArgs args)
        {
            base.FireBullet(args);
        }
    }
}