
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [System.Serializable]
    public struct LevelInfo
    {
        public string _label;
        public Text _txtScore;
        public Button _lvlButton;
        public Image[] imgEtoile;
        
        public int etoile1, etoile2, etoile3;
    }
    public string sceneName;
    public int allAction = 0;
  
 
    public Sprite spriteEtoile;
    public Button[] levelButton;

    public List<LevelInfo> _levelInfo = new List<LevelInfo>();

    [SerializeField] private Text actionText;
    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        //on desactive tous les levels du jeu sauf le 1(dans le menu selector)

        for (int i = 0; i < levelButton.Length; i++)
        {
            if( i + 1 > levelReached)
            {
                levelButton[i].interactable = false;
            }
            
        }



        #region saveLevel

        CheckStars("allAction_2", _levelInfo[0]);
        CheckStars("allAction_3", _levelInfo[1]);
        CheckStars("allAction_4", _levelInfo[2]);
        CheckStars("allAction_5", _levelInfo[3]);
        CheckStars("allAction_6", _levelInfo[4]);
        CheckStars("allAction_7", _levelInfo[5]);
        CheckStars("allAction_8", _levelInfo[6]);
        CheckStars("allAction_9", _levelInfo[7]);
        CheckStars("allAction_10", _levelInfo[8]);
        CheckStars("allAction_11", _levelInfo[9]);
        CheckStars("allAction_12", _levelInfo[10]);
        CheckStars("allAction_13", _levelInfo[11]);
        CheckStars("allAction_14", _levelInfo[12]);
        CheckStars("allAction_15", _levelInfo[13]);
        CheckStars("allAction_16", _levelInfo[14]);
        CheckStars("allAction_17", _levelInfo[15]);
        CheckStars("allAction_18", _levelInfo[16]);
        CheckStars("allAction_19", _levelInfo[17]);
        CheckStars("allAction_20", _levelInfo[18]);
        CheckStars("allAction_21", _levelInfo[19]);
       

        _levelInfo[0]._txtScore.text = PlayerPrefs.GetInt("allAction_" + 2).ToString();
        _levelInfo[1]._txtScore.text = PlayerPrefs.GetInt("allAction_" + 3).ToString();
        _levelInfo[2]._txtScore.text = PlayerPrefs.GetInt("allAction_" + 4).ToString();
        _levelInfo[3]._txtScore.text = PlayerPrefs.GetInt("allAction_" + 5).ToString();
        _levelInfo[4]._txtScore.text = PlayerPrefs.GetInt("allAction_" + 6).ToString();
        _levelInfo[5]._txtScore.text = PlayerPrefs.GetInt("allAction_" + 7).ToString();
        _levelInfo[6]._txtScore.text = PlayerPrefs.GetInt("allAction_" + 8).ToString();
        _levelInfo[7]._txtScore.text = PlayerPrefs.GetInt("allAction_" + 9).ToString();
        _levelInfo[8]._txtScore.text = PlayerPrefs.GetInt("allAction_" + 10).ToString();
        _levelInfo[9]._txtScore.text = PlayerPrefs.GetInt("allAction_" + 11).ToString();
        _levelInfo[10]._txtScore.text = PlayerPrefs.GetInt("allAction_" + 12).ToString();
        _levelInfo[11]._txtScore.text = PlayerPrefs.GetInt("allAction_" + 13).ToString();
        _levelInfo[12]._txtScore.text = PlayerPrefs.GetInt("allAction_" + 14).ToString();
        _levelInfo[13]._txtScore.text = PlayerPrefs.GetInt("allAction_" + 15).ToString();
        _levelInfo[14]._txtScore.text = PlayerPrefs.GetInt("allAction_" + 16).ToString();
        _levelInfo[15]._txtScore.text = PlayerPrefs.GetInt("allAction_" + 17).ToString();
        _levelInfo[16]._txtScore.text = PlayerPrefs.GetInt("allAction_" + 18).ToString();
        _levelInfo[17]._txtScore.text = PlayerPrefs.GetInt("allAction_" + 19).ToString();
        _levelInfo[18]._txtScore.text = PlayerPrefs.GetInt("allAction_" + 20).ToString();
        _levelInfo[19]._txtScore.text = PlayerPrefs.GetInt("allAction_" + 21).ToString();
        
       
        #endregion


    }
    public void LoadLevelPassed(string levelName)
    {
        //charger par rapport au nom
        SceneManager.LoadScene(levelName);
    }

    public void saveAction(int lvl)
    {

        actionText.text = PlayerPrefs.GetInt("allAction_" + lvl).ToString();
    }

  
  
    private void CheckStars(string lvl, LevelInfo levelInfo)
    {
        //Debug.Log(PlayerPrefs.GetInt(lvl));
        if (PlayerPrefs.GetInt(lvl) <= levelInfo.etoile3 && PlayerPrefs.GetInt(lvl) != 0)
        {
            levelInfo.imgEtoile[2].sprite = spriteEtoile;
        }
        if (PlayerPrefs.GetInt(lvl) <= levelInfo.etoile2 && PlayerPrefs.GetInt(lvl) != 0)
        {
            levelInfo.imgEtoile[1].sprite = spriteEtoile;
        }
        if (PlayerPrefs.GetInt(lvl) <= levelInfo.etoile1 && PlayerPrefs.GetInt(lvl) != 0)
        {
            levelInfo.imgEtoile[0].sprite = spriteEtoile;
        }
    }

}
