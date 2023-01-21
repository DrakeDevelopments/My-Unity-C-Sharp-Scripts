using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    /*  
        Purpose:    Pausing logic, when pause button is pressed from PlayerMovement script.
    */

    //  Where is it attached: Game > Player > ItemCollect

    private PlayerMovement playerMovement;

    public GameObject pauseCanvas;
    public GameObject pauseMenu;

    void Start()
    {
        //finding the scripts that will be used.
        playerMovement = FindObjectOfType<PlayerMovement>();
    }
        
    public void Pause()
    {
        playerMovement.canPause = false;
        playerMovement.canRest = false;

        //Reduce music volume
        FindObjectOfType<AudioManager>().reduceMusicVolume();

        //Switch to UI controls
        playerMovement.UIControls();

        //Unlock Cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        pauseCanvas.SetActive(true);
        pauseMenu.SetActive(true);
        pauseMenu.GetComponent<onEveryMenu>().ButtonSetter();
    }
}