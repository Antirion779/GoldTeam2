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
            GetComponent<CircleCollider2D>().enabled = false;
            Animator child = gameObject.GetComponentInChildren<Animator>();
            child.SetTrigger("Taken");
            MusicList.Instance.PlayTakePills();
            GameManager.Instance.AddPills(1);
            Destroy(gameObject, 2f);
            OpenDialogue();
        }
    }

    private void OpenDialogue()
    {
        if(GameManager.Instance.tutoPart == 1 || dialogueManager != null)
        {
            GameManager.Instance.PlayerAnim.SetBool("Walking", false);
            dialogueManager.UnHideDialogue("John");
        }
    }
}
