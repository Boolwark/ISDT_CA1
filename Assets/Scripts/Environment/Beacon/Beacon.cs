namespace Environment
{
    using UnityEngine;

    public class Beacon : MonoBehaviour
    {
        public float beaconDuration = 10f; // Duration for the beacon to stay active
        private bool isBeaconActive = false;
        private float beaconTimer = 0f;
        public GameObject lightRay; // Assign a cylinder or any object representing the ray of light

        void Update()
        {
            if (isBeaconActive)
            {
                beaconTimer += Time.deltaTime;
                if (beaconTimer > beaconDuration)
                {
                    DeactivateBeacon();
                }
            }
        }

        public void ToggleBeacon()
        {
            isBeaconActive = !isBeaconActive;
            lightRay.SetActive(isBeaconActive);
        }

        private void DeactivateBeacon()
        {
            isBeaconActive = false;
            beaconTimer = 0f;
            lightRay.SetActive(isBeaconActive);
        }
    }

}