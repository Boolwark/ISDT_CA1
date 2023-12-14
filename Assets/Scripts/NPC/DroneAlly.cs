using DefaultNamespace.ObjectPooling;
using UnityEngine;

namespace Assets.Scripts.NPC
{
    public class DroneAlly : MonoBehaviour
    {
        public float maxDistance = 5.0f;
        public float attackSpeed = 1.0f;
        public GameObject bulletPf;
        public LayerMask enemyLayer;
        public float distanceOffset = 2f;

        private void Update()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, maxDistance, enemyLayer);
            if (hitColliders.Length > 0)
            {
                Transform enemyTransform = hitColliders[0].transform;
                Vector3 direction = enemyTransform.position - transform.position;
                if (Vector3.Distance(transform.position, enemyTransform.position) < distanceOffset)
                {
                    transform.Translate(direction *attackSpeed *Time.deltaTime);
                }
              
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = rotation;

                FireBullet(enemyTransform);
            }
        }

        private void FireBullet(Transform target)
        {
            var bulletGO = ObjectPoolManager.SpawnObject(bulletPf, transform.position, Quaternion.identity);
            bulletGO.GetComponent<Rigidbody>().AddForce((target.transform.position - transform.position)*1000f,ForceMode.Force);
        }
    }
}