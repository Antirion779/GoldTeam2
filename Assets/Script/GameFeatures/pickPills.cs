using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickPills : MonoBehaviour
{
   
    [SerializeField] DialogueManager dialogueManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gameObject.GetComponentInChildren<Animator>().SetTrigger("Taken");
            MusicList.Instance.PlayTakePills();
            GameManager.Instance.AddPills(1);
            Destroy(gameObject);
            OpenDialogue();
        }
    }

    private void OpenDialogue()
    {
        if(GameManager.Instance.tutoPart == 1 || dialogueManager != null)
        {
            dialogueManager.UnHideDialogue("John");
        }
    }
}
