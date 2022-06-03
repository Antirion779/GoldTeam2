
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
        
            _levelInfo[0]._txtScore.text = PlayerPrefs.GetInt("allAction_" + 6).ToString();
            _levelInfo[1]._txtScore.text = PlayerPrefs.GetInt("allAction_" + 5).ToString();


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
