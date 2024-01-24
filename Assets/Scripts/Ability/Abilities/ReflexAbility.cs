using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ability.Abilities
{ [CreateAssetMenu]
    public class ReflexAbility : global::Ability.Ability
    {
        public GameObject grayScreenPrefab;
        private GameObject currentGrayScreen;
        public float slowDownFactor;
        
        public override void Activate(GameObject parent)
        {
    TimeStopEffect();
            

        }

        public override void BeginCooldown(GameObject parent)
        {
            Time.timeScale = 1f;
            Destroy(currentGrayScreen);
        }
        public void TimeStopEffect()
        {
            currentGrayScreen = Instantiate(grayScreenPrefab);
            Vector3 grayScreenSpawnPoint = Camera.main.transform.position + Camera.main.transform.forward;
            currentGrayScreen.transform.parent = Camera.main.transform;
            currentGrayScreen.transform.position = grayScreenSpawnPoint;
            currentGrayScreen.transform.forward = Camera.main.transform.forward;
            currentGrayScreen.gameObject.SetActive(true);
            Time.timeScale = slowDownFactor;
            
        

        }
    }
}