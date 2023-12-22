using System;
using Environment;
using UnityEngine;

namespace Weapons.Misc
{
    public class Flashlight : MonoBehaviour
    {
        public Light light;
        private bool isActive = false;
        private float intensity = 0f;
        private Rigidbody rb;
        private void Start()
        {
            intensity = light.intensity;
            rb = GetComponent<Rigidbody>();
        }

        public void ToggleLight()
        {
            light.intensity = isActive? intensity:0f;
            isActive = !isActive;
        }

        public void OnSelectEnter()
        {
            
                rb.isKinematic = true;
            
        }
        

        public void OnSelectExit()
        {
            foreach (Collider collider in Physics.OverlapSphere(transform.position,2f))
            {
                if (collider.TryGetComponent(out CustomXRSocketInteractor interactor))
                {
                    transform.SetParent(interactor.transform);
                    Debug.Log("Snapping");
                    transform.localPosition = Vector3.zero;
                    transform.localRotation = Quaternion.identity;
                  
                        rb.isKinematic = true;
                    
                }
            }
        }
    }
}