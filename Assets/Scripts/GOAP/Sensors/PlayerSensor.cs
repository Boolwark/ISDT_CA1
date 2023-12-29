using System;
using UnityEngine;

namespace GOAP.Sensors
{
    public class PlayerSensor : MonoBehaviour
    {
        public delegate void PlayerEnterEvent(Transform player);
        public delegate void PlayerExitEvent(Vector3 lastKnownPosition);
        public event PlayerEnterEvent OnPlayerEnter;
        public event PlayerExitEvent OnPlayerExit;
        private SphereCollider Collider;
        private void Awake()
        {
            Collider = GetComponent<SphereCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
            {
                OnPlayerEnter?.Invoke(player.transform);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
            {
                OnPlayerExit?.Invoke(player.transform.position);
            }
        }
    }
}