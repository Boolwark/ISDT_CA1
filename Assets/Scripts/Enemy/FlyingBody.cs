using System;
using CodeMonkey.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class FlyingBody : MonoBehaviour
    {
        [SerializeField] private GameObject enemyBodyPf;
        [SerializeField] private float explosionForce;
        public float airTime = 3f;
        
      

        public void OnEnemyDeath()
        {
            var enemyBody = Instantiate(enemyBodyPf, transform.position, Quaternion.identity);
            var rb = enemyBody.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.AddExplosionForce(explosionForce,-transform.forward,1f);
                FunctionTimer.Create(() => { rb.isKinematic = true; }, airTime);
        }
    }
}