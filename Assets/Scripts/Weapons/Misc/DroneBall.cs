using System;
using CodeMonkey.Utils;
using DefaultNamespace.ObjectPooling;
using Environment;
using UnityEngine;

namespace Weapons.Misc
{
    public class DroneBall : ThrowableObject
    {

        public GameObject spawnEffect;
        public GameObject dronePf;
        private bool activated = false;
        private void Start()
        {
            spawnEffect.SetActive(false);
        
        
        }

        public new void OnSelectExit()
        {
            if (activated) return;
           base.OnSelectExit();
            PlaySpawnEffect();
            SpawnDrone();
        }

        public void SpawnDrone()
        {
            ObjectPoolManager.SpawnObject(dronePf,transform.position,Quaternion.identity);
            activated = true;
        }

        private void PlaySpawnEffect()
        {
            spawnEffect.SetActive(true);
            FunctionTimer.Create(() => { spawnEffect.SetActive(false); }, 1f);
        }
    }
    }
