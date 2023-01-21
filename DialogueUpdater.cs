using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUpdater : MonoBehaviour
{

    private DialogueManager dialogueManager;
	public int npcUpdater;
	public GameObject NpcToDestroy;


	// Start is called before the first frame update
	void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

	IEnumerator OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			if (npcUpdater == 1)
			{
				dialogueManager.NPClvlup(npcUpdater);

                if (NpcToDestroy != null) 
				{ 
					Destroy(NpcToDestroy);
				}
			}
		}

		else
		{
			yield return null;
		}
	}
}
