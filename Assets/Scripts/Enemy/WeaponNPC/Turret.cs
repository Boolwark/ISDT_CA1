using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.ObjectPooling;
using Effects;
using Enemy;
using UnityEngine;
using Weapons;

namespace Enemy
{
    public class Turret : NPCWeapon
    {
        private Transform currentTarget;
        public WeaponControllerConfig WeaponControllerConfig;
        public  GunData GunData;
        private float timeToNextActivation;
        public ParticleSystem muzzleEffect;
        public Transform shootPoint;
        private void Start()
        {
            timeToNextActivation = 0f;
        }

        private void Update()
        {
            timeToNextActivation -= Time.deltaTime;
        }

        public override void Activate()
        {
            if (timeToNextActivation > 0f) return;
            Debug.Log("Firing "+ transform.name);
            timeToNextActivation = 1/GunData.fireRate;
            Fire();
        }

        public override bool IsReady()
        {
            return timeToNextActivation <= 0f;
        }

        public override void SetTarget(Transform newTarget)
        {
            currentTarget = newTarget;
        }

        private void Fire()
        {
            muzzleEffect.Play();
            var bulletGO = ObjectPoolManager.SpawnObject(GunData.bullet, shootPoint.transform.position, shootPoint.rotation);
            Vector3 dir = (currentTarget.transform.position - shootPoint.transform.position).normalized;
            bulletGO.GetComponent<Rigidbody>().AddForce(dir*1000f,ForceMode.Force);
        }

        public void OnSelectExit()
        {
            foreach (var weaponController in Physics.OverlapSphere(transform.position,WeaponControllerConfig.snapRadius,WeaponControllerConfig.whatIsWeaponController))
            {
                if (weaponController.TryGetComponent(out NPCWeaponController npcWeaponController))
                {
                    Debug.Log("Found Weapon Controller");
                    npcWeaponController.AddNewWeapon(this);
                    return;
                }
            }

           
        }
    }
    }
