using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MimicSpace
{
    /// <summary>
    /// This is a very basic movement script, if you want to replace it
    /// Just don't forget to update the Mimic's velocity vector with a Vector3(x, 0, z)
    /// </summary>
    public class Movement : MonoBehaviour
    {
        [Header("Controls")]
        [Tooltip("Body Height from ground")]
        [Range(0.5f, 5f)]
        public float height = 0.8f;
        public float speed = 5f;
        Vector3 velocity = Vector3.zero;
        public float velocityLerpCoef = 4f;
        Mimic myMimic;
        public Transform playerTransform;
        public Vector3 offset;

        private void Start()
        {
            myMimic = GetComponent<Mimic>();
        }

       
        void Update()
        {
            // Calculate the desired position with offset from the player
            Vector3 desiredPosition = playerTransform.position + offset;

            // Calculate the direction and velocity to get to the desired position
            velocity = Vector3.Lerp(velocity, (desiredPosition - transform.position).normalized * speed, velocityLerpCoef * Time.deltaTime);

            // Assigning velocity to the mimic to assure great leg placement
            myMimic.velocity = velocity;

            // Move the GameObject towards the desired position
            transform.position += velocity * Time.deltaTime;

            // Ensure that the GameObject maintains a certain height above the ground
            RaycastHit hit;
            Vector3 destHeight = transform.position;
            if (Physics.Raycast(transform.position + Vector3.up * 5f, -Vector3.up, out hit))
            {
                destHeight.y = hit.point.y + height;
            }
            transform.position = Vector3.Lerp(transform.position, destHeight, velocityLerpCoef * Time.deltaTime);
        }
    }
    }

