using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Weapons.XR
{
    public class XROffsetGrabInteractable : XRGrabInteractable
    {
        private void Start()
        {
            if (!attachTransform)
            {
                GameObject attachPoint = new GameObject("Offset Grab Pivot");
                attachPoint.transform.SetParent(transform,false);
                attachTransform = attachPoint.transform;
            }
        }

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            attachTransform.position = args.interactorObject.transform.position;
            attachTransform.rotation = args.interactorObject.transform.rotation;
            base.OnSelectEntered(args);
        }
    }
}