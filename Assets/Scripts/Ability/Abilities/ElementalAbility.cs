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
        private Vector3 aoeInitScale;
        public float damage = 100f;
        public Color effectColor;
        public Ease aoeEffectEase;
        public float radius;
        private VignetteEffect EffectUI;
        public GameObject AOEPf;
        public float duration;
        public LayerMask whatIsEnemy;
        private bool isActive = false;
        public GameObject elementalEffectPf;
        public override void Activate(GameObject parent)
        {
            if (isActive) return;
            if (EffectUI == null)
            {
                EffectUI = parent.GetComponentInChildren<VignetteEffect>();
                EffectUI.ChangeColor(effectColor);
            }

            isActive = true;
            Debug.Log("activate ability");
            EffectUI.TriggerVignetteEffect();
            aoeInitScale = AOEPf.transform.localScale;
            PlayAOEEffect(parent.transform);
           
     
              

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
            isActive = false;
        }

        private void PlayAOEEffect(Transform parent)
        {
            var aoe = ObjectPoolManager.SpawnObject(AOEPf, parent.transform.position, parent.transform.rotation);
            aoe.transform.DOScale(AOEPf.transform.localScale * radius,duration).SetEase(aoeEffectEase).OnComplete(
                () =>
                {
                    aoe.transform.localScale = aoeInitScale;
                    ObjectPoolManager.ReturnObjectToPool(aoe);
                });
        }
    }
}