using Assets.Scripts.NPC;
using UnityEngine;
using UnityEngine.Events;

namespace Environment
{
    public class AirfryerTray : MonoBehaviour
    {
        private bool isHeld = false;
        public PotatoGlados potatoGlados;
        public Vector3 insertPos = new Vector3(0.00200000009f, 0.194999993f, -0.0379999988f);
        public Quaternion insertRot = Quaternion.Euler(new Vector3(270,354.863159f,0));
        public float offset = 2f;
        public AirfryerCooker airfryerCooker;
        public UnityEvent OnAirFryerActivate;

        public void OnSelect()
        {
            isHeld = true;
            
        }
  
        public void OnSelectExit()
        {
            isHeld = false;
            if (Vector3.Distance(transform.position, airfryerCooker.transform.position) <= offset && potatoGlados.inserted)
            {
                transform.localPosition = insertPos;
                transform.localRotation = insertRot;
                Debug.Log("Air frying glados");
                AudioManager.Instance.PlaySFX("AirFry");
                OnAirFryerActivate?.Invoke();
            }
         
        }

     
    }
}