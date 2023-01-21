using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespRest : MonoBehaviour, IInteractable
{
    /*
    This is attached to each of the respawn points    
    */

    private PlayerStats playerstats;
    private PlayerMovement playerMovement;

    public int floorID;         //this needs to be set in the inspector of each respawn point. 0 WILL BE THE TUTORIAL LEVEL respawn point.

    public GameObject restCanvas;
    public GameObject restMenuButtons;

    void Start()
    {
        //finding the scripts that will be used.
        playerstats = FindObjectOfType<PlayerStats>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    public void Interact()
    {
        if(playerMovement.canRest == true)
        {
            //Prevent pause being activated
            playerMovement.canPause = false;
            playerMovement.canRest = false;

            //Lower Music
            FindObjectOfType<AudioManager>().reduceMusicVolume();

            //Switch to UI controls
            playerMovement.UIControls();

            //Unlock Cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //Load Canvas bits
            restCanvas.SetActive(true);
            restMenuButtons.SetActive(true);
            restMenuButtons.GetComponent<onEveryMenu>().ButtonSetter();

            //Unlock the ability to warp to the corresponding rest point
            playerstats.Unlock(floorID - 1);

            //Sets the respawn point in the PlayerStats script.
            PlayerStats.respawnPoint = transform.position;

            //Put health back to max.
            HealthManager.currentHealth = HealthManager.maxHealth;
        }
    }
}