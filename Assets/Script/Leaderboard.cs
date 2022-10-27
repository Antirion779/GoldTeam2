using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using GooglePlayGames;

public class Leaderboard : MonoBehaviour
{
    public static Leaderboard Instance;
    public int BestScoreLeaderboard;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        BestScoreLeaderboard = PlayerPrefs.GetInt("BestScoreLeaderboard");
    }
    public void Setup()
    {
        if (Instance == null)
            Instance = this;
    }

    public void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }

    public void DoLeaderboardPost(int _score)
    {
        Social.ReportScore(_score, GPGSIds.leaderboard_the_best_medic, success => {
            // handle success or failure
        });
    }
}
