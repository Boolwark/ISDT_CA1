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
      
            Vector3 desiredPosition = playerTransform.position + offset;

            
            velocity = Vector3.Lerp(velocity, (desiredPosition - transform.position).normalized * speed, velocityLerpCoef * Time.deltaTime);
            
            myMimic.velocity = velocity;


            transform.position += velocity * Time.deltaTime;
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

