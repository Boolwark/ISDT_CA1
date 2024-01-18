using Environment;
using UnityEngine;

namespace Util
{
    public class SnapToCustomInteractor : MonoBehaviour
    {
        public float snapRadius=3f;
        public void OnSelectExit()
        {
            foreach (Collider collider in Physics.OverlapSphere(transform.position,snapRadius))
            {
                if (collider.TryGetComponent(out CustomXRSocketInteractor interactor))
                {
                    interactor.AttachToTransform(transform);
                    Debug.Log("Snapping");
                   
                    if (TryGetComponent(out Rigidbody rb))
                    {
                        rb.isKinematic = true;
                    }
                }
            }
        }
    }
}