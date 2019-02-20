using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
	public Animator animator;
	public Text dialogueText;

	public Text nameText;

	private Queue<string> sentences;
	public static DialogueManager Instance { get; private set; }

	private void Awake()
	{
		if (Instance == null) Instance = this;
	}

	// Start is called before the first frame update
	private void Start()
	{
		sentences = new Queue<string>();
	}


	public void StartDialogue(Dialogue dialogue)
	{
		animator.SetBool("isOpen", true);

		nameText.text = dialogue.name;

		sentences.Clear();
		foreach (var sentence in dialogue.sentences) sentences.Enqueue(sentence);
		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		var sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));

	}

	private IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach (var letter in sentence)
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	private void EndDialogue()
	{
		animator.SetBool("isOpen", false);
		print("End of conversation.");
	}
}