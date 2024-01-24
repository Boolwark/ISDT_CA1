using System.Collections.Generic;
using DG.Tweening;
using UI.UI.DefaultNamespace.GameUI;
using UnityEngine.Rendering.Universal;
using Weapons;

namespace Ability.Abilities
{
    using System;
    using UnityEngine;
    using UnityEngine.XR.Interaction.Toolkit;

    namespace Ability.Abilities
    {
        [CreateAssetMenu]
        public class ReverseAbility : global::Ability.Ability
        {
            public float launchForce;
            private List<TelekineticProjectile> heldProjectiles = new();
            public float range = 5f;

            public LayerMask whatIsReversible;
            private HealEffect _healEffect;
            public override void Activate(GameObject parent)
            {
                if (_healEffect == null)
                {
                    _healEffect = parent.GetComponentInChildren<HealEffect>();
                }
                _healEffect.TriggerVignetteEffect();
                foreach (var heldProjectile in heldProjectiles)
                {
                    heldProjectile.Launch(launchForce);
                }
                heldProjectiles.Clear();
              

                foreach (var collider in  Physics.OverlapSphere(parent.transform.position,range,whatIsReversible))
                {
                    if (collider.TryGetComponent(out Reversible reversible))
                    {
                        reversible.Reverse();
                    }
                    if (collider.TryGetComponent(out TelekineticProjectile tp))
                    {
                        if (tp.isLaunching) continue;
                        tp.Activate(parent.transform);
                        heldProjectiles.Add(tp);
                    }
       
                }
            }

            public override void BeginCooldown(GameObject parent)
            {
                
            }
        }
    }
}