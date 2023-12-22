using System;
using Stats;
using UnityEngine;
using Weapons;

namespace Player.XR
{
    public class BreakLootCrate : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out LootBox lootBox))
            {
                Debug.Log("Breaking open lootbox");
                if (other.gameObject.TryGetComponent(out StatsManager sm))
                {
                    sm.OnKilled?.Invoke();
                }
            }
        }
    }
}