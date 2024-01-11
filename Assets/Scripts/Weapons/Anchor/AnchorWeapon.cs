using UnityEngine;

namespace Weapons
{
    public class AnchorWeapon : MonoBehaviour
    {
        public Anchor anchor;
        public void OnActivate()
        {
           anchor.Activate();
        }
    }
}