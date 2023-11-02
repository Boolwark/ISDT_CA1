using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;
using Util;

namespace UI
{
    public class SceneButton : MonoBehaviour
    {
        [SerializeField] private string sceneName;
        public Button button;
        public void OnClick()
        {
            print($"Navigating to scene {sceneName}");
            LevelManager.Instance.ChangeScene(sceneName);
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