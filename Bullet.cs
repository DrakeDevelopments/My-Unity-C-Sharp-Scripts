using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Attached to the bullet prefab. See PlayerSword script to update mellee attacks.

    public Rigidbody2D rb;
    public int damage = 30;
    public float speed = 20f;

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("fireBreath");
        rb.velocity = transform.right * speed;                  
        Destroy(gameObject, 1);                                 //Destroys bullets after 2s 
    }

    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        IDamageable damageable = hitInfo.GetComponent<IDamageable>();
        if(damageable != null)
        {
            damageable.Damage(damage);
            Destroy(gameObject);
            soundEffects();
        }

        else
        {
            soundEffects();
            Destroy(gameObject);
        }
    }
    private void soundEffects()
    {
        FindObjectOfType<AudioManager>().Play("fireExtinguish");
    }
}
