using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneName;

    public Animator animator;

    public GameObject WinMenuUI;
    public GameObject[] etoiles;

    private void Awake()
    {
        if(sceneName == "" || WinMenuUI == null || etoiles.Length != 3)
            Debug.Log("<color=gray>[</color><color=#FF00FF>LoadScene</color><color=gray>]</color><color=red> ATTENTION </color><color=#F48FB1> Some object are null </color><color=gray>-</color><color=cyan> Object Name : </color><color=yellow>" + transform.name + "</color><color=cyan> Scene Name to Load : </color><color=yellow>" + sceneName + "</color><color=cyan> Win Menu </color><color=yellow>" + WinMenuUI + "</color><color=cyan> Etoiles set : </color><color=yellow>" + etoiles.Length + "</color>");
    }

    private void OnTriggerEnter2D(Collider2D collision )
    {
        if (sceneName != "" && WinMenuUI != null && etoiles.Length == 3)
        {
            if(collision.CompareTag("Player"))
            {
                GameManager.Instance.ActualGameState = GameManager.GameState.End;
                WinMenuUI.SetActive(true);
                etoiles[0].SetActive(true);
            }
        }
    }

    public void loadNextScene()
    {
        LoadAndSaveData.instance.SaveData(SceneManager.GetActiveScene().buildIndex);
        animator.SetTrigger("FadeOut");
        
        SceneManager.LoadScene(sceneName);
        WinMenuUI.SetActive(false);
        etoiles[0].SetActive(false);
    }

}
