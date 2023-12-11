using DefaultNamespace.ObjectPooling;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Weapons.Projectiles;

namespace Weapons
{
    public class RocketLauncher : Gun
    {
        public GameObject rocketPrefab;
        public Transform rocketSpawnPoint;
        public Rocket currentRocket;

        protected override void Start()
        {
            base.Start();
            currentRocket = GetComponentInChildren<Rocket>();
        }
        public override void FireBullet(ActivateEventArgs args)
        {
            base.FireBullet(args);
            currentRocket.ActivateRocket();
            currentRocket = ObjectPoolManager.SpawnObject(rocketPrefab, rocketSpawnPoint.position, Quaternion.LookRotation(-transform.forward)).GetComponentInChildren<Rocket>();
            currentRocket.transform.parent.forward = rocketSpawnPoint.forward;
        }
    }
}