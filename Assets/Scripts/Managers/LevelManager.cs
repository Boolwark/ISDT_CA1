using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Util
{
    public class LevelManager  : Singleton<LevelManager>
    {
        public UnityEvent onSceneChanged;
        public int currentSceneIndex = -1; // Menu scene starts first, is scene index -1.
        public void ChangeScene(string sceneName)
        {
            currentSceneIndex++;
            SceneManager.LoadScene(sceneName);
            onSceneChanged?.Invoke();
        }

     

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
        }
    }
}