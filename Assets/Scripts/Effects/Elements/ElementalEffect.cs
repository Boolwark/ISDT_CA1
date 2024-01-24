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
        public StatsManager sm;

    

        public virtual void Activate()
        {
            sm = transform.parent.GetComponent<StatsManager>();
            var effect = ObjectPoolManager.SpawnObject(effectPf, transform.position, transform.rotation);
            effect.transform.parent = transform;
            sm.OnKilled.AddListener(DeathEffect);
            //effect logic goes here.
        }

        protected virtual void DeathEffect()
        {
            
        }
    }
}