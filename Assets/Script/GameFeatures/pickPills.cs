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
            GameManager.Instance.AddPills(1);
            Debug.Log("ta eu les sousou)");
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
