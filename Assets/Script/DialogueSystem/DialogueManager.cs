using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour {

	[SerializeField] GameObject _startConv;
	[SerializeField] TMP_Text _nameText;
	[SerializeField] TMP_Text _dialogueText;
	[SerializeField] Animator _animator;

	[SerializeField] MiniGameManager _miniGameManager;

	private Queue<string> _sentences;

	void Start () 
	{
		_sentences = new Queue<string>(); 
	}

	public void StartDialogue (Dialogue dialogue) 
	{
		_startConv.SetActive(false);
		_animator.SetBool("IsOpen", true);					

		_nameText.text = dialogue.name;              

		_sentences.Clear();							

		foreach (string sentence in dialogue.sentences)
		{
			_sentences.Enqueue(sentence);  
		}

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
		_dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			_dialogueText.text += letter;
			yield return null;
		}
	}

	public void EndDialogue() 
	{
		_animator.SetBool("IsOpen", false);

		switch(_miniGameManager.State)
        {
			case MiniGameManager.MiniGameState.SHERLOCK:
				_miniGameManager.StartSherlockGame();
				break;
			case MiniGameManager.MiniGameState.BLOOD:
				//
				break;
			case MiniGameManager.MiniGameState.HEART:
				//
				break;
			default:
				_miniGameManager.State = MiniGameManager.MiniGameState.NULL;
				Debug.LogError("MiniGameState enum enter in default");
				break;
        }
	}
}
