using System;
using DG.Tweening;
using UnityEngine;

namespace Weapons
{
    public class Reversible : MonoBehaviour
    {
        public float duration = 5f;
        public Ease ease=Ease.Linear;
        public bool localTween = false;
        private Material initMaterial;
        public Vector3 targetPosition;
        public Vector3 targetRotation;
        private MeshRenderer _meshRenderer;
        public Material reversingMaterial;

        private void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            initMaterial = _meshRenderer.material;
        }

        public void Reverse()
        {
            _meshRenderer.material = reversingMaterial;
            if (!localTween)
            {
                transform.DOMove(targetPosition, duration).SetEase(ease);
                transform.DORotate(targetRotation, duration).SetEase(ease).OnComplete(() =>
                {
                    _meshRenderer.material = initMaterial;});
            }

            else
            {
                transform.DOLocalMove(targetPosition, duration).SetEase(ease);
                transform.DOLocalRotate(targetRotation, duration).SetEase(ease).OnComplete(() =>
                {
                    _meshRenderer.material = initMaterial;});
            }

        }
    }
}