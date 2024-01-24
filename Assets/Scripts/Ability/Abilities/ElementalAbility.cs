using System;
using System.Collections.Generic;
using DefaultNamespace.ObjectPooling;
using DG.Tweening;
using Effects.Elements;
using UI.UI.DefaultNamespace.GameUI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.Interaction.Toolkit;

namespace Ability.Abilities
{
    [CreateAssetMenu]
    public class ElementalAbility : global::Ability.Ability
    {
        public float damage = 100f;
     
        public Ease aoeEffectEase;
        public float radius;
        private VignetteEffect EffectUI;
        public GameObject AOEPf;
        public float duration;
        public LayerMask whatIsEnemy;
        public GameObject elementalEffectPf;
        public override void Activate(GameObject parent)
        {
            if (EffectUI == null)
            {
                EffectUI = parent.GetComponentInChildren<VignetteEffect>();
                EffectUI.ChangeColor(Color.cyan);
            }
            EffectUI.TriggerVignetteEffect();
            PlayFreezeAOEEffect(parent.transform);
           
     
              

            foreach (var collider in  Physics.OverlapSphere(parent.transform.position,radius,whatIsEnemy))
            {
                var elementalEffect = ObjectPoolManager.SpawnObject(elementalEffectPf, collider.transform.position,
                    collider.transform.rotation);
                var effect = elementalEffect.GetComponent<ElementalEffect>();
                effect.transform.parent = collider.transform;
                effect.damage = damage;
             
            }
        }

        public override void BeginCooldown(GameObject parent)
        {
         
        }

        private void PlayFreezeAOEEffect(Transform parent)
        {
            var freezeAOE = ObjectPoolManager.SpawnObject(AOEPf, parent.transform.position, parent.transform.rotation);
            freezeAOE.transform.DOScale(freezeAOE.transform.localScale * radius,duration).SetEase(aoeEffectEase).OnComplete(
                () =>
                {
                    ObjectPoolManager.ReturnObjectToPool(freezeAOE);
                });
        }
    }
}