using DefaultNamespace.ObjectPooling;

namespace Enemy.Boss
{
    using UnityEngine;

    public class HomingRocket : MonoBehaviour
    {
        private Transform player;
        public float speed = 10f;
        public float countdown = 5f;
        public GameObject explosionVFX;

        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            player = GameObject.FindWithTag("Player").transform;
        }

        private void FixedUpdate()
        {
            if (player != null)
            {
                Vector3 direction = (player.transform.position - transform.position).normalized;
                rb.velocity = direction * speed;
                transform.LookAt(player.transform);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player") || countdown <= 0f)
            {
                Explode();
            }
        }

        private void Update()
        {
            countdown -= Time.deltaTime;
        }

        private void Explode()
        {
            Instantiate(explosionVFX, transform.position, Quaternion.identity);
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
}