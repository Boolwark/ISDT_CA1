using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : Util.Singleton<LeaderboardManager>
{
    private const int MaxScores = 5; // Max number of scores to store
    private float currentScore = 0f;
    public void TryAddScore(float newScore)
    {
        List<float> scores = LoadScores();
        scores.Add(newScore);
        scores.Sort((a, b) => b.CompareTo(a)); // Sort scores in descending order
        if (scores.Count > MaxScores)
        {
            scores.RemoveAt(scores.Count - 1); // Remove the lowest score if we exceed the max count
        }
        SaveScores(scores);
    }

    public void IncrementScore(float amount)
    {
        currentScore += amount;
    }

    private List<float> LoadScores()
    {
        List<float> scores = new List<float>();
        for (int i = 0; i < MaxScores; i++)
        {
            scores.Add(PlayerPrefs.GetFloat("Score_" + i, 0));
        }
        return scores;
    }

    private void SaveScores(List<float> scores)
    {
        for (int i = 0; i < scores.Count; i++)
        {
            PlayerPrefs.SetFloat("Score_" + i, scores[i]);
        }
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void OnDestroy()
    {
        TryAddScore(currentScore);
    }
}
