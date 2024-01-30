using System.Collections;
using UnityEngine;
using CodeMonkey.Utils;
using DefaultNamespace.ObjectPooling;
using DG.Tweening;
using Stats;
using Unity.VisualScripting;
using Weapons;

namespace NPC
{
    public class ElectroSpear : MonoBehaviour
    {
        public GunData GunData;
        public GameObject slashEffect;
        public GameObject shield;
        public Transform firePos;
        public float shieldDuration = 5f;
        private float cooldown;

        private void Update()
        {
            cooldown -= Time.deltaTime;
        }

        public void MeleeAttack()
        {
            transform.DORotate(transform.rotation.eulerAngles + new Vector3(360, 360, 360), 2f).SetLoops(5);
            slashEffect.SetActive(true);
        }

        public void RangedAttack()
        {
            if (cooldown <= 0f)
            {
                cooldown = 1 / GunData.fireRate;

                for (int i = 0; i < GunData.shotsPerFire; i++)
                {
                    var electroProjectile = ObjectPoolManager.SpawnObject(GunData.bullet, firePos.position, firePos.rotation);
                    if (electroProjectile.TryGetComponent(out Bullet bullet))
                    {
                        bullet.damage = GunData.damage;
                    }

                    if (electroProjectile.TryGetComponent(out Rigidbody rb))
                    {
                        rb.velocity = (electroProjectile.transform.forward * GunData.fireSpeed);
                    }
                  
                }
            }
        }

        public void ActivateShield()
        {
            StartCoroutine(_ActivateShield());
        }

        private IEnumerator _ActivateShield()
        {
            shield.SetActive(true);
            shield.transform.DOScale(shield.transform.localScale * 1.5f, 1f);
            yield return new WaitForSeconds(1F);
            shield.transform.DOScale(shield.transform.localPosition / 1.5f, 1f);
            shield.SetActive(false);

        }
    }
}