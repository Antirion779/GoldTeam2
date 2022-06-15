using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	//On déclare nos variables
	public Dialogue dialogue;
	public DialogueManager manager;

	public void TriggerDialogue()
	{
		manager.StartDialogue(dialogue);
	}

}
