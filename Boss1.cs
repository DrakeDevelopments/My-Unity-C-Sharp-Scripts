using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
//using System.Security.Cryptography;
using UnityEngine;

public class Boss1 : MonoBehaviour, IDamageable

    //This script is attached to the Enemy Prefab

{
    public Transform playerTransform;

    private GameObject player;
    public GameObject SpiritOrbPrefab;

    private bool enemyIdle = true;
    public bool hasWings = false;
    public bool canFlap = true;
    private bool facingLeft = true;
    private bool facingRight = false;
    private bool canAttack = true;

    public float EnemyCurrentHealth;

    public Animator animator;

    //Start of variables contained in EnemyCreator scriptable object

    private float attack = 10f;
    private float defence = 10f;
    private float maxHealth = 10f;
    private float attackRange = 1f;           //how many units away the enemy can attack from
    private float attackFrequency = 1f;       //how often damage is dealt
    private float speed = 14f;                 // speed of enemy
    private float m_MovementSmoothing = 1f;   // How much to smooth out the movement


    //End of variables contained in EnemyCreator scriptable object

    private Transform enemyTransform;
    Vector2 enemyPosition;
    Vector2 playerPosition;

    private Rigidbody2D m_Rigidbody2D;
    private int chaseDirection;

    private Vector3 m_Velocity = Vector3.zero;

    private void Start() //change this to Start when we build
    {
        if (PlayerStats.doubleJump == true) 
        {
            Destroy(gameObject);
        }
            
        EnemyCurrentHealth = maxHealth;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        enemyTransform = transform;
        enemyPosition = transform.position;
        playerPosition = player.transform.position;

        if (Vector2.Distance(playerPosition, enemyPosition) > 10)
        {
            enemyIdle = true;
        }

        else
        {
            enemyIdle = false;
        }

        if (hasWings == true & playerPosition.y - enemyPosition.y > 0)
        {
            StartCoroutine(Flap());
        }

        if (playerPosition.x - enemyPosition.x > 0.2)
        {
            chaseDirection = 1;

            if (facingLeft == true)
            {
                Flip();
                facingRight = true;
                facingLeft = false;
            }
        }

        if (playerPosition.x - enemyPosition.x < -0.2)
        {
            chaseDirection = -1;
            if (facingRight == true)
            {
                Flip();
                facingRight = false;
                facingLeft = true;
            }
        }

        if (playerPosition.x - enemyPosition.x >= -0.2 & playerPosition.x - enemyPosition.x <= 0.2)
        {
            chaseDirection = 0;
        }

        if(Vector2.Distance(playerPosition, enemyPosition) <= attackRange & canAttack == true)
        {
            StartCoroutine(ActivateAttack());
        }
    }

    private void FixedUpdate()
    {
        if (enemyIdle == false)
        {
            Vector3 targetVelocity = new Vector2(chaseDirection * speed, m_Rigidbody2D.velocity.y);
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }
    }

    private IEnumerator ActivateAttack()
    {
        canAttack = false;
        HealthManager.TakeDamage(attack);
        yield return new WaitForSeconds(attackFrequency);
        canAttack = true;
    }

    public void Damage(int damage)

    {
        EnemyCurrentHealth -= damage * ((200 - defence)/200);

        //animator.SetTrigger("Staggered");
        // m_Rigidbody2D.AddForce(new Vector2(0f, 200000));


        if (EnemyCurrentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerStats.boss1 = true;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        Destroy(gameObject);
        this.enabled = false;
        SpiritOrbPrefab.GetComponent<ItemCollect>().respawnOrbs = false;
        SpiritOrbPrefab.GetComponent<ItemCollect>().spiritOrbs = 200;
        Instantiate(SpiritOrbPrefab, enemyPosition, enemyTransform.rotation);
        PlayerStats.doubleJump = true;
    }

    private void Flip()
	{
            transform.Rotate(0f, 180f, 0f);
	}
    private IEnumerator Flap()
    {
        if (canFlap == true & enemyIdle == false)
        {
            canFlap = false;
            yield return new WaitForSeconds(0.2f);
            m_Rigidbody2D.velocity = new Vector2(0, 0);
            m_Rigidbody2D.AddForce(new Vector2(0f, 5000));
            yield return new WaitForSeconds(0.5f);
            canFlap = true;
        }
    }
}