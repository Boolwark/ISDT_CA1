using System;
using CodeMonkey.Utils;
using UI;
using UnityEngine;

namespace Environment.Battery
{
    public class Battery : MonoBehaviour
    {
        public ButtonFollowVisual buttonFollowVisual;
        private bool activated = false;
        public float effectDuration;
        public GameObject activateEffect;
        public void Activate()
        {
            if (!activated)
            {
                BatteryManager.Instance.OnBatteryActivated();
                PlayActivateEffect();
            }
            activated = true;
        }

        private void Start()
        {
            buttonFollowVisual.OnButtonPressed.AddListener(Activate);
        }


        private void PlayActivateEffect()
        {
            activateEffect.SetActive(true);
            FunctionTimer.Create(() =>
            { activateEffect.SetActive(false);
            },effectDuration);
        }
    }
}