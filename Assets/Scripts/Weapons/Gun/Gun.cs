using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
namespace Weapons
{
    public class Gun : MonoBehaviour
    {
        public GameObject bullet;
        public Transform spawnPoint;
        public float fireSpeed = 20f;

        void Start()
        {
            XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
            grabbable.activated.AddListener(FireBullet);
        }

        public void FireBullet(ActivateEventArgs args)
        {
            GameObject spawnedBullet = Instantiate(bullet);
            spawnedBullet.transform.position = spawnPoint.position;
            spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
            Destroy(spawnedBullet,5);
        }
    }
}