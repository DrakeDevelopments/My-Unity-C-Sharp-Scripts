using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    //This script is attached to the player.

    //The purpose of the script is to manage the health when the player gets hurts, and manage death.

    public static float maxHealth = 100 + (PlayerStats.HealthLvl*10);
    public static float currentHealth;
    public static float damage = 0;
    public float lavadamage = 25;

    public GameObject temporaryStorage;
    public GameObject SpiritOrbPrefab;
    public GameObject DeathMessage;

    private PlayerStats playerstats;
    public HealthBar healthBar;

    public LayerMask levelMask;

    //when the game starts current health is set to max health
    void Start()
    {
        playerstats = FindObjectOfType<PlayerStats>();
        maxHealth = 100 + (PlayerStats.HealthLvl * 10);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public static void TakeDamage(float damage)
    {
        currentHealth -= damage;
        FindObjectOfType<AudioManager>().Play("Ouch");
    }

    //Use the following for spike/lava damage

    public void LavaDamage()
    {
        FindObjectOfType<AudioManager>().Play("Ouch");
        currentHealth -= lavadamage;
    }


    void Update()
    {
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)                     //Respawn logic - when health is 0 player goes to respawn point held in PlayerStats script.
        {
            CancelInvoke();
            //Start death message
            StartCoroutine(DisplayDeathMessage());

            //Set value of spirit orbs to their current value
            SpiritOrbPrefab.GetComponent<ItemCollect>().spiritOrbs = PlayerStats.currentExp;

            //Change spriit orb script so that it knows they have been dropped by the player
            SpiritOrbPrefab.GetComponent<ItemCollect>().respawnOrbs = true;

            //Clear temp storage of old respawn spirit orbs
            foreach (Transform child in temporaryStorage.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            //Drop the spirit orbs at the death position +1 in the Y axis
            Instantiate(SpiritOrbPrefab, transform.position + new Vector3(0,1,0), transform.rotation);

            //Save the position of the spirit orbs in the stats
            PlayerStats.spiritOrbPosition = transform.position + new Vector3(0, 1, 0);

            //Setting the number of death spirit orbs
            PlayerStats.deathSpiritOrbs = PlayerStats.currentExp;


            //Reset health back to max
            currentHealth = maxHealth;

            //Sets Velocity to 0 for player object to stop it clipping into floor on respawn.
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            //Warp player to respawn point (Need to update this at some point to use proper reload mechanics)
            transform.position = new Vector2(PlayerStats.respawnPoint.x, PlayerStats.respawnPoint.y);

            Time.timeScale = 0;

            playerstats.WarpPlayer();

            //Set spirit orbs/experience to 0
            PlayerStats.currentExp = 0;

            //Update spirit orb text
            playerstats.UpdateExperienceText();
        }
    }

    public void instantiateDeathOrbs()
    {
        if (PlayerStats.deathSpiritOrbs>0)
        {
            //Set value of spirit orbs to their current value
            SpiritOrbPrefab.GetComponent<ItemCollect>().spiritOrbs = PlayerStats.deathSpiritOrbs;

            //Change spriit orb script so that it knows they have been dropped by the player
            SpiritOrbPrefab.GetComponent<ItemCollect>().respawnOrbs = true;

            //Instantiate the spirit orbs to the death location
            Instantiate(SpiritOrbPrefab, new Vector3(PlayerStats.spiritOrbPosition.x, PlayerStats.spiritOrbPosition.y, 0), transform.rotation);
        }
    }

    public void UpdateSlider()
    {
        maxHealth = 100 + (PlayerStats.HealthLvl * 10);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void UpdateSliderOnLoad()
    {
        maxHealth = 100 + (PlayerStats.HealthLvl * 10);
        healthBar.SetMaxHealth(maxHealth);
    }

    public IEnumerator DisplayDeathMessage()
    {
        //Death Message displayed
        DeathMessage.SetActive(true);
        yield return new WaitForSecondsRealtime(2f); //waitforseconds doesn't work if using timescale 0 so you have to use realtime at the end.
        DeathMessage.SetActive(false);
    }

    public void SaveLevelScene()
    {
        Collider2D[] levelScene = Physics2D.OverlapAreaAll(gameObject.transform.position, gameObject.transform.position, levelMask);         //Checks for collisions within circle around the triggerpoint on the NPC layer.

        if (levelScene != null)
        {
            foreach (Collider2D col in levelScene)
            {
                //Debug.Log(col.gameObject.name);
                PlayerStats.currentlyOpenScenes.Add(col.gameObject.name);
            }
        }
    }
}
