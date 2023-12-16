using System;
using UnityEngine;

namespace Environment
{
    public abstract class ThrowableObject : MonoBehaviour
    {
        private Rigidbody rb;
        public Transform target;
        public float force = 1000f;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void OnSelectExit()
        {
            if (target == null)
            {
                rb.AddForce(transform.forward * force,ForceMode.Force);
                return;
            }
            rb.AddForce((target.transform.position - transform.position).normalized*force,ForceMode.Force);
        }
    }
}