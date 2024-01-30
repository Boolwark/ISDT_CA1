using System;
using DG.Tweening;
using UnityEngine;
using Util;

namespace Weapons.PropellerGun
{
    public class AttractorEffect : MonoBehaviour
    {

        private bool attracting = false;
        public float range = 10f, speed = 20f;
        public float attractSpeed = 5f;
        public LayerMask whatIsEnemy;
        public float angle;
        public ParticleSystem[] chargeEffects;
        public Material effectMaterial;
        private Material initMaterial;
        public float manaCost;
        private MeshRenderer _renderer;
        
        private void Start()
        {
            _renderer = GetComponent<MeshRenderer>();
            initMaterial = _renderer.material;
        }

        public void Activate()
        {
            if (!ManaManager.Instance.HasEnoughMana(manaCost))
            {
                Debug.Log("Insufficient mana");
                return;
            }
            ManaManager.Instance.ChangeMana(-manaCost);
            attracting = true;
            AttractEnemies();
        }
        public void OnSelectExit()
        {
            attracting = false;
            Stop();
        }

        private void Stop()
        {
            if (!attracting)
            {
                foreach (var chargeEffect in chargeEffects)
                {
                    chargeEffect.Stop();
                }

                _renderer.material = initMaterial;
            }
        }

        private void AttractEnemies()
        {

            _renderer.material = effectMaterial;
            foreach (var chargeEffect in chargeEffects)
            {
                chargeEffect.Play();
            }
            foreach (Collider enemy in Physics.OverlapSphere(transform.position, range, whatIsEnemy))
            {
                Debug.Log("Attracting " + enemy.name);
                if (Vector3.Angle(enemy.transform.position, transform.position) > angle) continue;
                enemy.transform.DOMove(transform.position,
                    Vector3.Distance(transform.position, enemy.transform.position) / attractSpeed);
            };
        }
    }
}