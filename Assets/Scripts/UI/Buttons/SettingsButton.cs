using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class SettingsButton : GameButton
    {
        public Canvas settingsPanel;
   
        private bool isVisible;
        private void Start()
        {
            initScale = transform.localScale;
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