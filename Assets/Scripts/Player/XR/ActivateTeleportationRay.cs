using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace Player.XR
{
    public class ActivateTeleportationRay : MonoBehaviour
    {
        public GameObject LeftReticle;
        public GameObject RightReticle;
        private MeshRenderer _leftMeshRenderer;
        private MeshRenderer _rightMeshRenderer;
        private void Start()
        {
            _leftMeshRenderer = LeftReticle.GetComponent<MeshRenderer>();
            _rightMeshRenderer = RightReticle.GetComponent<MeshRenderer>();
        }

        public void  DeactivateReticles()
        {
            _leftMeshRenderer.enabled = false;
            _rightMeshRenderer.enabled = false;
        }
        public void  ActivateReticles()
        {
            _leftMeshRenderer.enabled = true;
            _rightMeshRenderer.enabled = true;
        }
    }
}