using System;
using UnityEngine;

namespace Weapons.Misc
{
    public class Flashlight : MonoBehaviour
    {
        public Light light;
        private bool isActive = false;
        private float intensity = 0f;

        private void Start()
        {
            intensity = light.intensity;
        }

        public void ToggleLight()
        {
            light.intensity = isActive? intensity:0f;
            isActive = !isActive;
        }
    }
}