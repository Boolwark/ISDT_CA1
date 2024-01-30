using System;
using DefaultNamespace.ObjectPooling;
using Stats;
using UnityEngine;
using UnityEngine.Pool;

namespace Effects.Elements
{
    public class ElementalEffect : MonoBehaviour
    {
        public float damage;
        public GameObject effectPf;
        public Material effectMaterial;
        public StatsManager sm;
        public float range=3f;
        private MeshRenderer _renderer;
        public LayerMask whatIsEnemy;

        public virtual void Activate()
        {
            if (transform.parent == null)
            {
                transform.parent = GetClosestTarget();
            }
            _renderer = transform.parent.GetComponent<MeshRenderer>();
            sm = transform.parent.GetComponent<StatsManager>();
            _renderer.material = effectMaterial;
            var effect = ObjectPoolManager.SpawnObject(effectPf, transform.position, transform.rotation);
            effect.transform.parent = transform;
            sm.OnKilled.AddListener(DeathEffect);
            //effect logic goes here.
        }

        private Transform GetClosestTarget()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, range, whatIsEnemy);
        
            Transform closestTarget = null;
            float closestDistance = Mathf.Infinity;

            foreach (Collider collider in colliders)
            {
                float distanceToTarget = Vector3.Distance(transform.position, collider.transform.position);

                if (distanceToTarget < closestDistance)
                {
                    closestDistance = distanceToTarget;
                    closestTarget = collider.transform;
                }
            }

            return closestTarget;
        }

        protected void DeathEffect()
        {
            
        }
    }
}