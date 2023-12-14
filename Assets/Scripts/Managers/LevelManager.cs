using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Util
{
    public class LevelManager  : Singleton<LevelManager>
    {
        public GameObject loadingScreen;
        public Slider slider;
        public TextMeshProUGUI progressText;
        public UnityEvent onSceneChanged;
        public int currentSceneIndex = -1; // Menu scene starts first, is scene index -1.
        public void ChangeScene(string sceneName)
        {
            currentSceneIndex++;
            StartCoroutine(LoadLevelAsync(sceneName));
            onSceneChanged?.Invoke();
        }
        public void ChangeSceneDirect(string sceneName)
        {
            currentSceneIndex++;
            onSceneChanged?.Invoke();
            SceneManager.LoadScene(sceneName);
            AudioManager.Instance.PlayMusic(sceneName);
        }

        IEnumerator LoadLevelAsync(string sceneName)
        {
            AudioManager.Instance.PlayMusic(sceneName);
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            loadingScreen.SetActive(true);
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                Debug.Log(progress);
                slider.value = progress;
                progressText.text = progress * 100f + "%";
                yield return null;
            }
        }

     

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
        }
    }
}