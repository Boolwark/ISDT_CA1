using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;
using Util;

namespace UI
{
    public class SceneButton : GameButton
    {
        [SerializeField] private string sceneName;
        public Button button;
   

      

        public void OnClick()
        {
            print($"Navigating to scene {sceneName}");
            LevelManager.Instance.ChangeSceneDirect(sceneName);
            Destroy(this);
            button.interactable = false;
            StartCoroutine(EnableButtonAfterDelay(button, 1f));
        }

      

    
        IEnumerator EnableButtonAfterDelay(Button button, float seconds) {
            yield return new WaitForSeconds(seconds);
            button.interactable = true;
        }
    }
}