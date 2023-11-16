using System;
using UnityEngine;

namespace Testing
{
    public class TestLeaderboard :  MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Test killing 5 enemies");
                LeaderboardManager.Instance.IncrementScore(5f);
            }
        }
    }
}