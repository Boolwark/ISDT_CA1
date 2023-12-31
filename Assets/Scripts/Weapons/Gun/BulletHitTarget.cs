using UnityEngine;

namespace Weapons
{
    public class BulletHitTarget : MonoBehaviour
    {
        public GameObject[] bulletHoles;
        public string soundEffect;

        void OnCollisionEnter(Collision collision)
        {
            if (!collision.collider.CompareTag("Enemy") &&  !collision.collider.CompareTag("Wall")) return;
            Debug.Log("hit target");

            int bulletHolesIndex = Random.Range(0, bulletHoles.Length-1);
		
            foreach (ContactPoint contact in collision.contacts)
            {
                var bulletHole = Instantiate(bulletHoles[bulletHolesIndex], contact.point, Quaternion.identity * Quaternion.Euler(0,0,bulletHolesIndex * 45f));
                Destroy(bulletHole,3f);
            }
            AudioManager.Instance.PlaySFX(soundEffect);
        }
    }
}