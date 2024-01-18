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
        private Vector3 initPosition, initRotation;
        public Vector3 targetPosition;
        public Vector3 targetRotation;
        private MeshRenderer _meshRenderer;
        public Material[] reverseMaterials;
        private bool reached;
        private void Start()
        {
            initPosition = transform.position;
            initRotation = transform.rotation.eulerAngles;
            _meshRenderer = GetComponent<MeshRenderer>();
            initMaterial = _meshRenderer.material;
        }

        public void Reverse()
        {
            Material reversingMaterial = reached ? reverseMaterials[0] : reverseMaterials[1];
            _meshRenderer.material = reversingMaterial;
            if (!reached)
            {
                if (!localTween)
                {
                    transform.DOMove(targetPosition, duration).SetEase(ease);
                    transform.DORotate(targetRotation, duration).SetEase(ease).OnComplete(() =>
                    {
                        _meshRenderer.material = initMaterial;
                        reached = true;
                    });
                }

                else
                {
                    transform.DOLocalMove(targetPosition, duration).SetEase(ease);
                    transform.DOLocalRotate(targetRotation, duration).SetEase(ease).OnComplete(() =>
                    {
                        _meshRenderer.material = initMaterial;
                        reached = true;
                    });
                }
            }

            else
            {
                if (!localTween)
                {
                    transform.DOMove(initPosition, duration).SetEase(ease);
                    transform.DORotate(initRotation, duration).SetEase(ease).OnComplete(() =>
                    {
                        _meshRenderer.material = initMaterial;
                        reached = false;
                    });
                }

                else
                {
                    transform.DOLocalMove(initPosition, duration).SetEase(ease);
                    transform.DOLocalRotate(initRotation, duration).SetEase(ease).OnComplete(() =>
                    {
                        _meshRenderer.material = initMaterial;
                        reached = false;
                    });
                } 
            }


        }
    }
}