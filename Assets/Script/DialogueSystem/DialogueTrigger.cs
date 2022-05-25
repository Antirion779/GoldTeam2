using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	//On déclare nos variables
	public Dialogue dialogue;

	public void TriggerDialogue ()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

}
