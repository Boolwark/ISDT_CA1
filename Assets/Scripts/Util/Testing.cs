using System;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace.ObjectPooling
{
    public class Testing : MonoBehaviour
    {
        public float coolDown = 1f;
        private float cdTimer = 0f;
        public GameObject cubePrefab;
        public string spawnSFXName;
        private void Update()
        {
            if (cdTimer<=0f)
            {
                ObjectPoolManager.SpawnObject(cubePrefab, transform.position, Quaternion.identity);
                AudioManager.Instance.PlaySFX(spawnSFXName);
                cdTimer = coolDown;
            }

            cdTimer -= Time.deltaTime;
        }
    }
}