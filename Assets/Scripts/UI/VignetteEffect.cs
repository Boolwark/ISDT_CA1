using UnityEngine;

namespace UI
{
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
  
          public class VignetteEffect : MonoBehaviour
          {
              public float duration = 1.0f;
              public float targetIntensity = 0.5f;
              private VolumeProfile volumeProfile;// Intensity to animate the vignette to
              public Volume volume;
              private float initialIntensity;
              private Color initColor = Color.red; 
              private Color currentColor = Color.green;

              public void ChangeColor(Color newColor)
              {
                  currentColor = newColor;
              }
              private UnityEngine.Rendering.Universal.Vignette vignette; // Store the initial intensity value
  
              private void Start()
              {
                  volumeProfile = volume.profile;
              }
  
              private void Update()
              {
                 
              }
  
              public void TriggerVignetteEffect()
              {
                  // Ensure the Global Post Process Volume component is assigned in the Inspector
                  
                  if (!volumeProfile) return;
  
                  UnityEngine.Rendering.Universal.Vignette vignette;
  
                  if (!volumeProfile.TryGet(out vignette)) return;
  
                  vignette.intensity.Override(0.5f);
                  vignette.color.Override(currentColor);
                  this.vignette = vignette;
                  StartCoroutine(ResetVignetteIntensity());
              }
  
              private IEnumerator ResetVignetteIntensity()
              {
                  yield return new WaitForSeconds(duration);
                  this.vignette.intensity.Override(0.0f);
                  vignette.color.Override(initColor);
                  // Create a Tween to reset the Vignette intensity back to its initial value
                  //DOTween.To(() => initialIntensity, x => vignette.intensity.value = x, initialIntensity, vignetteDuration);
              }
          }
      }
  }
}