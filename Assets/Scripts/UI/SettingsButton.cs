using System;
using UnityEngine;

namespace UI
{
    public class SettingsButton : MonoBehaviour
    {
        public GameObject settingsPanel;

        private void Start()
        {
            settingsPanel.SetActive(false);
        }

        public void OnClick()
        {
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
    }
}