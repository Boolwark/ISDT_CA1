using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.XR.Interaction.Toolkit;

namespace Player.XR
{
    public class XRGrabInteractableTwoAttach : XRGrabInteractable
    {
        public Transform leftAttachTransform;
        public Transform rightAttachTransform;
        private Rigidbody playerRB;
        void Start()
        {
            playerRB = GameObject.Find("PlayerBody").GetComponent<Rigidbody>();
            leftAttachTransform = GameObject.Find("LeftHand").transform;
            rightAttachTransform = GameObject.Find("RightHand").transform;
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
        }

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            playerRB.isKinematic = false;
            playerRB.velocity = Vector3.zero;
            base.OnSelectExited(args);
        }
    }
}