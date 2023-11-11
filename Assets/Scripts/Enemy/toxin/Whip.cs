using System;
using System.Collections;
using UnityEngine;

namespace Enemy.toxin
{
    using UnityEngine;
    using DG.Tweening;

    public class Whip : MonoBehaviour
    {
        public float whipForce = 500f, whipRadius = 5f;
        public Transform attackPoint;
        public Transform player;
        public float attackSpeed = 1.0f;
        public float attackDistance = 1.0f;

        private void Start()
        {
            player = GameObject.Find("Player").transform;
        }

        void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Player"))
            {
          WhipEffect(collider.GetComponent<Rigidbody>());
            }
        }

        private void WhipEffect(Rigidbody rb)
        {
            rb.AddExplosionForce(whipForce,-rb.transform.forward,whipRadius);
        }

        void AttackPlayer()
        {
            // Move attackPoint towards the player using DOTween
            attackPoint.DOMove(player.position, attackSpeed).SetEase(Ease.InOutSine);

            // Optionally, reset the position after a delay
            DOVirtual.DelayedCall(attackSpeed, () => {
                attackPoint.DOMove(transform.position, attackSpeed);
            });
        }
    }

}