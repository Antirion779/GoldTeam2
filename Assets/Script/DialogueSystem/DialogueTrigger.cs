using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	//On d�clare nos variables
	public Dialogue dialogue;
	public DialogueManager manager;

	public void TriggerDialogue()
	{
		manager.StartDialogue(dialogue);
	}

}
