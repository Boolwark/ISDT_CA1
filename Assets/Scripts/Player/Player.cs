using UnityEngine;
using Util;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public void OnDeath()
        {
            LevelManager.Instance.ChangeSceneDirect("LoseScene");
        }
        public void OnVictory()
        {
            LevelManager.Instance.ChangeSceneDirect("WinScene");
        }
    }
}