using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Util;

namespace Environment.Battery
{
    public class BatteryManager : Singleton<BatteryManager>
    {
        
        private int nBatteriesRemaining;

        private void Start()
        {
            nBatteriesRemaining = GameObject.FindGameObjectsWithTag("Battery").Length;
        }
        public void OnBatteryActivated()
        {
            nBatteriesRemaining--;
            print($"Number of remaining batteries:{nBatteriesRemaining}");
            if (nBatteriesRemaining == 0)
            {
              // Move onto next scene
             LevelManager.Instance.ChangeSceneDirect("SecondScene");
            }
        }
    }
}