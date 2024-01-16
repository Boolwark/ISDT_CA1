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
                    transform.SetParent(interactor.transform);
                    Debug.Log("Snapping");
                    transform.localPosition = Vector3.zero;
                    transform.localRotation = Quaternion.identity;
                    if (TryGetComponent(out Rigidbody rb))
                    {
                        rb.isKinematic = true;
                    }
             
                    
                }
            }
        }
    }
}