using System;
using Ability;
using Ability.Abilities;
using CodeMonkey.Utils;
using EzySlice;
using Stats;
using UnityEngine;
using UnityEngine.InputSystem;
using Util;

namespace Weapons.Sword
{
    public class SliceObject : MonoBehaviour
    {
        public Transform startSlicePoint;
        public Transform endSlicePoint;
        public VelocityEstimator velocityEstimator;
        public Material crossSectionMaterial;
        public LayerMask sliceableLayer;
        public float cutForce = 500f;
        private Rigidbody playerRB;
        public float dissolveSpeed = 0.1f;

        public AbilityHolder abilityHolder;
        public DashAbility dashAbility;
        private Ability.Ability oldAbility;
        private void FixedUpdate()
        {
            bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position,out RaycastHit hit,sliceableLayer);
            if (hasHit)
            {
                GameObject target = hit.transform.gameObject;
                print("Slicing " + target.name);
                Slice(target);
                AudioManager.Instance.PlaySFX("Slash");
            }
            
            
           
        }

        public void OnSelect()
        {
            if (!abilityHolder)
            {
                abilityHolder = FindObjectOfType<AbilityHolder>();
                playerRB = abilityHolder.transform.GetComponent<Rigidbody>();
                oldAbility = abilityHolder.ability;
             
              
           
            }
            playerRB.isKinematic = true;
            playerRB.velocity = Vector3.zero;
            abilityHolder.ability = dashAbility;
            
        }
     

        public void Slice(GameObject target)
        {
            Vector3 velocity = velocityEstimator.GetVelocityEstimate();
            Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
            planeNormal.Normalize();
            
            SlicedHull hull = target.Slice(endSlicePoint.position,planeNormal);
            if (target.TryGetComponent(out StatsManager sm))
            {
                Debug.Log("Death by slashing");
                sm.TakeDamage(99999);
            }
            if (hull != null)
            {
                GameObject upperHull = hull.CreateUpperHull(target,crossSectionMaterial);
                GameObject lowerHull = hull.CreateLowerHull(target,crossSectionMaterial);
                SetupSlicedComponent(upperHull);
                SetupSlicedComponent(lowerHull);
                Destroy(target);
            }
        }

        public void SetupSlicedComponent(GameObject slicedObject)
        {
            Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
            MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
            collider.convex = true;
            rb.AddExplosionForce(cutForce,slicedObject.transform.position,1);
           // FunctionTimer.Create(() => { DissolveManager.Instance.DissolveObject(slicedObject,2f); }, 2f);
        }
    }
    
}