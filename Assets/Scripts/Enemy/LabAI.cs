using System;
using UnityEngine;

namespace Enemy
{
    public class LabAI : MonoBehaviour
    {
        private bool isFadingIn;
        public DissolveController[] dissolveControllers;
        public GameObject dialog;
        private void Start()
        {
      
        }

        public void AppearAndGiveHint()
        {
            // Check if the object entering the collider is the player
          
                // Start the dissolve effect
                foreach (var dissolveController in dissolveControllers)
                {
                    dissolveController.OnFadeInEnd.AddListener(ShowDialog);
                    dissolveController.StartFadingIn();
                    
                }
                Debug.Log("Giving hint.");
        }

        private void ShowDialog()
        {
            dialog.SetActive(true);
        }
    }
}