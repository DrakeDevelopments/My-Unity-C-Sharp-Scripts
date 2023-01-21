using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    //public GameObject DialogueCanvas;
	public Dialogue dialogue;

    private DialogueManager dialogueManager;
    private PlayerStats playerStats;

    public bool promptDisplayed = false;
    public bool canInteract = false;
    public bool canContinueSentence = false;

    public void TriggerDialogue()
	{
        dialogueManager.findNPC();
    }

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        playerStats = FindObjectOfType<PlayerStats>();
    }

    public void Interact()
    {
        if (canInteract == true)
        {
            TriggerDialogue();
            canInteract = false;
        }

        if (canContinueSentence == true)
        {
            dialogueManager.DisplayNextSentence();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //If NPC comes into contact with player
        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            dialogueManager.dialogueTrigger = this;
            if(playerMovement.prompt.activeInHierarchy)
            {
                dialogueManager.StartPrompt(dialogue);
                canInteract = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //If NPC is no longer in contact with plyer.
        PauseMenu player = other.GetComponent<PauseMenu>();
        if (player != null)
        {
            dialogueManager.ClosePrompt();
            dialogueManager.EndDialogue();
            canInteract = false;
            dialogueManager.dialogueTrigger = null;
        }
    }

}