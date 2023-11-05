using System;
using UnityEngine;

namespace Weapons.Misc
{
    public class WebBullet : Bullet
    {
        [SerializeField] private GameObject spiderwebPrefab; 
        protected override void OnCollisionEnter(Collision col)
        {
            GameObject currentSpiderweb = Instantiate(spiderwebPrefab, col.transform.position, Quaternion.identity);
            Destroy(currentSpiderweb,0.5f);
            base.OnCollisionEnter(col);
        }
    }
}