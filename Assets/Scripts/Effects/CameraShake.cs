namespace Effects
{
    namespace DefaultNamespace.GameUI
    {
        using UnityEngine;
        using System.Collections;
        using DG.Tweening;

        public class CameraShake : MonoBehaviour
        {
            public float shakeDuration = 0.5f;     // Duration of the camera shake
            public float shakeStrength = 0.7f;     // Strength of the shake
            public int vibration = 10;             // Number of vibration cycles during shake
            public float interval = 0.02f;         // Time between vibration cycles

            private Vector3 originalPosition;      // Store the original position of the camera

            private void Start()
            {
                originalPosition = transform.localPosition;
            }

            public void ShakeCamera()
            {
                StartCoroutine(ShakeCoroutine());
            }

            private IEnumerator ShakeCoroutine()
            {
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
}