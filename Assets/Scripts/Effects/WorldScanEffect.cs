using System;
using DG.Tweening;
using UnityEngine;

namespace Effects
{
    public class WorldScanEffect : MonoBehaviour
    {
        public GameObject scanObj;
        public float effectDuration = 5f;
        public void Activate()
        {
            scanObj.transform.DOScale(scanObj.transform.localScale * 100f, effectDuration).OnComplete(() =>
            {
                Destroy(scanObj);
            });
        }
        

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                Activate();
            }
        }
    }
}