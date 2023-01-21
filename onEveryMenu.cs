using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class onEveryMenu : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public GameObject firstSelectedButton;
    public GameObject mainMenu;
    public GameObject player;
    public bool canPressEscToResume;
    public bool mainMenuMusic = false;

    public PlayerInputActions playerControls;
    private InputAction pause;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        pause = playerControls.Player.Pause;
    }

    private void OnDisable()
    {
        pause.Disable();
    }

    void Start()
    {
        Time.timeScale = 0f;
        playerMovement = FindObjectOfType<PlayerMovement>();

        //Clears currently selected object
        EventSystem.current.SetSelectedGameObject(null);

        //set a new selected object.
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);

        if(mainMenuMusic == true)
        {
            FindObjectOfType<AudioManager>().Play("MainMenuBackGround");
        }
    }   

    //If bool is set to true in inspector it will resume the game.
    void Update()
    {
        if(canPressEscToResume == true && pause.triggered)
        {
            HealthManager.maxHealth = 100 + (PlayerStats.HealthLvl * 10);
            Resume();
        }
    } 
    
    public void ButtonSetter()
    {
        Time.timeScale = 0f;

        //Clears currently selected object
        EventSystem.current.SetSelectedGameObject(null);

        //set a new selected object.
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }

    public void QuitGame()
    {
        Application.Quit ();
    } 
    
    public void Resume()
    {
        playerMovement.PlayerControls();
        //Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        //Set max health
        HealthManager.maxHealth = 100 + PlayerStats.HealthLvl * 10;

        //Put music volume back up
        FindObjectOfType<AudioManager>().increaseMusicVolume();

        playerMovement.canPause = true;
        playerMovement.canRest = true;
        StartCoroutine(SlightWait());
    } 

    IEnumerator SlightWait()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 1f;
        //Sets the gameobject that this is attached to to false, basically hiding it (it unticks the box in the heirachy)
        this.gameObject.SetActive(false);

        //Similar to above
        transform.root.gameObject.SetActive(false);
    }
}