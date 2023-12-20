
using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.XR.Interaction.Toolkit;

namespace Player.XR
{
    public class XRGrabInteractableTwoAttach : XRGrabInteractable
    {
        public bool enableMovement = false;
        public Transform leftAttachTransform;
        public Transform rightAttachTransform;
        private Rigidbody playerRB;
        private bool isHeld;
        private ContinuousMoveProviderBase _continuousMoveProviderBase;
        private float initialSpeed;
        void Start()
        {
            playerRB = GameObject.Find("PlayerBody").GetComponent<Rigidbody>();
            leftAttachTransform = GameObject.Find("LeftHand").transform;
            rightAttachTransform = GameObject.Find("RightHand").transform;
            _continuousMoveProviderBase = FindObjectOfType<ContinuousMoveProviderBase>();
            initialSpeed = _continuousMoveProviderBase.moveSpeed;
        }

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            playerRB.isKinematic = true;
            playerRB.velocity = Vector3.zero;
            if (args.interactableObject.transform.CompareTag("LeftHand"))
            {
                attachTransform = leftAttachTransform;
             
            }
            else if (args.interactableObject.transform.CompareTag("RightHand"))
            {
                attachTransform = rightAttachTransform;
            }
            transform.position = attachTransform.position;
            base.OnSelectEntered(args);
            isHeld = true;
        }

        private void Update()
        {
            if (isHeld)
            {
                // A fix to player clipping through walls. When they hold a weapon, they must become stationary. 
                if (!enableMovement)
                {
                    playerRB.velocity = Vector3.zero;
                    _continuousMoveProviderBase.moveSpeed = 0;
                }
             
            }
            else
            {
                _continuousMoveProviderBase.moveSpeed = initialSpeed;
            }
        }

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            playerRB.isKinematic = false;
            playerRB.velocity = Vector3.zero;
            base.OnSelectExited(args);
            isHeld = false;
        }
    }
}