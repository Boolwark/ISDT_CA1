using System;
using System.Collections;
using System.Collections.Generic;
using Effects;
using Effects.DefaultNamespace.GameUI;
using Environment;
using Stats;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Weapons.Misc
{
  using UnityEngine;

public class C4Explosive : MonoBehaviour
{
    public float attachRadius = 5.0f; // Radius to search for an attach point
    public float countdown = 5.0f; // Time in seconds before the C4 explodes
    public float explosionRadius = 10.0f; // Radius of the explosion
    public float explosionForce = 1000.0f;
    public float damageAmount;// Force of the explosion

    public UnityEvent OnExplode;

    private float remainingTime;
    public TextMeshProUGUI countdownText;

    private void Start()
    {
        remainingTime = countdown;
    }

    public void OnSelectExit()
    {
        // Find the nearest attach point within the specified radius
        print("On select exit invoked");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attachRadius);
        float closestDistance = float.MaxValue;
        Transform closestAttachPoint = null;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out CustomXRSocketInteractor customXRSocketInteractor))
            {
                transform.SetParent(customXRSocketInteractor.attachTransform);
                transform.localPosition = Vector3.zero;
                Invoke("Explode", countdown);
                break;
            }
        }

      
        StartCoroutine(CountdownToExplode());
    }
    IEnumerator CountdownToExplode()
    {
        while (remainingTime > 0)
        {
            // Update the text display each frame
            countdownText.text = $"{remainingTime:F2}";
            remainingTime -= Time.deltaTime;
            yield return null; 
        }

        Explode();
    }

    void Explode()
    {
     
        //Spawn the explosion!
        ExplosionManager.Instance.SpawnExplosion(transform.position,Quaternion.identity);
        // Find all colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (var collider in colliders)
        {
            // Apply damage to enemies or destructible objects
            if (collider.TryGetComponent(out StatsManager sm))
            {
                // Add your damage logic here
                // For example, you could call a TakeDamage method on an enemy script
                sm.TakeDamage(damageAmount);
            }
            // the explosion can destroy the environment as well, such as doors,walls,etc...
            if (collider.TryGetComponent(out MeshDestroy meshDestroy))
            {
                meshDestroy.DestroyMesh();
            }
            // Apply explosion force (if the object has a Rigidbody)
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }

        FindObjectOfType<CameraShake>().ShakeCamera();
        Destroy(gameObject);
    }
}

}