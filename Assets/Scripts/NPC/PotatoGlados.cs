using System;
using System.Collections;
using Environment;
using UnityEngine;

namespace Assets.Scripts.NPC
{
    /// <summary>
    /// All for the shit and giggles
    /// </summary>
    public class PotatoGlados : MonoBehaviour
    {
        private bool spoken = false;
        public Transform bowlTransform;
        public bool inserted = false;
        public AirfryerTray airfryerTray;
        public float offset = 5f;

        private void Update()
        {
            if (!spoken && Vector3.Distance(Camera.main.transform.position, transform.position) <= offset)
            {
                StartCoroutine(PlayDialog());
            }
        }

        private IEnumerator PlayDialog()
        {
            spoken = true;
            AudioManager.Instance.PlaySFX("GladosSpeech1");
            yield return new WaitForSeconds(1f);
            AudioManager.Instance.PlaySFX("GladosSpeech2");
        }
        public void OnSelectExit()
        {
       
            if (Vector3.Distance(transform.position, airfryerTray.transform.position) <= offset)
            {

                inserted = true; 
                transform.SetParent(bowlTransform);
                transform.localPosition = Vector3.zero;
                Debug.Log("Air frying glados");
            }
         
        }
    }
}