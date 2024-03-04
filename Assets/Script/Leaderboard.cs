using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using GooglePlayGames;
using UnityEngine.SceneManagement;
using System;
using GooglePlayGames.BasicApi;

public class Leaderboard : MonoBehaviour
{
    public static Leaderboard Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        Debug.Log("BEST SCORE LEADERBOARD : " + PlayerPrefs.GetInt("BestScoreLeaderboard"));
    }

    private void LogInToGooglePlay()
    {
        //PlayGamesPlatform.Instance.Authenticate(ProcessAuthentification);
    }

    private void ProcessAuthentification(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            Debug.Log("OUAIS ON EST LA");
        }
        else
        {
            Debug.Log("FUUUUUUUCK");
        }
    }

    public void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }

    public void DoLeaderboardPost(int _score)
    {
        Social.ReportScore(_score, GPGSIds.leaderboard_the_best_medic, LeaderBoardUpdate);
    }

    private void LeaderBoardUpdate(bool success)
    {
        if (success) Debug.Log("Updated LeaderBoard");
        else Debug.Log("Unable To update Leaderboard");
        
    }

    public void getLeaderboardScore()
    {
        string _sceneNumber = SceneManager.GetActiveScene().name;
        Debug.Log("SCENE NUMBER : " + _sceneNumber.Substring(_sceneNumber.Length - 2));
        int scoreString = 0;
        if(_sceneNumber.Substring(_sceneNumber.Length - 2) != "to")
        {
            scoreString = Int32.Parse(_sceneNumber.Substring(_sceneNumber.Length - 2));
        }
        else
        {
            scoreString = Int32.Parse(_sceneNumber.Substring(5,2));
        }

        Debug.Log(scoreString);

        int scoreLeaderboard = scoreString * 1000 + (1000 - GameManager.Instance.ActionPoint * 10);
        //Debug.LogError(scoreLeaderboard);

        if (GameManager.Instance.ActionPoint < 100 && scoreLeaderboard > PlayerPrefs.GetInt("BestScoreLeaderboard"))//normal HS
        {
            PlayerPrefs.SetInt("BestScoreLeaderboard", scoreLeaderboard);
            DoLeaderboardPost(scoreLeaderboard);
            Debug.Log("TROP FORT");
        }
        else//noobs
        {
            scoreLeaderboard = Int32.Parse(_sceneNumber.Substring(_sceneNumber.Length - 2)) * 1000;
            PlayerPrefs.SetInt("BestScoreLeaderboard", scoreLeaderboard);
            DoLeaderboardPost(scoreLeaderboard);
            Debug.Log("NOOOBS");
        }

    }
}
