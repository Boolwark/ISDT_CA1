using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons
{
    public class SniperGun : Gun
    {
        public Camera playerCamera;
        public float normalFOV = 60f; // Default field of view
        public float zoomedFOV = 20f; // Field of view when zoomed in
        public InputActionProperty zoomAction;
     
        protected override void Start()
        {
            base.Start();
            playerCamera = Camera.main; // Assumes the main camera is the player's camera
            // ... rest of your start method ...
        }


        public void OnSelectEnter()
        {
          
                Debug.Log("zooming");
                playerCamera.fieldOfView = zoomedFOV;
                
            
        }

        public void OnSelectExit()
        {
            playerCamera.fieldOfView = normalFOV;
        }
    }
}