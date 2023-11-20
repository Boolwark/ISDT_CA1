using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ability.Abilities
{ [CreateAssetMenu]
    public class ReflexAbility : Ability
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
            currentGrayScreen.gameObject.SetActive(true);
            Time.timeScale = slowDownFactor;
            
        

        }
    }
}