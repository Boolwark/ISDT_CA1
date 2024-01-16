using System;
using Effects;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Weapons
{
    public class Anchor : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Enemy"))
            { Debug.Log("Anchor hit enemy:"+collision.collider.name);
                if (collision.collider.TryGetComponent(out MeshDestroy meshDestroy))
                {
                     Debug.Log("Anchor hit mesh destroytu");
                    meshDestroy.DestroyMesh();
                }
            }
        }
    }
}