using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    //This script is attached to the ActionCanvas

    //LevelUpMenuScoreUpdater references a lot of the variables in this script
    public static Vector2 respawnPoint = new Vector2(0, 0);
    public Vector2 playerPosition;
    public GameObject playerObject;
    public GameObject canvasLoading;
    public GameObject levelTrigger;
    public GameObject temporaryStorage;
    public Image loadingProgressBar;

    //For Displaying Score text
    public Text scoreText;

    //A record of scenes to be saved/loaded
    public static List<string> currentlyOpenScenes = new List<string>();
    public static List<AsyncOperation> savedScenes = new List<AsyncOperation>();

    //Player Levels
    public static int currentExp;

    public static int HealthLvl = 1;
    public static int StrengthLvl = 1;
    public static int DexterityLvl = 1;
    public static int DefenceLvl = 1;
    public static int ManaLvl = 1;
    public static int AgilityLvl = 1;

    public static int PlayerLvl = HealthLvl + StrengthLvl + DexterityLvl + DefenceLvl + ManaLvl + AgilityLvl;

    //Reference Scripts
    private HealthManager healthManager;
    private LevelLoadingManager levelLoadingManager;
    private PlayerMovement playerMovement;

    public static string fileName;
    public static bool loadCompleted = false;

    //Upgrades
    public static bool doubleJump = false;

    //Warp Locations
    public List<bool> unlockWarps;

    //Shortcuts
    public static bool shortcut1 = false;

    //Bosses Killed
    public static bool boss1 = false;

    //Death spirit orb location
    public static Vector2 spiritOrbPosition;

    //Death spirit Orb amount
    public static int deathSpiritOrbs;

    //Loading manager stuff??
    public static bool loadingManagerScriptActive = false;

    private void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();
        levelLoadingManager = FindObjectOfType<LevelLoadingManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    //The 'floor' integer is taken from the 'RespRest' script which is attached to the level's respawn point. An unlock button script is attached to the warp location buttons that checks for values to warp to. 
    public void Unlock(int floor)
    {
        unlockWarps[floor] = true;
    }

    public void AddExperience(int experienceToAdd)
    {
        currentExp += experienceToAdd;
        scoreText.text = currentExp.ToString();
    }

    public void UpdateExperienceText()
    {
        scoreText.text = currentExp.ToString();
    }

    public void SavePlayer()
    {
        currentlyOpenScenes.Clear();
        playerPosition = playerObject.transform.position;

        if (SceneManager.sceneCount > 0)
        {
            for (int i = 1; i < SceneManager.sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                currentlyOpenScenes.Add(scene.name);
                //Debug.Log(scene.name);
            }
        }

        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        //Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        ShowLoadingBar();
        PlayerData data = SaveSystem.LoadPlayer();

        //Loading player position from save file
        playerPosition = new Vector2(data.playerPosition[0], data.playerPosition[1]);

        //Moving the player object to the saved player position
        playerObject.transform.position = new Vector3(playerPosition[0], playerPosition[1], 0);

        //Scene management
        currentlyOpenScenes = data.currentlyOpenScenes;
        currentlyOpenScenes.Remove("Game");
        savedScenes.Clear();
        
        for (int i = 0; i < currentlyOpenScenes.Count; ++i)
        {
            levelTrigger = GameObject.Find(currentlyOpenScenes[i]);
            levelTrigger.GetComponent<LevelLoadingManager>().isLoaded = true;
            levelTrigger.GetComponent<LevelLoadingManager>().shouldLoad = true;
            savedScenes.Add(SceneManager.LoadSceneAsync(currentlyOpenScenes[i], LoadSceneMode.Additive));
        }
        

        StartCoroutine(LoadingScreen());

        //Loading Player Levels
        currentExp = data.currentExp;
        HealthLvl = data.HealthLvl;
        StrengthLvl = data.StrengthLvl;
        DexterityLvl = data.DexterityLvl;
        DefenceLvl = data.DefenceLvl;
        ManaLvl = data.ManaLvl;
        AgilityLvl = data.AgilityLvl;
        HealthManager.currentHealth = data.currentHealth;

        //Setting health slider and health for some reason
        healthManager.UpdateSliderOnLoad();

        //Loading Upgrades
        doubleJump = data.doubleJump;

        //Loading unlocked Warp locations
        unlockWarps = data.unlockWarps;        
        
        //Loading unlocked shortcuts
        shortcut1 = data.shortcut1;

        boss1 = data.boss1;

        //Display Number of Spririt orbs/exp
        scoreText.text = currentExp.ToString();

        //Loading respawn point
        respawnPoint = new Vector2(data.respawnPoint[0], data.respawnPoint[1]);

        //Unpause game
        playerMovement.canPause = true;
        playerMovement.canRest = true;

        //Load Death Spirit Orbs
        deathSpiritOrbs = data.deathSpiritOrbs;

        //Death spirit orb location
        spiritOrbPosition = new Vector2(data.spiritOrbPosition[0], data.spiritOrbPosition[1]);

        //Load in death orb
        healthManager.instantiateDeathOrbs();

        //Audio Manager
        FindObjectOfType<AudioManager>().StopPlaying("MainMenuBackGround");
        //Might have to get the below to change based on current scene
        FindObjectOfType<AudioManager>().Play("Scene1");
    }

    public void WarpPlayer()
    {
        //Prior to this the warp button has been pressed, and the player object has been moved to the warp location
        
        //Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        //Display loading bar
        ShowLoadingBar();

        //Unload all scenes
        if (SceneManager.sceneCount > 0)
        {
            for (int i = 0; i < SceneManager.sceneCount - 1; ++i)
            {
                SceneManager.UnloadSceneAsync(currentlyOpenScenes[i]);
                levelTrigger = GameObject.Find(currentlyOpenScenes[i]);
                levelTrigger.GetComponent<LevelLoadingManager>().isLoaded = false;
                levelTrigger.GetComponent<LevelLoadingManager>().shouldLoad = false;
            }
        }

        //clears the variables below for later
        currentlyOpenScenes.Clear();
        savedScenes.Clear();

        //Runs a method from the healthmanager to save the scenes/levels that the player is currently in contact with
        healthManager.SaveLevelScene();

        //Saving scenes so that when the loading coroutine is ran it knows what to load
        for (int i = 0; i < currentlyOpenScenes.Count; ++i)
        {
            levelTrigger = GameObject.Find(currentlyOpenScenes[i]);
            levelTrigger.GetComponent<LevelLoadingManager>().isLoaded = true;
            levelTrigger.GetComponent<LevelLoadingManager>().shouldLoad = true;
            savedScenes.Add(SceneManager.LoadSceneAsync(currentlyOpenScenes[i], LoadSceneMode.Additive));
        }

        StartCoroutine(LoadingScreen());
    }

    public void NewGameCutscene()
    {
        //Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        SceneManager.LoadSceneAsync("OpeningCutscene", LoadSceneMode.Additive);

        //Audio Manager
        FindObjectOfType<AudioManager>().StopPlaying("MainMenuBackGround");

    }

    public void NewGame()
    {
        //Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        ShowLoadingBar();

        savedScenes.Clear();
        currentlyOpenScenes.Clear();

        //These are the scenes we start in
        currentlyOpenScenes.Add("Level1");
        //currentlyOpenScenes.Add("Level2");
        //currentlyOpenScenes.Add("Level5");

        //This starts loading the scenes
        for (int i = 0; i < currentlyOpenScenes.Count; ++i)
        {
            levelTrigger = GameObject.Find(currentlyOpenScenes[i]);
            levelTrigger.GetComponent<LevelLoadingManager>().isLoaded = true;
            levelTrigger.GetComponent<LevelLoadingManager>().shouldLoad = true;
            savedScenes.Add(SceneManager.LoadSceneAsync(currentlyOpenScenes[i], LoadSceneMode.Additive));
        }

        StartCoroutine(LoadingScreen());

        //Setting Spirit Orbs or experience points to zero.
        currentExp = 0;

        //Display Number of Spririt orbs/exp
        scoreText.text = currentExp.ToString();

        //setting Standard Levels to 1.
        HealthLvl = 1;
        StrengthLvl = 1;
        DexterityLvl = 1;
        DefenceLvl = 1;
        ManaLvl = 1;
        AgilityLvl = 1;

        //Setting all unlocked abilities to locked
        doubleJump = false;

        //Set location of default spawn point.
        respawnPoint = new Vector2(0f, 0f);

        //Resets Health
        healthManager.UpdateSlider();

        //Setting unlocked warps to all locked.
        for (int i = 0; i < unlockWarps.Count; i++)
        {
            unlockWarps[i] = false;
        }

        if (unlockWarps.Contains(true))
        {
            Debug.Log("failure");
        }
        

        //Locking all shortcuts.
        shortcut1 = false;

        //Resetting bosses killed.
        boss1 = false;

        //Setting player start position.
        playerObject.transform.position = new Vector3(0, 0, 0);

        //Unpausing time kind of...
        playerMovement.canPause = true;
        playerMovement.canRest = true;

        //Audio Manager
        FindObjectOfType<AudioManager>().StopPlaying("MainMenuBackGround");
        //Might have to get the below to change based on current scene
        FindObjectOfType<AudioManager>().Play("Scene1");
    }

    public void ShowLoadingBar()
    {
        canvasLoading.SetActive(true);
    }

    IEnumerator LoadingScreen()
    {
        float totalProgress = 0;
        //Iterate through all the scenes to load
        for (int i = 0; i < savedScenes.Count; ++i)
        {
            //Debug.Log(savedScenes.Count);
            savedScenes[i].allowSceneActivation = false;
            while (!savedScenes[i].isDone)
            {;
                //Adding the scene progress to the total progress
                totalProgress += savedScenes[i].progress;

                //the fillAmount needs a value between 0 and 1, so we devide the progress by the number of scenes to load
                loadingProgressBar.fillAmount = totalProgress / savedScenes.Count;

                //hide the progress bar
                if (savedScenes[i].progress >= 0.8f)
                {
                    savedScenes[i].allowSceneActivation = true;
                }

                if (totalProgress / savedScenes.Count >= 0.89f)
                {
                    //Enable Player controls and disable UI controls
                    playerMovement.PlayerControls();
                    yield return new WaitForSecondsRealtime(0.5f);
                    Time.timeScale = 1;
                    canvasLoading.SetActive(false);
                    loadCompleted = true;
                }
                yield return null;
            }
        }
    }

    //Deleting profiles
    public void DeletePlayer1()
    {
        SaveSystem.DeletePlayer1();
    }

    public void DeletePlayer2()
    {
        SaveSystem.DeletePlayer2();
    }

    public void DeletePlayer3()
    {
        SaveSystem.DeletePlayer3();
    }

    public void UnloadScenes()
    {
        //This stops lava/spike/poison damage from continuing after you go back to the menu and load a new profile/new game
        healthManager.CancelInvoke();

        //Destroys every game object in the temporary story game object found in the 'Game' scene
        foreach (Transform child in temporaryStorage.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        //Prevents being able to pause/rest in game
        playerMovement.canPause = false;
        playerMovement.canRest = false;

        //Unloads all scenes
        if (SceneManager.sceneCount > 0)
        {
            for (int i = 0; i < SceneManager.sceneCount - 1; ++i)
            {
                SceneManager.UnloadSceneAsync(currentlyOpenScenes[i]);
                levelTrigger = GameObject.Find(currentlyOpenScenes[i]);
                levelTrigger.GetComponent<LevelLoadingManager>().isLoaded = false;
                levelTrigger.GetComponent<LevelLoadingManager>().shouldLoad = false;
            }
        }
        FindObjectOfType<AudioManager>().StopPlaying("Scene1");
        FindObjectOfType<AudioManager>().Play("MainMenuBackGround");
    }

}
