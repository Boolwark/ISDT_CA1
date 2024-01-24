using DefaultNamespace.ObjectPooling;
using UnityEngine;

namespace Environment.OxygenTank
{
    public class OxygenTank : MonoBehaviour
    {
        public GameObject firePf;
        public float throwForce = 200f;
        public void Burn()
        {
            ObjectPoolManager.SpawnObject(firePf, transform.position, Quaternion.identity);
        }

        public void Throw()
        {
            var rb = GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddForce(transform.forward * throwForce,ForceMode.Force);
        }
    }
}