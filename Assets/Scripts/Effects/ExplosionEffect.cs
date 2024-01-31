using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Effects
{
    public class ExplosionEffect : MonoBehaviour
    {
        private void Start()
        {
            ExplosionManager.Instance.SpawnExplosion(transform.position,transform.rotation);
        }
    }
}