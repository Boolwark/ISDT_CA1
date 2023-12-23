namespace Weapons.Turret
{
    using System;
    using DefaultNamespace.ObjectPooling;
    using UnityEngine;
    public class ControllableTurret : MonoBehaviour
        {
            private bool isOn;
            public Transform playerTransform;
            public float rotSpeed;
            private float cdTimer;
            public float fireRate;
            public GameObject bulletPrefab;
            public Transform firePosition;
        
            public float shootForce;

            public void OnActivate()
            {
                isOn = !isOn;
            }
            private void Update()
            {
                if (isOn)
                {
                    Quaternion currentRotation = transform.rotation;
                    Quaternion targetRotation = Quaternion.Euler(playerTransform.eulerAngles.x, playerTransform.rotation.eulerAngles.y, currentRotation.eulerAngles.z);
                    transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, UnityEngine.Time.deltaTime * rotSpeed);
                    
                    if (cdTimer <= 0f)
                    {
                        cdTimer = 1 / fireRate;
                        FireShot();
                    }
                }

             
                cdTimer -= UnityEngine.Time.deltaTime;
            }

            private void FireShot()
            {
                var bulletGO = ObjectPoolManager.SpawnObject(bulletPrefab,firePosition.position,Quaternion.identity);
                bulletGO.transform.forward = transform.forward;
                if (bulletGO.TryGetComponent(out Rigidbody rb))
                {
                    rb.AddForce(transform.forward * shootForce,ForceMode.Force);
                }
            
            }

            private void Start()
            {
                playerTransform = Camera.main.transform;
            }
        }
    }
