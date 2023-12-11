using System;
using DefaultNamespace.ObjectPooling;
using Effects;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Weapons.Projectiles
{
    public class Rocket : MonoBehaviour
    {
      
        private Rigidbody rb;
        public float rocketSpeed;

       

        public void ActivateRocket()
        {
            rb = GetComponent<Rigidbody>();
            rb.velocity = rb.transform.forward * rocketSpeed;
       
        }

        private void OnCollisionEnter(Collision collision)
        {
            ExplosionManager.Instance.SpawnExplosion(transform.position,transform.rotation);
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
}