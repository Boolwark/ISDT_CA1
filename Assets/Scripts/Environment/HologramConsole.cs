using System;
using DefaultNamespace.ObjectPooling;
using UnityEngine;

namespace Environment
{
    public class HologramConsole : MonoBehaviour
    {
        public GameObject hologramPf;
        public Transform spawnPoint;
        private bool spawned = false;
        private GameObject currentHologram;
        public void OnButtonClicked()
        {
            if (!spawned)
            {     
                currentHologram = ObjectPoolManager.SpawnObject(hologramPf, spawnPoint.position, spawnPoint.rotation);
            }
            else
            {
                ObjectPoolManager.ReturnObjectToPool(currentHologram);
            }

            spawned = !spawned;


        }
        
    }
}