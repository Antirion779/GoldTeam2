using UnityEngine;
using System.Linq;

public class LoadAndSaveData : MonoBehaviour
{
    public static LoadAndSaveData instance;

    //public PlayerMovement playerMovement;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de LoadAndSaveData dans la scène");
            return;
        }

        instance = this;
    }


    public void SaveData(int lvl, string NameSave)
    {
        
        NameSave = "allAction_";
        

        if (PlayerPrefs.GetInt(NameSave + lvl) > GameManager.Instance.ActionPoint && PlayerPrefs.HasKey(NameSave + lvl) == true)
        {
            
            PlayerPrefs.SetInt(NameSave + lvl, GameManager.Instance.ActionPoint);
            Debug.Log("The key " + NameSave + " exists");
        }
        else if (PlayerPrefs.HasKey(NameSave + lvl) == false )
        {
            PlayerPrefs.SetInt(NameSave + lvl, GameManager.Instance.ActionPoint);
            Debug.Log("The key " + NameSave + "DONT DONT exists");
        }


       
       



        //on vérifie si le niveau que on fait n'est pas un niveau que l'on a deja fait
        if (CurrentSceneManager.instance.levelToUnlock > PlayerPrefs.GetInt("levelReached", 1))
        {
            //la on enregistre un nouveau niveau 
            PlayerPrefs.SetInt("levelReached", CurrentSceneManager.instance.levelToUnlock);
        }

      
    }


}
