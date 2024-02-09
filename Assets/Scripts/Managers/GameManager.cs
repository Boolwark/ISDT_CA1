using System;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public class GameManager : Singleton<GameManager>
    {
        public bool dontDestroyPlayer = true;
        public GameObject player;
        public Dictionary<string,Vector3> sceneNamesToSpawnPoints = new Dictionary<string,Vector3>(){
            { "MenuScene", new Vector3(12.5699997f,2.52999997f,7.23000002f) },
            { "LoseScene",new Vector3(2.1099999f,1.98800004f,-3.52018523f) },
            { "WinScene",new Vector3(2.1099999f,1.98800004f,-3.52018523f) },
            { "UnderwaterScene", new Vector3(7.31599998f,1.02999997f,-11.2950001f)}
        };
        public enum Difficulty
        {
            EASY,
            NORMAL,
            NIGHTMARE
        }
        public Difficulty chosenDifficulty = Difficulty.NORMAL;

        private void Start()
        {
            if (dontDestroyPlayer)
            {
                DontDestroyOnLoad(transform.parent);
            }
            else
            {
                player.transform.parent = null;
            }
        
        }

        public void OnSceneChanged()
        {
            print($"Current scene index is {LevelManager.Instance.currentScene}");
            string currentScene = LevelManager.Instance.currentScene;
            Vector3 spawnPoint =  sceneNamesToSpawnPoints[currentScene];
            player.transform.position = spawnPoint;
        }
    }
}