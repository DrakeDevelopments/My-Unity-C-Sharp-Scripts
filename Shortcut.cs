using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shortcut : MonoBehaviour, IDamageable
{

    //Attached to ShortcutBreakable Prefab

    public int health = 3;
    public GameObject parent;

    private void Start()
    {
        //Check if player has already unlocked shortcut. If they have it reduces the shortcut's health to 1 and then runs the damage script to finish it off.
        if (PlayerStats.shortcut1 == true)
        {
            health = 1;
            Damage(1);
        }
    }
    public void Damage(int damage)
    {
        health--;
        if(health <= 0)
        {
            Destroy(parent);
            Destroy(gameObject);
            PlayerStats.shortcut1 = true;
        }
    }
}