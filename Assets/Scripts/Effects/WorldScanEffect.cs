using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Util;

namespace Effects
{
    public class WorldScanEffect : MonoBehaviour
    {
        public float duration = 1 * 60;
        public GameObject scanObj;
        public float effectDuration = 5f;
        public GameObject beaconObj;
        public TextMeshProUGUI countdownTxt;
        public UnityEvent OnCountdownFinished;

        private void Start()
        {
            StartCoroutine(Countdown());
        }

        public void Activate()
        {
            beaconObj.SetActive(true);
            scanObj.transform.DOScale(scanObj.transform.localScale * 100f, effectDuration).OnComplete(() =>
            {
                Destroy(scanObj);
                KillAllEnemies();
            });
        }
        

        public IEnumerator Countdown()
        {
            while (duration >= 0)
            {
                duration -= Time.deltaTime;
                TimeSpan t = TimeSpan.FromSeconds( duration );

                string timeStamp = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms", 
                    t.Hours, 
                    t.Minutes, 
                    t.Seconds, 
                    t.Milliseconds);
                countdownTxt.text = timeStamp;
                yield return null;
            }

            Activate();
            FunctionTimer.Create(() => {       OnCountdownFinished?.Invoke(); }, 3f);
      
            
        }

        private void KillAllEnemies()
        {
            foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                DissolveManager.Instance.DissolveObject(enemy,1f);
            }

            
        }
    }
}