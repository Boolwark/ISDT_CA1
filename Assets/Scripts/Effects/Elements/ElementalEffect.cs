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
        private Renderer _renderer;
        public LayerMask whatIsEnemy;
        public bool hitEnemy;

        private void Start() 
        {
                 
        }

        public virtual void Activate()
        {
            if (transform.parent == null)
            {
                transform.parent = GetClosestTarget();
            }

            if (transform.parent == null)
            {
                hitEnemy = false;
                return;
            }
            _renderer = transform.parent.GetComponent<MeshRenderer>();
            if (_renderer == null)
            {
                _renderer = transform.parent.GetComponent<SkinnedMeshRenderer>();
            }

            if (_renderer == null)
            {
                return;
            }
            _renderer.material = effectMaterial;
            var effect = ObjectPoolManager.SpawnObject(effectPf, transform.position, transform.rotation);
            effect.transform.parent = transform;
            if (transform.parent.TryGetComponent(out StatsManager statsManager))
            {
                sm = statsManager;
                sm.OnKilled.AddListener(DeathEffect);
                hitEnemy = true;
            }
        
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