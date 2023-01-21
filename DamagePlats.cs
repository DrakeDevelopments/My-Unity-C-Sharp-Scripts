using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlats : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D hitInfo)                       //searches for colliders activating trigger
    {
        HealthManager player = hitInfo.GetComponent<HealthManager>();   //tries to get Playerstats script from detected object (if it exists, otherwise null is returned)
        if (player != null)                                         //if Playerstats script is found the next bit of code will activate
        {
            player.InvokeRepeating("LavaDamage", 0.01f, 0.2f);       //method in Playerstats script is called after 0.01s, then called every 0.2s 
        }
    }

    void OnTriggerExit2D(Collider2D hitInfo)                        //searches for colliders de-activating trigger
    {
        HealthManager player = hitInfo.GetComponent<HealthManager>();   //tries to get Playerstats script from detected object (if it exists, otherwise null is returned)
        if (player != null)                                         //if Playerstats script is found the next bit of code will activate
        {
            player.CancelInvoke();                                  //InvokeRepeat is cancelled

        }
    }

}