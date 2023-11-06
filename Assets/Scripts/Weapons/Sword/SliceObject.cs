using System;
using Ability;
using Ability.Abilities;
using EzySlice;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons.Sword
{
    public class SliceObject : MonoBehaviour
    {
        public Transform startSlicePoint;
        public Transform endSlicePoint;
        public VelocityEstimator velocityEstimator;
        public Material crossSectionMaterial;
        public LayerMask sliceableLayer;
        public float cutForce = 5f;

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
            }
            
            
           
        }

        public void OnSelect()
        {
            if (!abilityHolder)
            {
                abilityHolder = FindObjectOfType<AbilityHolder>();
                oldAbility = abilityHolder.ability;
            }
            abilityHolder.ability = dashAbility;
            
        }
        public void OnDeSelect()
        {
            abilityHolder.ability = oldAbility;
        }

        public void Slice(GameObject target)
        {
            Vector3 velocity = velocityEstimator.GetVelocityEstimate();
            Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
            planeNormal.Normalize();
            
            SlicedHull hull = target.Slice(endSlicePoint.position,planeNormal);
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

        }
    }
    
}