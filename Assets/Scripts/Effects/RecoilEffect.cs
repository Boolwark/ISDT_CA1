using Effects.DefaultNamespace.GameUI;
using UnityEngine;

namespace Effects
{
    public class RecoilEffect : MonoBehaviour
    {
        public CameraShake cameraShake;

        public void Activate(float shakeStrength,int nVibrations,float shakeDuration)
        {
            cameraShake.shakeStrength = shakeStrength;
            cameraShake.vibration = nVibrations;
            cameraShake.shakeDuration = shakeDuration;
            cameraShake.ShakeCamera();
        }
    }
}