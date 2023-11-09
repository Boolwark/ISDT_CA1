using System;
using UnityEngine;

namespace Enemy
{
    public class SerpentAgent : MonoBehaviour
    {
     
        private bool isFadingIn;
        public DissolveController[] dissolveControllers;
        private void Start()
        {
         
        }
        private void OnTriggerEnter(Collider other)
        {
            // Check if the object entering the collider is the player
            if (other.CompareTag("Player"))
            {
                // Start the dissolve effect
                foreach (var dissolveController in dissolveControllers)
                {
                    dissolveController.StartFadingIn();
                }
             
            }
        }
    }
}