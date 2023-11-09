using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class FadeTransition : MonoBehaviour
    {
        public Canvas fadeCanvas;
        public GameObject player;
        public float fadeInDuration = 0.5f;
        public float fadeOutDuration = 0.3f;
        public Vector3 offset;
        // scale up and down this black canvas to block the player's screen, giving the illusion
        // of a fade in
        private void Awake()
        {
            fadeCanvas.enabled = false;
        }

        public void PlayTransition()
        {
            fadeCanvas.enabled = true;
            transform.position = player.transform.position + offset;
            var seq = DOTween.Sequence();
            seq.Append(transform.DOScale(new Vector3(10f, 10f, 10f), fadeInDuration))
                .Append(transform.DOScale(Vector3.zero, fadeOutDuration));
        }
    }
}