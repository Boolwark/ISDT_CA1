using UnityEngine;

namespace Environment
{
    public class AlertLight : MonoBehaviour
    {
        public float minIntensity = 0.1f;
        public float maxIntensity = 3f;
        public float pulseSpeed = 1.0f;

        private Light alertLight;
        private float time;

        private void Start()
        {
            alertLight = GetComponent<Light>();
        }

        private void Update()
        {
            time += Time.deltaTime * pulseSpeed;
            float intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.Sin(time) * 0.5f + 0.5f);
            alertLight.intensity = intensity;
        }
    }
}