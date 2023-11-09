using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class GameButton : MonoBehaviour
    { protected Vector3 initScale;
        public float expandedScaleFactor = 1.2f;
        private void Start()
        {
            initScale = transform.localScale;
        }
        public void OnHoverEnter()
        {
            transform.DOScale(initScale * expandedScaleFactor, 0.5f).SetEase(Ease.InQuad);
        }
        public void OnHoverExit()
        {
            transform.DOScale(initScale, 0.5f).SetEase(Ease.InQuad);
        }
    }
}