using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Effects.Elements;
using Unity.VisualScripting;
using UnityEngine;

namespace Weapons.Projectiles
{
    public class BulletExplodeEffect : MonoBehaviour
    {
        public GameObject effectPf;
        public float radius;
        public LayerMask whatIsEnemy;
        public GameObject effect;
        public void OnDestroyed()
        {
            ActivateEffect();
            foreach (var enemy in            Physics.OverlapSphere(transform.position, radius, whatIsEnemy))
            {
                ApplyEffect(enemy.transform);
            }
        }
        private void ApplyEffect(Transform enemy)
        {
            Instantiate(effect, enemy);
        }

        
        private void ActivateEffect()
        {
            StartCoroutine(_ActivateShield());
        }

        private IEnumerator _ActivateShield()
        {
            effectPf.SetActive(true);
            effectPf.transform.DOScale
                (effectPf.transform.localScale * 1.5f, 1f);
            yield return new WaitForSeconds(1F);
            effectPf.transform.DOScale(effectPf.transform.localPosition / 1.5f, 1f);
            effectPf.SetActive(false);

        }
    }
    
}