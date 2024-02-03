using System;
using DefaultNamespace.ObjectPooling;
using UnityEngine;

namespace Enemy
{
    public class TentacleLauncher : Enemy.NPCWeapon
    {
        public GameObject tentaclePf;
        public float cooldown = 3f;
        private float timeToNextActivation;
        public override void Activate()
        {
            if (timeToNextActivation > 0f) return;
            Debug.Log("Firing "+ transform.name);
            timeToNextActivation = cooldown;
            ObjectPoolManager.SpawnObject(tentaclePf, transform.position, transform.rotation);
        }

        public override bool IsReady()
        {
            return timeToNextActivation <= 0f;
        }

        public override void SetTarget(Transform newTarget)
        {
            
        }

        private void Update()
        {
            timeToNextActivation -= Time.deltaTime;
        }
    }
}