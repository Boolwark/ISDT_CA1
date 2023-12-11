using CodeMonkey.Utils;
using UnityEngine;

namespace Environment.Battery
{
    public class Battery : MonoBehaviour
    {
        private bool activated = false;
        public float dropHeight = 5f;
        public float effectDuration;
        public GameObject lootCratePf;
        public GameObject activateEffect;
        public void OnSelect()
        {
            if (!activated)
            {
                BatteryManager.Instance.OnBatteryActivated();
                //Spawn Loot drop
                SpawnLootDrop();
                PlayActivateEffect();
            }
            activated = true;
        }

        private void SpawnLootDrop()
        {
            var lootCrate = Instantiate(lootCratePf, transform.position + new Vector3(0f, dropHeight, 0f), Quaternion.identity);
            var rb = lootCrate.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            FunctionTimer.Create(() => { rb.isKinematic = true; },2f);
        }

        private void PlayActivateEffect()
        {
            activateEffect.SetActive(true);
            FunctionTimer.Create(() =>
            { activateEffect.SetActive(false);
            },effectDuration);
        }
    }
}