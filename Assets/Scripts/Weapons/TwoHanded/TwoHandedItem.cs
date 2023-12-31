using Weapons.Sword;

namespace Weapons.TwoHanded
{
   
     using UnityEngine;
     using UnityEngine.InputSystem;


     namespace Weapons.TwoHanded
     {
         //base class returns status of player's grip
         /// <summary>
         /// base class returns status of player's grip, to be derived from items requiring two hands to function. 
         /// </summary>
         public class TwoHandedItem : MonoBehaviour
         {
             public InputActionReference leftHandGrabAction;
             public InputActionReference rightHandGrabAction;
             public SliceObject SliceObject;
             public enum GrabState
             {
                 ONEHAND,
                 TWOHAND
             }
             public GrabState GetGrabState()
             {
                 var leftGrabValue = leftHandGrabAction.action.ReadValue<float>();
                 Debug.Log("Left grab value is"  + leftGrabValue);
                 var rightGrabValue = rightHandGrabAction.action.ReadValue<float>();
                 Debug.Log("Right grab value is"  + rightGrabValue);
                 if (leftGrabValue> 0.5f &&
                     rightGrabValue > 0.5f) return GrabState.TWOHAND;
                 return GrabState.ONEHAND;
             }
             
         }
     }
}