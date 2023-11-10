using System;
using UnityEngine;

namespace Enemy.toxin
{
    using UnityEngine;
    using DG.Tweening;

    public class Whip : MonoBehaviour
    {
        public Transform attackPoint;
        public Transform player;
        public float attackSpeed = 1.0f;
        public float attackDistance = 1.0f;

        private void Start()
        {
            player = GameObject.Find("Player").transform;
        }

        void Update()
        {
            if (Vector3.Distance(transform.position, player.position) <= attackDistance)
            {
                AttackPlayer();
            }
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