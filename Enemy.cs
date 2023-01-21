using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
//using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable

    //This script is attached to the MasterMonster Prefab

{
    public EnemyCreator enemyCreator;
    public Transform playerTransform;

    public GameObject SpiritOrbPrefab;
    public GameObject parentObject;
    public GameObject child;

    private GameObject player;

    private bool enemyIdle = true;
    public bool canFlap = true;
    private bool facingLeft = false;
    private bool facingRight = true;

    //used to delay time between attacks
    private bool canAttack = true;

    //used to stop duplicate spirit orbs
    private bool iAmDead = false;

    private float EnemyCurrentHealth;

    private Animator animator;

    //Start of variables contained in EnemyCreator scriptable object

    private float attack;
    private float defence;
    private float maxHealth;
    private float attackRange = 0.1f;       //how many units away the enemy can attack from
    private float attackFrequency;          //how often damage is dealt
    private float speed;                    //Speed of enemy
    private float m_MovementSmoothing;      //How much to smooth out the movement
    private float stoppingDistance;         //How close the any gets before stopping

    private bool hasWings;
    private bool patrols;
    private bool attackLinkedToAnimation;

    public int spiritOrbsDrop;          //How many spririt orbs get dropped


    //End of variables contained in EnemyCreator scriptable object

    private Transform enemyTransform;
    Vector2 enemyPosition;
    Vector2 playerPosition;

    private Rigidbody2D m_Rigidbody2D;
    private int chaseDirection;

    private Vector3 m_Velocity = Vector3.zero;

    //Used for attack:
    public Transform MelleeAttackPoint;
    public LayerMask PlayerLayer;

    private void Start() //change this to Start when we build
    {
        //Start of variables contained in EnemyCreator scriptable object
        animator = gameObject.GetComponent<Animator>();
        parentObject = GameObject.Find(this.name + " spawn point");
        gameObject.transform.SetParent(parentObject.transform);
        attack = enemyCreator.attack;
        defence = enemyCreator.defence;
        maxHealth = enemyCreator.maxHealth;
        attackRange = enemyCreator.attackRange;
        attackFrequency = enemyCreator.attackFrequency;
        speed = enemyCreator.speed;
        m_MovementSmoothing = enemyCreator.m_MovementSmoothing;
        stoppingDistance = enemyCreator.stoppingDistance;
        hasWings = enemyCreator.hasWings;
        spiritOrbsDrop = enemyCreator.spiritOrbsDrop;
        patrols = enemyCreator.patrols;
        attackLinkedToAnimation = enemyCreator.attackLinkedToAnimation;

        //this.GetComponent<SpriteRenderer>().sprite= enemyCreator.monsterImage;
        //this.GetComponent<CapsuleCollider2D>().size = enemyCreator.triggerCapsuleSize;
        //child.GetComponent<CapsuleCollider2D>().size = enemyCreator.triggerCapsuleSize;

        //End of variables contained in EnemyCreator scriptable object


        EnemyCurrentHealth = maxHealth;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if(patrols == false)
        {
            Move();
            FaceDirection();
        }

        else
        {
            PatrolMovement();
        }

        animator.SetFloat("Speed", Mathf.Abs(m_Rigidbody2D.velocity.x));

        enemyTransform = transform;
        enemyPosition = transform.position;
        playerPosition = player.transform.position;

        if (Vector2.Distance(playerPosition, enemyPosition) > 10 | Vector2.Distance(playerPosition, enemyPosition) < stoppingDistance)
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

        if (playerPosition.x - enemyPosition.x >= -0.2 & playerPosition.x - enemyPosition.x <= 0.2)
        {
            chaseDirection = 0;
        }

        if(Vector2.Distance(playerPosition, enemyPosition) <= attackRange & canAttack == true)
        {
            StartCoroutine(ActivateAttack());
        }
    }


    //non patrol movement behaviour
    private void Move()
    {
        if (enemyIdle == false)
        {
            Vector3 targetVelocity = new Vector2(chaseDirection * speed, m_Rigidbody2D.velocity.y);
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }

        else
        {
            Vector3 targetVelocity = new Vector2(0, m_Rigidbody2D.velocity.y);
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }
    }

    //If patrol bool is true this behaviour will cause the enemy to patrol between 2 colliders attached to objects.
    private void PatrolMovement()
    {
        if (facingLeft == true)
        {
            Vector3 targetVelocity = new Vector2(speed*-1, m_Rigidbody2D.velocity.y);
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }

        else
        {
            Vector3 targetVelocity = new Vector2(speed, m_Rigidbody2D.velocity.y);
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }
    }

    public void PatrolChangeDirection()
    {
        if(patrols == true)
        {
            facingLeft = !facingLeft;
            Flip();
        }
    }

    private void FaceDirection()
    {
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
    }

    private IEnumerator ActivateAttack()
    {
        if (attackLinkedToAnimation)
        {
            canAttack = false;
            animator.SetBool("MAttack", true);
            yield return new WaitForSeconds(attackFrequency);
            canAttack = true;
        }

        else
        {
            EnemyMelleeAttack();
            canAttack = false;
            yield return new WaitForSeconds(attackFrequency);
            canAttack = true;
        }
    }

    public void Damage(int damage)

    {
        EnemyCurrentHealth -= damage * ((200 - defence)/200);

        //animator.SetTrigger("Staggered");
        // m_Rigidbody2D.AddForce(new Vector2(0f, 200000));


        if (EnemyCurrentHealth <= 0 & iAmDead ==false)
        {
            iAmDead = true;
            GameObject.Find(gameObject.name + (" spawn point")).GetComponent<EnemyRespawn>().enemyIsDead = true; //Updates respawn script
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
       //Dropping exp item
        SpiritOrbPrefab.GetComponent<ItemCollect>().respawnOrbs = false;
        SpiritOrbPrefab.GetComponent<ItemCollect>().spiritOrbs = spiritOrbsDrop;
        Instantiate(SpiritOrbPrefab, enemyPosition+new Vector2(0,0.5f), enemyTransform.rotation, enemyTransform.parent);

        //Removing components and destroying enemy
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        Destroy(gameObject);
        this.enabled = false;
        yield return null;
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

    //Attack

    //Draws the circle in which player will be detected upon attack
    void OnDrawGizmosSelected()                                        
    {
        if (MelleeAttackPoint == null)
            return;

        Gizmos.DrawWireSphere(MelleeAttackPoint.position, attackRange);
    }

    //Attacks all players within the circle
    void EnemyMelleeAttack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(MelleeAttackPoint.position, attackRange, PlayerLayer);  

        foreach (Collider2D enemy in hitPlayer)
        {
            if (enemy.tag == "Player")
            {
                HealthManager.TakeDamage(attack);
            }
        }
    }
}