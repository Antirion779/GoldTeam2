
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

    }
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
            _levelInfo[20]._txtScore.text = PlayerPrefs.GetInt("allAction_" + 22).ToString();
            _levelInfo[21]._txtScore.text = PlayerPrefs.GetInt("allAction_" + 23).ToString();
            _levelInfo[22]._txtScore.text = PlayerPrefs.GetInt("allAction_" + 24).ToString();
            _levelInfo[23]._txtScore.text = PlayerPrefs.GetInt("allAction_" + 25).ToString();
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
}
