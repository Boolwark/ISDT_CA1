using System.Collections;
using Effects;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

namespace Weapons.Misc
{
    public class AirstrikePhone : MonoBehaviour
    {
     // Prefab for the airstrike visual effect
        public TextMeshProUGUI airstrikeCountText; // UI Text to show remaining airstrikes
        public float cooldown = 5f; // Cooldown duration in seconds
        public VisualEffect airstrikeVFX;
        private float cooldownTimer = 0f;
        public float duration;
        private int airstrikeCount = 3; // Example starting count

        void Update()
        {
            if (cooldownTimer > 0)
            {
                cooldownTimer -= Time.deltaTime;
            }
        }

        public void OnActivate()
        {
            if (cooldownTimer <= 0 && airstrikeCount > 0)
            {
                ActivateAirstrike();
                cooldownTimer = cooldown;
                airstrikeCount--;
                UpdateAirstrikeCountUI();
            }
        }

        private void ActivateAirstrike()
        {
            airstrikeVFX.enabled = true;

            // Eliminate all enemies in the scene
            foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy")) 
            {
                ExplosionManager.Instance.SpawnExplosion(enemy.transform.position,quaternion.identity);
                Destroy(enemy.gameObject);
            }

            StartCoroutine(DeactiveAirstrike());
        }

        private IEnumerator DeactiveAirstrike()
        {
            yield return new WaitForSeconds(duration);
            airstrikeVFX.enabled = false;
        }

        private void UpdateAirstrikeCountUI()
        {
            if (airstrikeCountText != null)
            {
                airstrikeCountText.text = "Airstrikes Left: " + airstrikeCount;
            }
        }
    }
}
