using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Weapons.Magazines;
using Weapons.Magazines.Weapons.Magazines;

namespace Weapons
{
    public class Gun : GunBase
    {
       
           
        private int currentAmmo;
        private int totalAmmo;
        private bool isReloading = false;
        private bool triggerHeld = false;
        private Coroutine autoFireCoroutine;
        public Transform magazineHoldPos;
        private MagazineHolder currentMagazineHolder;

        protected override void Start()
        {
            base.Start();
            currentAmmo = gunData.magazineSize;
            totalAmmo = gunData.magazineSize * gunData.nClips; // Example total ammo.
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
            while (triggerHeld && currentAmmo > 0 && !isReloading && shotsFired < gunData.shotsPerFire)
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
                yield return new WaitForSeconds(gunData.fireRate);
            }
            autoFireCoroutine = null;
        }

        private IEnumerator Reload()
        {
            if (isReloading)
                yield break;

            isReloading = true;
            // Add reload animation/sound here.
            yield return new WaitForSeconds(gunData.reloadDuration);
            currentAmmo = Mathf.Min(totalAmmo, gunData.magazineSize);
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

        public void InsertMagazine(MagazineHolder magazineHolder)
        {
            if (currentMagazineHolder != null)
            {
                currentMagazineHolder.transform.SetParent(null);
            }

            if (magazineHolder.isUsed) return;
            magazineHolder.isUsed = true;
            currentMagazineHolder = magazineHolder;
            base.SetBulletPf(magazineHolder.Magazine.bulletPrefab);
            totalAmmo += magazineHolder.Magazine.ammoCount;
            magazineHolder.transform.SetParent(magazineHoldPos);
            magazineHolder.transform.localPosition = Vector3.zero;
            magazineHolder.transform.localRotation = Quaternion.identity;
            magazineHolder.OnAttached();
        }
    }

}