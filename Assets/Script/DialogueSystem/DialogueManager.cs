using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {

	//On déclare nos variables
	[SerializeField] GameObject startConv;
	[SerializeField] TMP_Text nameText;
	[SerializeField] TMP_Text dialogueText;
	[SerializeField] Animator animator;

	[SerializeField] SherlockGame sherlockGame;

	private Queue<string> sentences;

	// Start is called before the first frame update
	void Start () {
		sentences = new Queue<string>(); 
	}

	public void StartDialogue (Dialogue dialogue) //lorsque l'on start le dialogue
	{
		startConv.SetActive(false);
		animator.SetBool("IsOpen", true);					

		nameText.text = dialogue.name;              

		sentences.Clear();							

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);  //on ajoute les sentences
		}

		DisplayNextSentence();	
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0) 
		{
			EndDialogue(); 
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	public void EndDialogue() //Dialogue terminé
	{
		animator.SetBool("IsOpen", false);
		sherlockGame.StartGame();
	}
}
