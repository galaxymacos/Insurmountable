using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interactable
{
	
	public Dialogue dialogue;

	private bool _dialogueStarted = false;



	public override void Interact()
	{
		if (!_dialogueStarted)
		{
			DialogueManager.Instance.StartDialogue(dialogue);
			_dialogueStarted = true;
		}
		else
		{
			DialogueManager.Instance.DisplayNextSentence();
		}
	}
}
 