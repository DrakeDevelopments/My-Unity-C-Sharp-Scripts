using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
	//This is attached to the DialogueManager game object

	/*
	1. Each NPC needs their own method which should match their name used in the DialogueTrigger component in the NPC game object.
	2. see Curator method and int for example
	*/
	public DialogueTrigger dialogueTrigger;

	public TextMeshProUGUI nameText;
	public TextMeshProUGUI dialogueText;
	public GameObject prompt;

	public Animator animator;

	private Queue<string> sentences;

	//NPC LEVELS (Looks at dialogue scripts, Level is linked to what NPC says)
	public int curatorlvl = 1;
	public int gobbolvl = 1;
	public int estateAgentlvl = 1;

	//NPC in collision box
	public string currentNPC;

	public void findNPC()
    {
		//we want to get the name of the npc 
		Invoke(currentNPC, 0f);
    }

	public void NPClvlup(int npcUpdater)
	{
		switch (npcUpdater)
		{
			case 1:
				if(curatorlvl == 1)
                {
					curatorlvl = 3;
                }
				break;

			default:;
				break;
		}
	}

	public void Curator()
	{
		switch (curatorlvl)
        {
			case 3:
				StartDialogue3(dialogueTrigger.dialogue);
				break;

			case 2:
				StartDialogue2(dialogueTrigger.dialogue);
				break;

			case 1:
				StartDialogue1(dialogueTrigger.dialogue);
				
				break;
			
			default:
				StartDialogue1(dialogueTrigger.dialogue);
				break;
        }
	}

	public void Gobbo()
	{
		switch (gobbolvl)
		{
			case 3:
				StartDialogue3(dialogueTrigger.dialogue);
				break;

			case 2:
				StartDialogue2(dialogueTrigger.dialogue);
				break;

			case 1:
				StartDialogue1(dialogueTrigger.dialogue);

				break;

			default:
				StartDialogue1(dialogueTrigger.dialogue);
				break;
		}
	}

	public void EstateAgent()
	{
		switch (estateAgentlvl)
		{
			case 3:
				StartDialogue3(dialogueTrigger.dialogue);
				break;

			case 2:
				StartDialogue2(dialogueTrigger.dialogue);
				break;

			case 1:
				StartDialogue1(dialogueTrigger.dialogue);

				break;

			default:
				StartDialogue1(dialogueTrigger.dialogue);
				break;
		}
	}

	// Use this for initialization
	void Start()
	{
		sentences = new Queue<string>();
	}

	public void StartPrompt(Dialogue dialogue)
    {
		prompt.SetActive(true);

		currentNPC = dialogue.name;
    }

	public void ClosePrompt()
	{ 
		prompt.SetActive(false);
	}

	public void StartDialogue1(Dialogue dialogue)
	{
		ClosePrompt();
		
		animator.SetBool("IsOpen", true);

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences1)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void StartDialogue2(Dialogue dialogue)
	{
		ClosePrompt();

		animator.SetBool("IsOpen", true);

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences2)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void StartDialogue3(Dialogue dialogue)
	{
		ClosePrompt();

		animator.SetBool("IsOpen", true);

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences3)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void StartDialogue4(Dialogue dialogue)
	{
		ClosePrompt();

		animator.SetBool("IsOpen", true);

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences4)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
			if (currentNPC == "Curator" && curatorlvl == 1)
			{
				curatorlvl = 2;
				Destroy(dialogueTrigger.gameObject);
			}

			EndDialogue();
			return;
		}
		
		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
			dialogueTrigger.canContinueSentence = true;
		}
	}

	public void EndDialogue()
	{
		animator.SetBool("IsOpen", false);

		if(dialogueTrigger == null)
        {
			ClosePrompt();
        }

        else
        {
			dialogueTrigger.promptDisplayed = false;
			dialogueTrigger.canContinueSentence = false;
		}
	}
}