using DefaultNamespace.ObjectPooling;
using UnityEngine;

namespace Environment.OxygenTank
{
    public class OxygenTank : MonoBehaviour
    {
        public GameObject firePf;
        public void Burn()
        {
            ObjectPoolManager.SpawnObject(firePf, transform.position, Quaternion.identity);
        }
    }
}