using System;
using DG.Tweening;
using UnityEngine;

namespace Weapons.PropellerGun
{
    public class PropellerGun : MonoBehaviour
    {
        public GameObject blades;
        private bool spinBlades = false;
        public float range = 10f, speed = 20f;
        public float attractSpeed = 5f;
        public LayerMask whatIsEnemy;
        public float angle;
        public ParticleSystem chargeEffect;
        public void Activate()
        {
           
            spinBlades = !spinBlades;
            
        }
        
        private void Start()
        {
            InvokeRepeating(nameof(AttractEnemies),0f,1f);
        }

        private void Update()
        {
            if (spinBlades)
            {
                blades.transform.Rotate(blades.transform.right * speed * Time.deltaTime);
            }
        }

        private void AttractEnemies()
        {
            if (!spinBlades)
            {
                chargeEffect.Stop();
                return;
            }
            chargeEffect.Play();
            foreach (Collider enemy in Physics.OverlapSphere(transform.position, range, whatIsEnemy))
            {
                Debug.Log("Attracting " + enemy.name);
                if (Vector3.Angle(enemy.transform.position, transform.position) > angle) continue;
                enemy.transform.DOMove(transform.position,
                    Vector3.Distance(transform.position, enemy.transform.position) / attractSpeed);
            };
        }
    }
}