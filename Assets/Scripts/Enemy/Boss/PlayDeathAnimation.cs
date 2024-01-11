using System;
using DG.Tweening;
using UnityEngine;

namespace Enemy.Boss
{
    public class PlayDeathAnimation : MonoBehaviour
    {
        private bool playing = false;
        public float rotateSpeed = 20f;
        public float fallSpeed = 20f;
        public GameObject pf;
        private GameObject instance;
        public void Activate()
        {
            playing = true;
            instance = Instantiate(pf);

        }

        private void Update()
        {
            if (playing && instance != null)
            {
                instance.transform.Rotate(transform.forward, Time.deltaTime * rotateSpeed);
                instance.transform.Translate(-transform.up + transform.forward * Time.deltaTime * fallSpeed);
            }
        }
    }
}