using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using CodeMonkey.Utils;
using Stats;
using UI.UI.DefaultNamespace.GameUI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons
{
    public class LootBox : MonoBehaviour
    {
        private bool opened = false;
        public float healAmount = 10f;
        public float lootExplosionForce;
        public GameObject lootDropEffect;
        public List<GameObject> lootList = new List<GameObject>();
        public void OnActivate()
        {
          
                Debug.Log("Healing player");
                if (!opened)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<StatsManager>().TakeHeal(healAmount);
                    LootDrop();
                }

                opened = true;
                Destroy(gameObject,1f);

        }

        public void LootDrop()
        {
            //TODO: Implement loot drop here!
            lootDropEffect.SetActive(true);
            foreach (var loot in lootList)
            {
                Instantiate(loot, transform.position, transform.rotation);
                Rigidbody lootRB = loot.GetComponentInChildren<Rigidbody>();
                lootRB.isKinematic = false;
                lootRB.GetComponentInChildren<Rigidbody>().AddExplosionForce(lootExplosionForce,transform.position,1F);
                FunctionTimer.Create(() => { lootRB.isKinematic = true; }, 1f);
            }
            
        }
    }
}