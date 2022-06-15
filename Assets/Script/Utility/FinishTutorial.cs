using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class FinishTutorial : MonoBehaviour
{
    public bool level1;
    public bool level10;
    public bool level20;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (level1)
            {
                Unlock("CgkIwt3h49cJEAIQAQ");
            }
            else if (level10)
            {
                Unlock("CgkIwt3h49cJEAIQAg");
            }
            else if (level20)
            {
                Unlock("CgkIwt3h49cJEAIQAw");
            }
        }
    }

    private void Unlock(string achievement)
    {
        Social.ReportProgress(achievement, 100.0f, success => {
            // handle success or failure
        });
    }
}
