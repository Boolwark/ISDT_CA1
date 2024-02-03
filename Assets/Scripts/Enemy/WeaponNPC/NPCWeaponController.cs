using System;
using System.Collections.Generic;
using Effects.DefaultNamespace.GameUI;
using UnityEngine;

namespace Enemy
{
    public class NPCWeaponController : MonoBehaviour
    {
        public List<NPCWeapon> weapons;
        private Transform target;
        public Transform attachPoint;
        public WeaponControllerConfig WeaponControllerConfig;
        private void Start()
        {
            InvokeRepeating(nameof(FindTargets),0f,3f);
        }

        private void FindTargets(NPCWeaponController weapon)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, WeaponControllerConfig.range,WeaponControllerConfig.whatIsEnemy);
            Transform closestTarget = null;
            float closestDistance = Mathf.Infinity;

            foreach (var hitCollider in hitColliders)
            {
               
                    float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestTarget = hitCollider.transform;
                    }
                
            }
            target = closestTarget;
        }


        private void Update()
        {
            if(target != null)
            {
                foreach (NPCWeapon npcWeapon in weapons)
                {
                    if (!npcWeapon.IsReady()) continue;
                    Debug.Log("Firing NPC weapon: "+ npcWeapon.name);
                    npcWeapon.Activate();
                }
            }
           
        }

        public void AddNewWeapon(NPCWeapon npcWeapon)
        {
            Debug.Log("attaching weapon");
            weapons.Add(npcWeapon);
            npcWeapon.transform.SetParent(attachPoint);
            npcWeapon.transform.localPosition = Vector3.zero;
            npcWeapon.transform.forward = attachPoint.forward;
        }
        
    }
}