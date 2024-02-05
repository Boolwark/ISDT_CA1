using DG.Tweening;
using UnityEngine;

namespace Environment
{
    public class Door : MonoBehaviour,IDestructible
    {
        private bool isOpen = false;
        public Ease ease;
        public float duration = 1f;
        public Vector3 closePosition, openPosition;
        public void Toggle()
        {
            if (isOpen)
            {
                transform.DOMove(closePosition, duration
                ).SetEase(ease);
            }
            else
            {
                transform.DOMove(openPosition, duration
                ).SetEase(ease);
            }

            isOpen = !isOpen;

        }

      
        public void OnDestroy()
        {
            Destroy(this.gameObject);
        }
    }
}