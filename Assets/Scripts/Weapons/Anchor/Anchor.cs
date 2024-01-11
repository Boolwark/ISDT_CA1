using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Weapons
{
    public class Anchor : MonoBehaviour
    {
        public Bullet bullet;
        public GameObject trailObj;
        private Rigidbody rb;
        public float flySpeed = 20f;
        private bool activated = false;

        private void Start()
        {
            bullet = GetComponent<Bullet>();
            rb = GetComponent<Rigidbody>();
            trailObj.SetActive(false);
            bullet.enabled = false;
        }

        public void Activate()
        {
            activated = true;
            bullet.enabled = true;
            trailObj.SetActive(true);
            rb.isKinematic = true;
            
        }

        private void Update()
        {
            if (activated)
            {
                transform.Translate(-transform.forward *flySpeed*Time.deltaTime);
            }
        }
    }
}