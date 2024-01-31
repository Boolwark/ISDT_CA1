using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Effects.Elements
{
    public class ShockEffect : ElementalEffect
    {
        public float shakeStrength;
        public int vibration;
        public float interval;

        public override void Activate()
        {
            base.Activate();
            if (base.hitEnemy)
            {
                StartCoroutine(ShakeCoroutine());
                base.sm.TakeDamage(damage);
            }

        }
        void Start()
        {
            Activate();
        }

        
        
        private IEnumerator ShakeCoroutine()
        {
            Vector3 originalPosition = transform.position;
            for (int i = 0; i < vibration; i++)
            {
                // Generate random offsets for the camera's position
                Vector3 randomOffset = Random.insideUnitSphere * shakeStrength;

                // Apply the offset to the camera's local position
                transform.localPosition = originalPosition + randomOffset;

                // Wait for a short interval before the next vibration cycle
                yield return new WaitForSeconds(interval);
            }

            // Reset the camera's position to the original
            transform.localPosition = originalPosition;
        }
    }
}