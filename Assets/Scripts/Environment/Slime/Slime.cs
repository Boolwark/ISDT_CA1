using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using CrashKonijn.Goap.Classes.References;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;

namespace Environment.Slime
{
    public class Slime : MonoBehaviour
    {
        private bool trapped = false;
        public Material material;
        public float trapDuration = 2f;
        private float timeSinceLastTrap = 0f;
        public float range = 0.1f;
        public float trapCooldown = 2f;
        private Color initColor;
        public Color playerColor = Color.black;
        
        private float updateDestinationRate = 1f;
        public float wanderRadius = 3f;
        private Vector3 CurrentDestination;
        public float timeToMoveToDestination = 3f;
        
        private ActionBasedContinuousMoveProvider playerMP;
        private Transform playerBody;
        private void Start()
        {
            initColor = material.GetVector("_Tint");
            playerMP = FindObjectOfType<ActionBasedContinuousMoveProvider>();
            playerBody = FindObjectOfType<Player.Player>().transform;
            InvokeRepeating(nameof(UpdateDestination),0f,updateDestinationRate);
            InvokeRepeating(nameof(TryToTrapPlayer),0f,trapCooldown);
        }

        private void UpdateDestination()
        {
            Vector3 newPosition = GetRandomPosition();
            CurrentDestination = newPosition;
            transform.DOMove(CurrentDestination, timeToMoveToDestination);
        }

       

       

        private Vector3 GetRandomPosition()
        {
            
                Vector2 random = Random.insideUnitCircle * wanderRadius;
                Vector3 position = transform.position + new Vector3(
                    random.x,
                    0,
                    random.y
                );
            
              
            
        

            return position;
        }

        void TryToTrapPlayer()
        {
            if (Vector3.Distance(playerBody.transform.position, transform.position) > range) return;
            StartCoroutine(TrapPlayer());
        }

      

        private IEnumerator TrapPlayer()
        {
            
     
            var initPos = playerBody.transform.position;
            float timeElapsed = 0f;
            while (timeElapsed <= trapDuration && !trapped)
            {
                timeElapsed += Time.deltaTime;
                Debug.Log("Trapping player");
                playerBody.transform.position = initPos;
                material.SetVector("_Tint",Color.Lerp(initColor, Color.black, timeElapsed));
                yield return null;
            }

            trapped = true;





        }

       
    }
}