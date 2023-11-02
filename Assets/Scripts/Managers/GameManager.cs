using System;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public class GameManager : Singleton<GameManager>
    {
        //Vector3(21.0999641,-3.67000008,2.07999992)
        public GameObject player;
        public List<Vector3> sceneSpawnPoints = new List<Vector3>();
        public enum Difficulty
        {
            EASY,
            NORMAL,
            NIGHTMARE
        }
        public Difficulty chosenDifficulty = Difficulty.NORMAL;

        private void Start()
        {
            DontDestroyOnLoad(transform.parent);
        }

        public void OnSceneChanged()
        {
            print($"Current scene index is {LevelManager.Instance.currentSceneIndex}");
            Vector3 spawnPoint = sceneSpawnPoints[LevelManager.Instance.currentSceneIndex];
            player.transform.position = spawnPoint;
        }
    }
}