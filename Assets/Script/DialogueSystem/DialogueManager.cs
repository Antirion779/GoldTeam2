using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {

	[SerializeField] TMP_Text _nameText;
	[SerializeField] TMP_Text _dialogueText;
	[SerializeField] Image[] imageDialogue;
	[SerializeField] Animator _animator;

	private Queue<string> _sentences;
	public bool _ownPill;

	void Awake () 
	{
		_sentences = new Queue<string>(); 
	}

	public void StartDialogue (Dialogue dialogue) 
	{
		GameManager.Instance.ActualGameState = GameManager.GameState.Dialogue;
		_animator.SetBool("IsOpen", true);					

		_nameText.text = dialogue.name;              

		_sentences.Clear();							

		foreach (string sentence in dialogue.sentences)
		{
			_sentences.Enqueue(sentence);  
		}

		DisplayNextSentence();	
	}

	public void HideDialogue()
    {
		_animator.SetBool("IsOpen", false);
		Debug.Log(_sentences.Count);
		if(_ownPill)
			GameManager.Instance.ActualGameState = GameManager.GameState.PlayerInMovement;
		else
			GameManager.Instance.ActualGameState = GameManager.GameState.PlayerStartMove;
		_ownPill = false;
	}

	public void UnHideDialogue(string speakerName, bool ownPill)
    {
		_ownPill = ownPill;
		GameManager.Instance.ActualGameState = GameManager.GameState.Dialogue;
		_nameText.text = speakerName;
		if (speakerName == "John")
        {
			imageDialogue[0].gameObject.SetActive(true);
			imageDialogue[1].gameObject.SetActive(false);
		}
		else
        {
			imageDialogue[0].gameObject.SetActive(false);
			imageDialogue[1].gameObject.SetActive(true);
		}

		_dialogueText.text = "";
		_animator.SetBool("IsOpen", true);
		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (_sentences.Count == 0) 
		{
			EndDialogue(); 
			return;
		}

		string sentence = _sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		yield return new WaitForSeconds(0.5f);
		_dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			_dialogueText.text += letter;
			yield return null;
		}
	}

	public void EndDialogue()
	{
		Debug.Log("end of dialogue");
		_animator.SetBool("IsOpen", false);
	}
}
