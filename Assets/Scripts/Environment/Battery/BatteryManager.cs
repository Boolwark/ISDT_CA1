using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Util;

namespace Environment.Battery
{
    public class BatteryManager : Singleton<BatteryManager>
    {
        public TextMeshProUGUI batteryUI;
        private int nBatteriesActivated=0;
        private int totalBatteries;
        public UnityEvent OnAllBatteriesActivated;

        private void Start()
        {
            totalBatteries = FindObjectsOfType<Battery>().Length;
            batteryUI = FindObjectOfType<BatteryUI>().textMeshProUGUI;

        }
        public void OnBatteryActivated()
        {
            nBatteriesActivated++;
            print($"Number of activated batteries:{nBatteriesActivated}");
            batteryUI.text = $"Activated batteries:\n {nBatteriesActivated}/{totalBatteries}";
            if (nBatteriesActivated == totalBatteries)
            {
                OnAllBatteriesActivated?.Invoke();
            }
        }
    }
}