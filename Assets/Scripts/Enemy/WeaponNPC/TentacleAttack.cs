using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.ObjectPooling;
using DG.Tweening;
using Effects.DefaultNamespace.GameUI;
using UnityEngine;
using Random = UnityEngine.Random;

public class TentacleAttack : MonoBehaviour
{
    public int vibration = 10;
    public float shakeStrength;
    public float attackRange = 5f;
    public float force=100f;
    public float attackDuration = 1f;
    public LayerMask targetLayer;
    private Rigidbody rb;
    public float offset;
    private bool isAttacking = false;
    public List<GameObject> tentacles;

    private void Start()
    {
        foreach (var tentacle in tentacles)
        {
            tentacle.GetComponent<Rigidbody>().AddForce(tentacle.transform.forward*100f,ForceMode.Force);
        }
    }

  

    private void Attack()
    {
        Vector3 targetPos = Camera.main.transform.position + Camera.main.transform.forward * offset;
        foreach (GameObject tentacle in tentacles)
        {
            tentacle.transform.DOMove(targetPos, 0.5f);
            StartCoroutine(ShakeCoroutine(tentacle.transform));
        }
    }
    private IEnumerator ShakeCoroutine(Transform target)
    {
        Vector3 ogPos = target.transform.position;
        for (int i = 0; i < vibration; i++)
        {
            // Generate random offsets for the camera's position
            Vector3 randomOffset = Random.insideUnitSphere * shakeStrength;

         
            target.transform.position = ogPos + randomOffset;

      
            yield return new WaitForSeconds(0.1f);
        }


        target.transform.position = ogPos;
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Shaking player");
           FindObjectOfType<CameraShake>().ShakeCamera();
           Attack();
    
        }

    }
}