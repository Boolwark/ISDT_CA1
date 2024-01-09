using System;
using TMPro;
using UnityEngine;
using Util.DialogSystem;

namespace Enemy
{
    public class LabAI : MonoBehaviour
    {
        private bool isFadingIn;
        public DissolveController[] dissolveControllers;
        public TMP_Text textUI;
        public DialogController dialogController;
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
            dialogController.PlayDialog(textUI);
        }
    }
}