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
        private float cooldown;
        public string[] enemyTags;

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
        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (HitValidTarget(collision.gameObject) &&
                collision.collider.TryGetComponent(out StatsManager statsManager))
            {
                print($"Melee damaging {collision.collider.name}");
                statsManager.TakeDamage(GunData.damage * 1.5f);
            }
        }
        private bool HitValidTarget(GameObject target)
        {
            foreach (string validTag in enemyTags)
            {
                if (target.CompareTag(validTag)) return true;
            }

            return false;
        }
    }
}