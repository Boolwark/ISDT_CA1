using System;
using CodeMonkey.Utils;
using Unity.VisualScripting;
using UnityEngine;

namespace Util
{
    public class LootCrateManager : MonoBehaviour
    {
        public GameObject lootCratePf;
        public float dropHeight;
        public float timeToNextDrop = 0f;
        public float dropCooldown = 30f;//every 30 seconds
        private void SpawnLootDrop()
        {
            var lootCrate = Instantiate(lootCratePf, transform.position + new Vector3(0f, dropHeight, 0f), Quaternion.identity);
            var rb = lootCrate.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            FunctionTimer.Create(() => { rb.isKinematic = true; },2f);
        }

        private void Update()
        {
            timeToNextDrop -= Time.deltaTime;
            if (timeToNextDrop <= 0f)
            {
                timeToNextDrop = dropCooldown;
                SpawnLootDrop();
            }
        }
    }
}