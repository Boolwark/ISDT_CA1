using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
namespace UI
{
   



    namespace DefaultNamespace.GameUI
    {
        using UnityEngine;
        using DG.Tweening;

        public class DamageEffect : MonoBehaviour
        {
            public float vignetteDuration = 1.0f;
            public float targetIntensity = 0.5f;
            private VolumeProfile volumeProfile;// Intensity to animate the vignette to
            public Volume volume;
            private float initialIntensity;
            private UnityEngine.Rendering.Universal.Vignette vignette; // Store the initial intensity value

            private void Start()
            {
                volumeProfile = volume.profile;
            }

            private void Update()
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Damaging player");
                    TriggerVignetteEffect();
                }
            }

            public void TriggerVignetteEffect()
            {
                // Ensure the Global Post Process Volume component is assigned in the Inspector
                
                if (!volumeProfile) return;

                UnityEngine.Rendering.Universal.Vignette vignette;

                if (!volumeProfile.TryGet(out vignette)) return;

                vignette.intensity.Override(0.5f);
                this.vignette = vignette;
                StartCoroutine(ResetVignetteIntensity());
            }

            private IEnumerator ResetVignetteIntensity()
            {
                yield return new WaitForSeconds(vignetteDuration);
                this.vignette.intensity.Override(0.0f);
                // Create a Tween to reset the Vignette intensity back to its initial value
                //DOTween.To(() => initialIntensity, x => vignette.intensity.value = x, initialIntensity, vignetteDuration);
            }
        }
    }
}