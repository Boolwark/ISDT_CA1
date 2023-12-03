using DG.Tweening;
using UnityEngine;

namespace Effects
{
    public class FadeInOutEffect : MonoBehaviour
    {
        public GameObject fadeInCanvas;
        public float fadeInDuration, fadeOutDuration;
        public void PlayEffect()
        {

            fadeInCanvas.transform.DOScale(new Vector3(5f, 5f, 5f), fadeInDuration).SetEase(Ease.InBounce).OnComplete(() =>
            {
                fadeInCanvas.transform.DOScale(Vector3.zero, fadeOutDuration).SetEase(Ease.OutBounce);
            });
        }
    }
}