using System;
using Stats;
using UnityEngine;

namespace Environment
{
    public class Debris : ThrowableObject
    {
        public float damage = 100f;
       
        public new void OnSelectExit()
        {
            base.OnSelectExit();
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Thrown object has hit " + collision.gameObject.name);
            if (collision.collider.CompareTag("Enemy"))
            {
                if (collision.collider.TryGetComponent(out StatsManager sm))
                {
                    sm.TakeDamage(damage);
                }
            }
        }
    }
}