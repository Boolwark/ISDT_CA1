using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class SettingsButton : GameButton
    {
        public RectTransform settingsPanel;
   
        private bool isVisible = false;
        [SerializeField] private Vector3 canvasInitScale = new Vector3(0.5f,0.5f,0.5f);
        private void Start()
        {
            initScale = transform.localScale;
            canvasInitScale = settingsPanel.transform.localScale;
            settingsPanel.transform.localScale = Vector3.zero;
        }

        public void OnClick()
        {
            Debug.Log("Clicking settings panel button");
         

            isVisible = !isVisible;
            if (isVisible)
            {
                settingsPanel.transform.localScale = canvasInitScale;
            }
            else
            {
                settingsPanel.transform.localScale = Vector3.zero;
            }

        }
      
    }
}