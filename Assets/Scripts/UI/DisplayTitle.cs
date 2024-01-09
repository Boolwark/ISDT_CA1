using CodeMonkey.Utils;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class DisplayTitle : MonoBehaviour
    {

        public Vector3 intendedScale;
        public float animDuration = 5f;
        public float titleStayDuration = 1f;
        public Ease easeType;

        private void Start()
        {

            transform.DOScale(intendedScale, animDuration).SetEase(easeType).OnComplete(() =>
            {
                Debug.Log("Scaling to zero");
                transform.DOScale(Vector3.zero, animDuration / 2);
            });

        }
    }
}