using System;
using DefaultNamespace.ObjectPooling;
using Effects;
using Stats;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Weapons.Projectiles
{
    public class Rocket : MonoBehaviour
    {
      
        private Rigidbody rb;
        public float rocketSpeed;
        public float countdown = 3f;
        public float damage = 10000f;
        private bool exploded = false;
        public void ActivateRocket()
        {
            rb = GetComponent<Rigidbody>();
            rb.velocity = rb.transform.forward * rocketSpeed;
        }

      

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if(collision.collider.TryGetComponent(out StatsManager sm))
                {
                    sm.TakeDamage(damage);
                }
                Debug.Log("Enemy hit exploding");
                Explode();
            }
            
        }

        private void Explode()
        {
            if (exploded) return;
            exploded = true;
            ExplosionManager.Instance.SpawnExplosion(transform.position,transform.rotation);
            //ObjectPoolManager.ReturnObjectToPool(gameObject);
        }

        private void Update()
        {
            countdown -= Time.deltaTime;
            if(countdown <= 0f)
            {
                Debug.Log("Countdown reached");
                Explode(); 
            }
        }
    }
}