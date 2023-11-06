using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Weapons
{
    public class MachineGun : Gun
    {
        public int magazineSize = 30;
        public float fireRate = 0.1f;
        public float reloadDuration = 2f;
        public float shotsPerFire = 5;

        private int currentAmmo;
        private int totalAmmo;
        private bool isReloading = false;
        private bool triggerHeld = false;
        private Coroutine autoFireCoroutine;

        protected override void Start()
        {
            base.Start();
            currentAmmo = magazineSize;
            totalAmmo = magazineSize * 3; // Example total ammo.
            UpdateAmmoDisplay();
        }

        public override void FireBullet(ActivateEventArgs args)
        {
            if (isReloading || currentAmmo <= 0)
                return;

            triggerHeld = true;
            if (autoFireCoroutine == null) // Ensure we only have one coroutine running.
            {
                autoFireCoroutine = StartCoroutine(AutoFire());
            }
        }


        private IEnumerator AutoFire()
        {
            int shotsFired = 0;
            while (triggerHeld && currentAmmo > 0 && !isReloading && shotsFired < shotsPerFire)
            {
                shotsFired++;
                base.FireBullet(new ActivateEventArgs());
                currentAmmo--;
                muzzleEffect.Play();
                UpdateAmmoDisplay();
                if (currentAmmo <= 0)
                {
                    StartCoroutine(Reload());
                }
                yield return new WaitForSeconds(fireRate);
            }
            autoFireCoroutine = null;
        }

        private IEnumerator Reload()
        {
            if (isReloading)
                yield break;

            isReloading = true;
            // Add reload animation/sound here.
            yield return new WaitForSeconds(reloadDuration);
            currentAmmo = Mathf.Min(totalAmmo, magazineSize);
            totalAmmo -= currentAmmo;
            isReloading = false;
            UpdateAmmoDisplay();
        }

        protected override void UpdateAmmoDisplay()
        {
            ammoDisplayText.text = $"{currentAmmo} / {totalAmmo}";
        }

        private void Update()
        {
            if (grabbable.isSelected && !triggerHeld)
            {
                if (autoFireCoroutine != null)
                {
                    StopCoroutine(autoFireCoroutine); // Stop only the AutoFire coroutine
                    autoFireCoroutine = null;
                }
            }
        }
    }

}