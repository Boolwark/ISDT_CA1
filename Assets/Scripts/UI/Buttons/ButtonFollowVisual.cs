using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace UI
{
    public class ButtonFollowVisual : MonoBehaviour
    {
        private XRBaseInteractable interactable;
        public Transform visualTarget;
        public float resetSpeed=5f;
        private bool isFollowing = false;
        public Vector3 localAxis;
        private Vector3 offset;
        private Vector3 initLocalPos;
        private Transform pokeAttachTransform;
        public float followAngleThreshold;
        private bool freeze;
        public UnityEvent OnButtonPressed;
        private void Start()
        {
            interactable = GetComponent<XRBaseInteractable>();
            interactable.hoverEntered.AddListener(Follow);
            interactable.hoverExited.AddListener(Reset);
            interactable.selectEntered.AddListener(Freeze);
            initLocalPos = visualTarget.localPosition;
        }

        public void Follow(BaseInteractionEventArgs hover)
        {
            
          
                var interactor = (XRBaseInteractor) hover.interactorObject;
                isFollowing = true;
                pokeAttachTransform = interactor.attachTransform;
                offset = visualTarget.position - pokeAttachTransform.position;
                float pokeAngle = Vector3.Angle(offset, visualTarget.TransformDirection(localAxis));
                if (pokeAngle < followAngleThreshold)
                {
                    isFollowing = true;
                    freeze = false;
                }

        }

        public void Freeze(BaseInteractionEventArgs hover)
        {
            freeze = true;
        }

        private void Update()
        {
            if (freeze)
            {
                return;
            }
            if (isFollowing)
            {
                Vector3 localTargetPosition = visualTarget.InverseTransformPoint(pokeAttachTransform.position + offset);
                Vector3 constrainedLocalTargetPosition = Vector3.Project(localTargetPosition, localAxis);
                visualTarget.position = visualTarget.TransformPoint(constrainedLocalTargetPosition);
            }
            else
            {
                visualTarget.localPosition =
                    Vector3.Lerp(visualTarget.localPosition, initLocalPos, Time.deltaTime * resetSpeed);
            }
        }

        public void Reset(BaseInteractionEventArgs hover)
        {
            isFollowing = false;
            freeze = false;
            OnButtonPressed?.Invoke();
        }
    }
}