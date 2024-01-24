using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using Ability.Abilities;
using DefaultNamespace.ObjectPooling;
using UnityEngine.Pool;

namespace Ability.Abilities
{
    [CreateAssetMenu]
    public class WebAbility : global::Ability.Ability
    {
        [SerializeField] private GameObject webProjectilePrefab; // Attach this in the inspector
      
        [SerializeField] private GameObject playerRightHand;// Assign this in the inspector
        [SerializeField] private float webRange = 10f;
        public float forwardForce = 100f, upwardForce = 5f;

        public override void Activate(GameObject parent)
        {
            Debug.Log("Firing webs");
            if (!playerRightHand)
            {
                playerRightHand = GameObject.Find("RightHand");
            }

          
         
            RaycastHit hit;
            if (Physics.Raycast(parent.transform.position, parent.transform.forward, out hit, webRange))
            {
                Debug.Log("Hit " + hit.transform.name);
                FireWebProjectile();

             
              

                GameObject currentTarget = hit.collider.gameObject;
                if (currentTarget.CompareTag("Enemy"))
                {
                    Debug.Log("Fired web on" + currentTarget.name);
                    NavMeshAgent enemyAgent = currentTarget.GetComponent<NavMeshAgent>();
                    if (enemyAgent != null)
                    {
          
                        enemyAgent.isStopped = true;
                    }

                    // Optional: Start a coroutine to remove the immobilization after some time
              
                }
            }
        }

 

    

        private void FireWebProjectile()
        {
            var spawnedProjectile = ObjectPoolManager.SpawnObject(webProjectilePrefab, playerRightHand.transform.position,
                playerRightHand.transform.rotation);
            var rb = spawnedProjectile.GetComponent<Rigidbody>();
            rb.AddForce(spawnedProjectile.transform.forward * forwardForce,ForceMode.Force);
            rb.AddForce(spawnedProjectile.transform.up * upwardForce,ForceMode.Force);
            Destroy(spawnedProjectile,1f);
        }
    }
}
