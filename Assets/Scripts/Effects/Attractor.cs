using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Scripts.Effects
{
    public class Attractor : MonoBehaviour
    {
        public Rigidbody rb;
        public static List<Attractor> attractors;
        const float G = 667.4f;

        private void OnEnable()
        {
            if (attractors == null)
            {
                attractors = new();
            }

            attractors.Add(this);

        }

        private void OnDisable()
        {
            attractors.Remove(this);
        }

        void Attract(Attractor otherAttractor)
        {
            Vector3 dir = transform.position - otherAttractor.transform.position;
            float force = G * (rb.mass * otherAttractor.rb.mass) / dir.sqrMagnitude;
            otherAttractor.rb.AddForce(force * dir.normalized,ForceMode.Acceleration);
        }

        void FixedUpdate()
        {
            foreach (Attractor attractor in attractors)
            {
                if (attractor.transform == transform) continue;
                Attract(attractor);
            }
        }
    }
}