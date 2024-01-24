using System;
using System.Collections.Generic;
using Ability;
using DG.Tweening;
using UI;
using UI.UI.DefaultNamespace.GameUI;
using UnityEngine;

namespace Weapons
{
    public class Gauntlet : MonoBehaviour
    {
        public Transform rightHand;
        public LayerMask whatIsReversible;
        public float range,launchForce;
        public GameObject[] fingers;
        public Vector3 rotation;
        public float clenchDuration=0.5f;
        private Ability.Ability oldAbility;
        public Ability.Ability UltrahandGrappleAbility;
        private AbilityHolder _abilityHolder;
        private HealEffect _healEffect;
        private List<TelekineticProjectile> heldProjectiles = new();
        private void Start()
        {
            _abilityHolder = FindObjectOfType<AbilityHolder>();
            _healEffect = FindObjectOfType<HealEffect>();
        }

     
        public void OnSelect()
        {
            _abilityHolder.ability = UltrahandGrappleAbility;
       
        }
        public void OnActivate()
        {
           

            
        }

        public void OnActivateExit()
        {
            foreach (var fingers in fingers)
            {
                fingers.transform.DORotate(transform.rotation.eulerAngles - rotation, clenchDuration).SetEase(Ease.Linear);
            }
        }
    }
}