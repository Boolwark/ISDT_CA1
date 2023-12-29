using UnityEngine;
using UnityEngine.InputSystem;
using Weapons.Sword;

namespace Weapons.TwoHanded
{
    public class Axe : MonoBehaviour
    {
        public InputActionReference leftHandGrabAction;
        public InputActionReference rightHandGrabAction;
        public SliceObject SliceObject;
        public enum GrabState
        {
            ONEHAND,
            TWOHAND
        }
        private GrabState GetGrabState()
        {
            var leftGrabValue = leftHandGrabAction.action.ReadValue<float>();
            Debug.Log("Left grab value is"  + leftGrabValue);
            var rightGrabValue = rightHandGrabAction.action.ReadValue<float>();
            Debug.Log("Right grab value is"  + rightGrabValue);
            if (leftGrabValue> 0.5f &&
                rightGrabValue > 0.5f) return GrabState.TWOHAND;
            return GrabState.ONEHAND;
        }

        void Update()
        {
            SliceObject.enabled = GetGrabState() == GrabState.TWOHAND;
        }
    }
}