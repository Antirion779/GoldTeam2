
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelButton;

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
    }
    public void LoadLevelPassed(string levelName)
    {
        //charger par rapport au nom
        SceneManager.LoadScene(levelName);
    }
}
