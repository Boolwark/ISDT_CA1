using System;
using UnityEngine;
using UnityEngine.Events;

namespace Util
{
    public class OnPlayerEnter : MonoBehaviour
    {

        public UnityEvent OnPlayerEntered;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                OnPlayerEntered?.Invoke();
            }
        }
    }
}