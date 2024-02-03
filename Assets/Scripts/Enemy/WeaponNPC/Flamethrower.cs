using System;
using System.Collections;
using Enemy;
using UnityEngine;

namespace Effects
{
    public class Flamethrower : NPCWeapon
    {
        public  Fire fire;
        public float activeTime = 5f;
        public float cooldown = 3f;
        private float timeToNextActivation;
        private void Start()
        {
            fire.gameObject.SetActive(false);
        }

        private void Update()
        {
            timeToNextActivation -= Time.deltaTime;
        }

        public override void Activate()
        {
            if (timeToNextActivation > 0f) return;
            Debug.Log("Firing "+ transform.name);
            timeToNextActivation = cooldown;
            StartCoroutine(FlameEffect());
        }

        public override bool IsReady()
        {
            return timeToNextActivation <= 0f;
        }

        public override void SetTarget(Transform newTarget)
        {
            
        }

        private IEnumerator FlameEffect()
        {
            fire.gameObject.SetActive(true);
            yield return new WaitForSeconds(activeTime);
            fire.gameObject.SetActive(false);
        }

    }
}