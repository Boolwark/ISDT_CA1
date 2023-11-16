using System;
using TMPro;
using UnityEngine;

namespace Util
{
    public class LeaderboardText : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;
        private const int MaxScores = 5; // Max number of scores to display

        private void Start()
        {
            DisplayTopScores();
        }

        private void DisplayTopScores()
        {
            string scoreDisplay = "Top Scores:\n";
            for (int i = 0; i < MaxScores; i++)
            {
                float score = PlayerPrefs.GetFloat("Score_" + i, 0);
                scoreDisplay += (i + 1).ToString() + ". " + score.ToString("F2") + "\n"; // "F2" for two decimal places
            }

            scoreText.text = scoreDisplay;
        }
    }
}