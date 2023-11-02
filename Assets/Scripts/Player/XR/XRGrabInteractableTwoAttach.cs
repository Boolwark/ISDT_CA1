using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Player.XR
{
    public class XRGrabInteractableTwoAttach : XRGrabInteractable
    {
        public Transform leftAttachTransform;
        public Transform rightAttachTransform;
        void Start()
        {
            leftAttachTransform = GameObject.Find("LeftHand").transform;
            rightAttachTransform = GameObject.Find("RightHand").transform;
        }

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
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
    }
}