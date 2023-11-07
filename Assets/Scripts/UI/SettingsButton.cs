using System;
using UnityEngine;

namespace UI
{
    public class SettingsButton : MonoBehaviour
    {
        public Canvas settingsPanel;
        private Vector3 initScale;
        private bool isVisible;
        private void Start()
        {
            initScale = settingsPanel.transform.localScale;
            settingsPanel.transform.localScale = Vector3.zero;
        }

        public void OnClick()
        {
            Debug.Log("Clicking settings panel button");
            if (!isVisible)
            {
                settingsPanel.transform.localScale = initScale;
            }
            else
            {
                settingsPanel.transform.localScale = Vector3.zero;
            }

            isVisible = !isVisible;

        }
    }
}