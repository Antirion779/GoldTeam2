using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	//On d�clare nos variables
	public Dialogue dialogue;

	public void TriggerDialogue ()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

}
