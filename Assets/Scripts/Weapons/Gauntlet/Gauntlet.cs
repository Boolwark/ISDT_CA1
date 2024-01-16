using DG.Tweening;
using UnityEngine;

namespace Weapons
{
    public class Gauntlet : MonoBehaviour
    {
        public Transform rightHand;
        public LayerMask whatIsReversible;
        public float range;
        public GameObject[] fingers;
        public Vector3 rotation;
        public float clenchDuration=0.5f;
        public void OnActivate()
        {
            transform.parent = rightHand;
            foreach (var fingers in fingers)
            {
                fingers.transform.DORotate(transform.rotation.eulerAngles + rotation, clenchDuration).SetEase(Ease.Linear);
            }

            foreach (var collider in  Physics.OverlapSphere(transform.position,range,whatIsReversible))
            {
                collider.GetComponent<Reversible>().Reverse();
            }

            
        }

        public void OnActivateExit()
        {
            foreach (var fingers in fingers)
            {
                fingers.transform.DORotate(transform.rotation.eulerAngles - rotation, clenchDuration).SetEase(Ease.Linear);
            }
        }
    }
}