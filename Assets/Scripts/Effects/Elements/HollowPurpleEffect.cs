using DG.Tweening;
using Scenes.Scripts.Effects;
using Unity.VisualScripting;
using UnityEngine;

namespace Effects.Elements
{
    public class HollowPurpleEffect : ElementalEffect
    {
        private Attractor _attractor;
        public Ease shrinkEase;
        public float duration = 5f;
        public override void Activate()
        {
            base.Activate();
            if (base.hitEnemy)
            {
                base.sm.TakeDamage(damage);
            }
        }
        protected new void DeathEffect()
        {
            transform.DOScale(Vector3.zero, duration).SetEase(shrinkEase);
        }

        void Start()
        {
            Activate();
        }
    }
}