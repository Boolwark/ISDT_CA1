using UnityEngine;
using UnityEngine.InputSystem;
using Weapons.Sword;
using Weapons.TwoHanded.Weapons.TwoHanded;

namespace Weapons.TwoHanded
{
    public class Axe : TwoHandedItem
    {
        void Update()
        {
            SliceObject.enabled = GetGrabState() == GrabState.TWOHAND;
        }
    }
}