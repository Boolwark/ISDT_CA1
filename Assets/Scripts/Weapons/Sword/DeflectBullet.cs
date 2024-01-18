using UnityEngine;

namespace Weapons.Sword
{
    public class DeflectBullet : MonoBehaviour
    {
        public Material deflectedMaterial;
        public float deflectionForce;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out Bullet bullet))
            {
                Debug.Log("Deflecting");
                bullet.enemyTag = "Enemy";
                bullet.GetComponent<TrailRenderer>().material = deflectedMaterial;
                //apply deflection physics to bullet here
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                if (bulletRb != null)
                {
                    // Calculate deflection direction (in this example, we reflect the bullet opposite to the collision normal)
                    Vector3 deflectionDirection = -collision.contacts[0].normal;
                    bulletRb.isKinematic = false;
                    // Apply force to the bullet to deflect it
                    bulletRb.velocity = deflectionDirection * deflectionForce;
                }
            }            
        }
    }
}