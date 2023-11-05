using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Effects
{
    public class Web : MonoBehaviour
    {
        public float immobilizeDuration;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Enemy"))
            {
                if (collision.collider.TryGetComponent(out NavMeshAgent agent) && agent.isActiveAndEnabled)
                {
                    Debug.Log("Immobilizing " + agent.name);
                    agent.isStopped = true;
                    StartCoroutine(ResumeAgent(agent));
                }
            }
        }

        private IEnumerator ResumeAgent(NavMeshAgent agent)
        {
            yield return new WaitForSeconds(immobilizeDuration);
            agent.isStopped = false;
        }
    }
}