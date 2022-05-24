using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testLoadScene : MonoBehaviour
{
    public string sceneName;
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
        Debug.Log("c save");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }

}
