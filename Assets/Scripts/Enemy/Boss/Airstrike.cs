using System;
using System.Collections;
using Effects;
using Stats;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

namespace Weapons.Misc
{
    public class Airstrike : MonoBehaviour
    {
     // Prefab for the airstrike visual effect
    // UI Text to show remaining airstrikes
        public float cooldown = 5f; // Cooldown duration in seconds
        public ParticleSystem airstrikeVFX;
        private float cooldownTimer = 0f;
        public GameObject reticle;
        public float duration;
        public float yOffset;
        public float aimDelay;
        public Transform target;
        public float damageRadius = 3f;
        public float damage;

        private void Start()
        {
            reticle.SetActive(false);
            target = GameObject.FindGameObjectWithTag("Player").transform;
            airstrikeVFX.Stop();
        }

        void Update()
        {
            if (cooldownTimer > 0)
            {
                cooldownTimer -= Time.deltaTime;
            }
            if (cooldownTimer <= 0)
            {
                StartCoroutine(ActivateAirstrike());
                cooldownTimer = cooldown;
            }
        }
        

        private IEnumerator ActivateAirstrike()
        {
            reticle.SetActive(false);
            Debug.Log("Airstrike");
            airstrikeVFX.Stop();
            reticle.transform.position = new Vector3(target.transform.position.x,target.transform.position.y - yOffset,target.transform.position.z);
            reticle.SetActive(true);
            yield return new WaitForSeconds(aimDelay);
            
            airstrikeVFX.Play();

            // Eliminate all enemies in the scene
           
               
                if (Vector3.Distance(target.transform.position, reticle.transform.position) > damageRadius)
                {
                    if (target.transform.TryGetComponent(out StatsManager statsManager))
                    {
                        Debug.Log("PLayer is damageed");
                        ExplosionManager.Instance.SpawnExplosion(reticle.transform.position,Quaternion.identity);
                        statsManager.TakeDamage(damage);
                    } 
                }
                
            StartCoroutine(DeactiveAirstrike());
        }

        private IEnumerator DeactiveAirstrike()
        {
            yield return new WaitForSeconds(duration);
            airstrikeVFX.Stop();
            reticle.SetActive(true);
        }
        
    }
}
