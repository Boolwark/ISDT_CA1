using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Environment.Animations
{
    public class PlatformEntryAnimation : MonoBehaviour
    {
        public float animDuration = 5f;
        public Ease easeType;
        public Vector3 targetPosition;
        public Vector3 targetRotation;
        public UnityEvent OnAnimationComplete;

        private void Start()
        {
            transform.DOMove(targetPosition, animDuration
            ).SetEase(easeType);
            transform.DORotate(targetRotation, animDuration).SetEase(easeType);
            OnAnimationComplete?.Invoke();
        }
    }
}