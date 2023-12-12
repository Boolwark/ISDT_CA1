using System;
using UnityEngine;

namespace Environment
{
    public class Barricade : MonoBehaviour
    {
        private Vector3 playerLastPos;
        private Transform player;
        public float maxOffset = 0.5f;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerLastPos = player.transform.position;
            
        }

        private void LateUpdate()
        {
            if (player.transform.position.x < transform.position.x ||
                player.transform.position.z < transform.position.z)
            {
                player.transform.position = playerLastPos;
            }
            else
            {
                playerLastPos = player.transform.position;
            }
        }
    }
}