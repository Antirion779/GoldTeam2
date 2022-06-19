using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {

	[SerializeField] TMP_Text _nameText;
	[SerializeField] TMP_Text _dialogueText;
	[SerializeField] Image[] imageDialogue;
	public Animator _animator;

	private Queue<string> _sentences;
	public bool _ownPill;
	public int _dialoguePart = 0;

	[SerializeField] private GameObject _dialogueLoc;
	[SerializeField] private GameObject _dialogueCross1;
	[SerializeField] private GameObject _dialogueCross2;

	void Awake () 
	{
		_sentences = new Queue<string>(); 
	}

	public void StartDialogue (Dialogue dialogue) 
	{
		_dialoguePart++;
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
		if(TestIfEmoteAreNull() == false)
			HideTutoEmote();
		_animator.SetBool("IsOpen", false);
		if(_dialoguePart == 2)
			GameManager.Instance.ActualGameState = GameManager.GameState.PlayerInMovement;
		else
			GameManager.Instance.ActualGameState = GameManager.GameState.PlayerStartMove;
		_ownPill = false;
	}

	public void UnHideDialogue(string speakerName)
    {
		_dialoguePart++;
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
		if (TestIfEmoteAreNull() == false)
			UpdateTutoEmote();

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

	private void UpdateTutoEmote()
    {
		switch (_dialoguePart)
		{
			case 1:
				_dialogueCross1.SetActive(true);
				break;
			case 2:
				_dialogueLoc.SetActive(true);
				break;
			case 3:
				_dialogueCross2.SetActive(true);
				break;
			default:
				Debug.LogError("dialogue manager Switch enter in default");
				break;
		}
	}

	private void HideTutoEmote()
    {
		if (TestIfEmoteAreNull() == false)
        {
			_dialogueCross1.SetActive(false);
			_dialogueCross2.SetActive(false);
			_dialogueLoc.SetActive(false);
		}
	}

	private bool TestIfEmoteAreNull()
    {
		if (_dialogueLoc == null || _dialogueCross1 == null || _dialogueCross2 == null)
			return true;
		else
			return false;
    }
}
