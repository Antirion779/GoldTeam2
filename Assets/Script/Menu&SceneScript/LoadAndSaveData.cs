using UnityEngine;
using System.Linq;

public class LoadAndSaveData : MonoBehaviour
{
    public static LoadAndSaveData instance;



    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de LoadAndSaveData dans la scène");
            return;
        }

        instance = this;
    }

    private void Start()
    {
       // Achievement.Instance.allAction = PlayerPrefs.GetInt("allAction", 0);
    }
    public void SaveData()
    {
        //PlayerPrefs.SetInt("allAction", Achievement.Instance.allAction);
        //on vérifie si le niveau que on fait n'est pas un niveau que l'on a deja fait
        if (CurrentSceneManager.instance.levelToUnlock > PlayerPrefs.GetInt("levelReached", 1))
        {
            //la on enregistre un nouveau niveau 
            PlayerPrefs.SetInt("levelReached", CurrentSceneManager.instance.levelToUnlock);
        }

      
    }
}
