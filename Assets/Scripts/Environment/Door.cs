using UnityEngine;

namespace Environment
{
    public class Door : MonoBehaviour,IDestructible
    {
        private bool isOpen = false;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Player") && !isOpen)
            {
                if (TryGetComponent(out Rigidbody rb))
                {
                    rb.isKinematic = false;
                    rb.AddExplosionForce(100f,transform.forward,1f);
                    isOpen = true;
                  
                }
              
            }
        }
        public void OnDestroy()
        {
            Destroy(this.gameObject);
        }
    }
}