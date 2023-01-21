using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


public class ItemCollect : MonoBehaviour
{
    /*  
        Purpose:    This script is used for detecting whether the player is touching the spirit orb.
                    Each enemy has a spirit orb value assigned to itself

        Where is it attached: Prefabs > SpiritOrbs
    */

    private PlayerStats playerstats;

    private GameObject tempStorage;

    //Enemy or player (on death) sets the value of the spiritOrb int.
    public int spiritOrbs;

    //When player dies this gets set to true. When enemy dies it gets set to false.
    public bool respawnOrbs = false;

    //On start the private int realValue takes the spiritOrb value so that each spirit orb has its own indivual value.
    private int realValue;

    //On start the private bool takes the public value so that each spirit orb is treated correctly.
    private bool realRespawnOrb = false;
    private void Awake()
    {
        realRespawnOrb = respawnOrbs;
        realValue = spiritOrbs;
    }
    void Start()
    {
        gameObject.GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = new Color32(0, 255, 255, 1);
        if (realRespawnOrb == true)
        {
            tempStorage = GameObject.Find("TemporaryStorage");
            gameObject.transform.parent = tempStorage.transform;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            gameObject.GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = new Color32(255, 0, 0, 1);
        }

        playerstats = FindObjectOfType<PlayerStats>();

    }


    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Collector")) //The collector tag is on the 'Item Collect' game object on the player.
        {
            //Adding experience
            playerstats.AddExperience(realValue);
            //Debug.Log("I picked up "+realValue+" orbs!");
            //Stop duplicate error from happening
            realValue = 0;

            //Destroying the spirit orb
            Destroy(gameObject);

            //Setting death spirit orbs to 0 on collection of them. Otherwise you can save and reload to keep them in after collection!
            if (realRespawnOrb==true)
            {
                PlayerStats.deathSpiritOrbs = 0;
            }
            yield return null;
        }
    }
}