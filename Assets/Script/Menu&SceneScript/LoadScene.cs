using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneName;

    public Animator animator;
    private void OnTriggerEnter2D(Collider2D collision )
    {
            if(collision.CompareTag("Player"))
             {
            StartCoroutine(loadNextScene());
             }
         
       
    }

    public IEnumerator loadNextScene()
    {
        LoadAndSaveData.instance.SaveData();
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }

}
