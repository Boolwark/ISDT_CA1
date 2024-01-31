using System;
using UnityEngine;
using Weapons.Magazines.Weapons.Magazines;

namespace Weapons.Magazines
{
    public class MagazineHolder : MonoBehaviour
    {
        public LayerMask whatIsGun;
        public Magazine Magazine;
        public bool isUsed;
        private BoxCollider boxCollider;

        private void Start()
        {
            boxCollider = GetComponent<BoxCollider>();
        }

        public void OnSelectExit()
        {
            foreach (var gunCollider in Physics.OverlapSphere(transform.position,Magazine.attachRange,whatIsGun))
            {
                if (gunCollider.TryGetComponent(out Gun gun))
                {
                    Debug.Log("Inserting magazine");
                    gun.InsertMagazine(this);
                    return;
                }
            }
        }

        public void OnAttached()
        {
            //disable grabbing
            isUsed = true;
            boxCollider.isTrigger = true;
        }
    }
}