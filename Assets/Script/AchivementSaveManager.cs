using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivementSaveManager : MonoBehaviour
{
    public static AchivementSaveManager Instance;

    public int NbPPLHealSave
    {
        get{return PlayerPrefs.GetInt("NbPPLHealSave"); }
        set{PlayerPrefs.SetInt("NbPPLHealSave", value);}
    }

    public int NbGlissadeSave
    {
        get { return PlayerPrefs.GetInt("NbGlissadeSave"); }
        set { PlayerPrefs.SetInt("NbGlissadeSave", value); }
    }

    public int LesActionSave
    {
        get { return PlayerPrefs.GetInt("LesActionSave"); }
        set { PlayerPrefs.SetInt("LesActionSave", value); }
    }
    public int NbBombSave
    {
        get { return PlayerPrefs.GetInt("NbBombSave"); }
        set { PlayerPrefs.SetInt("NbBombSave", value); }
    }
    public int NbDieEnemyRange
    {
        get { return PlayerPrefs.GetInt("NbDieEnemyRange"); }
        set { PlayerPrefs.SetInt("NbDieEnemyRange", value); }
    }
    public int NbDieEnemyCac
    {
        get { return PlayerPrefs.GetInt("NbDieEnemyCac"); }
        set { PlayerPrefs.SetInt("NbDieEnemyCac", value); }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
