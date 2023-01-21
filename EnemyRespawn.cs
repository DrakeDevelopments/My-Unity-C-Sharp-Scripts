using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public bool enemyIsDead;
    public bool spawnOnStart;

    public float timeSinceDeath;
    public float timeUntilRespawn;

    private GameObject LastEnemy;
    public GameObject enemyPreFab; 

    public string EnemyName;
    public EnemyCreator monster;

    void Start () 
    {
        this.gameObject.name = EnemyName + " spawn point";

        if(spawnOnStart == true)
        {
            timeSinceDeath = timeUntilRespawn;
        }
    }

    void Update () 
    {
        if(enemyIsDead == true) 
        {
            timeSinceDeath += Time.deltaTime;               //Timer starts when monster is killed.
        }

        if(timeSinceDeath >= timeUntilRespawn)              //If the timer is bigger than cooldown.
        {
            enemyPreFab.GetComponent<Enemy>().enemyCreator = monster;
            enemyPreFab.transform.position = transform.position;
            Instantiate(enemyPreFab);
            
            LastEnemy = GameObject.Find(enemyPreFab.name + "(Clone)");
            LastEnemy.name = EnemyName;

            enemyIsDead = false;

            timeSinceDeath = 0;
        }
    }
}