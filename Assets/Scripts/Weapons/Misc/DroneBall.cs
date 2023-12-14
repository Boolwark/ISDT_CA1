using System;
using CodeMonkey.Utils;
using DefaultNamespace.ObjectPooling;
using DepthFirstScheduler;
using UnityEngine;

namespace Weapons.Misc
{
    public class DroneBall : MonoBehaviour
    {
        private Rigidbody rb;
        public GameObject spawnEffect;
        public GameObject dronePf;
        private bool activated = false;
        private void Start()
        {
            spawnEffect.SetActive(false);
            rb = GetComponent<Rigidbody>();
        
        }

        public void OnSelectExit()
        {
            if (activated) return;
            rb.AddForce(transform.forward*1000f,ForceMode.Force);
            rb.AddForce(transform.up*1000f,ForceMode.Force);
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
