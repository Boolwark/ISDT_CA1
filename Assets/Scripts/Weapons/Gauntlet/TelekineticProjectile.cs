using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Weapons
{
    public class TelekineticProjectile : MonoBehaviour
    {
        private bool isLevitating = false;
        private Transform target;
        public float rotSpeed = 10f;
        public Material levitatingMaterial;
        private Material initMaterial;
        private MeshRenderer _meshRenderer;
        public float launchDuration = 3f;
        private Rigidbody rb;
        public bool isLaunching = false;
        public Vector3 offset;

        private IEnumerator LaunchCoroutine()
        {
            isLaunching = true;
            yield return new WaitForSeconds(launchDuration);
            isLaunching = false;
        }
        public void Activate(Transform target)
        {
            transform.parent = target;
            isLevitating = true;
            this.target = target;
            rb.isKinematic = true;
            transform.DOMove(target.position + offset, 1f).SetEase(Ease.Linear);
            _meshRenderer.material = levitatingMaterial;

        }

        public void Launch(float launchForce)
        {
            StartCoroutine(LaunchCoroutine());
            isLevitating = false;
            _meshRenderer.material = initMaterial;
            rb.isKinematic = false;
            rb.AddForce(rb.transform.forward * launchForce,ForceMode.Force);
        }

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            _meshRenderer = GetComponent<MeshRenderer>();
            initMaterial = _meshRenderer.material;
        }
        private void Update()
        {
            if (isLevitating)
            {
                transform.RotateAround(target.position,target.forward,Time.deltaTime * rotSpeed);
            }
        }
    }
}