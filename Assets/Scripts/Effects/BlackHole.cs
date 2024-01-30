using System;
using DG.Tweening;
using UnityEngine;

namespace Effects
{
    public class BlackHoleEffect : MonoBehaviour
    {

        private Vector3 initScale;
        public Vector3 finalScale;
        public float range;
        public LayerMask whatIsEnemy;
        public float duration = 5f;
        public Ease ease;
        private void Start()
        {
            initScale = transform.localScale;
            transform.DOScale(finalScale, duration).SetEase(ease).OnComplete(() =>
            {
                transform.DOScale(initScale, 0);
            });
            foreach (Collider enemy in Physics.OverlapSphere(transform.position, range, whatIsEnemy))
            {
                Debug.Log("Attracting " + enemy.name);
                enemy.transform.DOMove(transform.position,
                    duration);
            }
        }
    }
}