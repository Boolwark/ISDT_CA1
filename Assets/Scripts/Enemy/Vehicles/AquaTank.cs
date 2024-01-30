using DefaultNamespace.ObjectPooling;
using DG.Tweening;
using Effects;
using Stats;
using UnityEngine;
using Weapons;

namespace Vehicles
{
    public class AquaTank : MonoBehaviour
    {
        private bool isOn;
        public ParticleSystem sparkEffect;
            public Transform playerTransform;
            public float rotSpeed;
            public float speed;
            private float cdTimer;
            public float fireRate;
            public GameObject bulletPrefab;
            public Transform[] firePositions;
            public float distanceOffset = 3f;
            public float damageOnUse=5f;
            public float bulletSpeed,throwForce;

            private enum State
            {
                HOSTILE,
                CONTROLLED
            }

            
            
            public StatsManager StatsManager;

            private State currentState;
            private void Update()
            {
                if (currentState == State.HOSTILE)
                {
                    Vector3 directionToPlayer = playerTransform.position - transform.position;
                    Quaternion lookRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionToPlayer.normalized), rotSpeed * Time.deltaTime);

                    transform.rotation = lookRotation;
                    if (Vector3.Distance(playerTransform.position, transform.position) <= distanceOffset)
                    {
                        transform.Translate(directionToPlayer.normalized*Time.deltaTime*speed);
                    }
                    else
                    {
                        transform.RotateAround(playerTransform.position,Vector3.up,rotSpeed*Time.deltaTime);
                    }

                    if (cdTimer <= 0f)
                    {
                        FireShot();
                    }
                }

             
                cdTimer -= UnityEngine.Time.deltaTime;
            }

            public void OnSelect()
            {
                currentState = State.CONTROLLED;
            }
           
            public void OnActivate()
            {
                if (cdTimer <= 0f && currentState == State.CONTROLLED)
                {
                    StatsManager.TakeDamage(damageOnUse);
                    FireShot();
                    sparkEffect.Play();
                }
            }

            public void OnSelectExit()
            {
                Debug.Log("Throwing");
                currentState = State.HOSTILE;
                var rb = GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.AddForce(playerTransform.forward * throwForce,ForceMode.Force);
            }

            private void FireShot()
            {
                cdTimer = 1 / fireRate;
                foreach (var firePosition in firePositions)
                {
                    firePosition.GetComponent<ParticleSystem>().Play();
                    var bulletGO = ObjectPoolManager.SpawnObject(bulletPrefab,firePosition.position,Quaternion.identity);
                    bulletGO.transform.forward = transform.forward;
                    if (bulletGO.TryGetComponent(out Rigidbody rb))
                    {
                        if (currentState == State.CONTROLLED)
                        {
                            var bullet = bulletGO.GetComponent<Bullet>();
                            bullet.enemyTags.Add("Enemy");
                        }
                        rb.velocity = (currentState==State.CONTROLLED?transform.forward:(playerTransform.position - bulletGO.transform.position).normalized * bulletSpeed);
                        
                    }
                }
              
            
            }

            private void Start()
            {
                playerTransform = FindObjectOfType<Player.Player>().transform;
            }

            public void ExplodeOnDeath()
            {
                ExplosionManager.Instance.SpawnExplosion(transform.position,transform.rotation);
            }
        }
    }



    
