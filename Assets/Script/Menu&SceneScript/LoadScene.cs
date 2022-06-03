using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneName;

    public Animator animator;

    public GameObject WinMenuUI;
    private void OnTriggerEnter2D(Collider2D collision )
    {
            if(collision.CompareTag("Player"))
             {
            
            WinMenuUI.SetActive(true);
             }
         
       
    }

    public void loadNextScene()
    {
        
        LoadAndSaveData.instance.SaveData(SceneManager.GetActiveScene().buildIndex);
        animator.SetTrigger("FadeOut");
        
        SceneManager.LoadScene(sceneName);
        WinMenuUI.SetActive(false);
    }

}
