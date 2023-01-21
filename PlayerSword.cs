using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour

    //Where: This script is attached to the player object. Bullets need to be updated from the "Bullet" script. 

    //Animation. These methods are motly activated by animation events.

    //What: The purpose of this script is to find enemies within attack radius after pressing attack button, and then give them damage.

    //What Secondary: Used for pogo hop (Character bounces when attacking down).

{
    public Animator animator;                                           //Inspector pane lets you drop animator into this.
    public Transform nAttackPoint;                                       //Inspector pane lets you drop object into this. It is the circle centre point transform co-ordinates.
    public Transform uAttackPoint;                                       //Inspector pane lets you drop object into this. It is the circle centre point transform co-ordinates.
    public Transform bAttackPoint;                                       //Inspector pane lets you drop object into this. It is the circle centre point transform co-ordinates.
    public Transform dAttackPoint;                                       //Inspector pane lets you drop object into this. It is the circle centre point transform co-ordinates.

    public float attackRange = 0.5f;                                    //Gives radius of circle. Is also used by the MeleeAttack script.

    public LayerMask DamageLayers;                                         //Give a dropdown menu in the inspector pane to select applicable layers.
    public LayerMask SpikeLayer;                                         //Give a dropdown menu in the inspector pane to select applicable layers.

    private int damage = 30;

    void OnDrawGizmosSelected()                                         //Draws the circle in which enemy will be detected. Click on player object whilst gizmos are active
    {
        if (nAttackPoint == null)
            return;
        Gizmos.DrawWireSphere(nAttackPoint.position, attackRange);
        
        if (uAttackPoint == null)
            return;
        Gizmos.DrawWireSphere(uAttackPoint.position, attackRange);
             
        if (bAttackPoint == null)
            return;
        Gizmos.DrawWireSphere(bAttackPoint.position, attackRange);

        if (dAttackPoint == null)
            return;
        Gizmos.DrawWireSphere(dAttackPoint.position, attackRange);
    }

    private void DealDamage(Collider2D enemy)
    {
        damage = 24 + PlayerStats.StrengthLvl;

        IDamageable damageable = enemy.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(damage);
        }   
    }

    void nAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(nAttackPoint.position, attackRange, DamageLayers);         //Checks for collisions within circle around the triggerpoint on the NPC layer.

        foreach (Collider2D enemy in hitEnemies)
        {
            DealDamage(enemy);
        }
    }

    void uAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(uAttackPoint.position, attackRange, DamageLayers);         //Checks for collisions within circle around the triggerpoint on the NPC layer.

        foreach (Collider2D enemy in hitEnemies)
        {
            DealDamage(enemy);
        }
    }

    void bAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(bAttackPoint.position, attackRange, DamageLayers);         //Checks for collisions within circle around the triggerpoint on the NPC layer.

        foreach (Collider2D enemy in hitEnemies)
        {
            DealDamage(enemy);
        }
    }

    void dAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(dAttackPoint.position, attackRange, DamageLayers);         //Checks for collisions within circle around the triggerpoint on the NPC layer.

        foreach (Collider2D enemy in hitEnemies)
        {
            DealDamage(enemy);
            CharacterController2D controllerpogo = GetComponent<CharacterController2D>();
            StartCoroutine(controllerpogo.Pogo());
        }

        Collider2D[] hitSpike = Physics2D.OverlapCircleAll(dAttackPoint.position, attackRange, SpikeLayer);

        foreach (Collider2D spike in hitSpike)
        {
            CharacterController2D controllerpogo = GetComponent<CharacterController2D>();
            StartCoroutine(controllerpogo.Pogo());
        }

    }
}
