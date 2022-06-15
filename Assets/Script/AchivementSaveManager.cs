using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class AchivementSaveManager : MonoBehaviour
{
    public static AchivementSaveManager Instance;

    private int nbPPLHealSave;
    private int nbGlissadeSave;
    private int lesActionSave;
    private int nbBombSave;
    private int nbDieEnemyRange;
    private int nbDieEnemyCac;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        nbPPLHealSave = PlayerPrefs.GetInt("NbPPLHealSave");
        nbGlissadeSave = PlayerPrefs.GetInt("NbGlissadeSave");
        lesActionSave = PlayerPrefs.GetInt("LesActionSave");
        nbBombSave = PlayerPrefs.GetInt("NbBombSave");
        nbDieEnemyRange = PlayerPrefs.GetInt("NbDieEnemyRange");
        nbDieEnemyCac = PlayerPrefs.GetInt("NbDieEnemyCac");
    }

    public void NbPPLHealSave()
    {
        nbPPLHealSave++;
        PlayerPrefs.SetInt("NbPPLHealSave", nbPPLHealSave);
        if (nbPPLHealSave > 5)
        {
            Unlock("CgkIwt3h49cJEAIQBA");
        }
        if (nbPPLHealSave > 10)
        {
            Unlock("CgkIwt3h49cJEAIQBQ");
        }
        if (nbPPLHealSave > 15)
        {
            Unlock("CgkIwt3h49cJEAIQBg");
        }
    }

    public void NbGlissadeSave()
    {
        nbGlissadeSave++;
        PlayerPrefs.SetInt("NbGlissadeSave", nbGlissadeSave);
        if (nbGlissadeSave > 10)
        {
            Unlock("CgkIwt3h49cJEAIQBw");
        }
        if (nbGlissadeSave > 25)
        {
            Unlock("CgkIwt3h49cJEAIQCA");
        }
        if (nbGlissadeSave > 50)
        {
            Unlock("CgkIwt3h49cJEAIQCQ");
        }
    }

    public void LesActionSave()
    {
        lesActionSave++;
        PlayerPrefs.SetInt("LesActionSave", lesActionSave);
        if (lesActionSave > 100)
        {
            Unlock("CgkIwt3h49cJEAIQCg");
        }
        if (lesActionSave > 300)
        {
            Unlock("CgkIwt3h49cJEAIQCw");
        }
        if (lesActionSave > 500)
        {
            Unlock("CgkIwt3h49cJEAIQDA");
        }
    }
    public void NbBombSave()
    {
        nbBombSave++;
        PlayerPrefs.SetInt("NbBombSave", nbBombSave);
        if (nbBombSave > 1)
        {
            Unlock("CgkIwt3h49cJEAIQDQ");
        }
        if (nbBombSave > 3)
        {
            Unlock("CgkIwt3h49cJEAIQDg");
        }
        if (nbBombSave > 10)
        {
            Unlock("CgkIwt3h49cJEAIQDw");
        }
    }
    public void NbDieEnemyRange()
    {
        nbDieEnemyRange++;
        PlayerPrefs.SetInt("NbDieEnemyRange", nbDieEnemyRange);
        if (nbDieEnemyRange > 5)
        {
            Unlock("CgkIwt3h49cJEAIQEA");
        }
        if (nbDieEnemyRange > 15)
        {
            Unlock("CgkIwt3h49cJEAIQEQ");
        }
        if (nbDieEnemyRange > 25)
        {
            Unlock("CgkIwt3h49cJEAIQEg");
        }
    }
    public void NbDieEnemyCac()
    {
        nbDieEnemyCac++;
        PlayerPrefs.SetInt("NbDieEnemyCac", nbDieEnemyCac);
        if (nbDieEnemyCac > 5)
        {
            Unlock("CgkIwt3h49cJEAIQEw");
        }
        if (nbDieEnemyCac > 15)
        {
            Unlock("CgkIwt3h49cJEAIQFA");
        }
        if (nbDieEnemyCac > 25)
        {
            Unlock("CgkIwt3h49cJEAIQFQ");
        }
    }

    private void Unlock(string achievement)
    {
        Social.ReportProgress(achievement, 100.0f, success => {
            // handle success or failure
        });
    }
}
