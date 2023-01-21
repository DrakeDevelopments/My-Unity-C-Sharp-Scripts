using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyCreator : ScriptableObject     
{
    public float attack =1;
    public float defence;
    public float maxHealth;
    public float attackRange;           //How many units away the enemy can attack from
    public float attackFrequency;       //How often damage is dealt
    public float speed;                 //Speed of enemy
    public float m_MovementSmoothing;   //How much to smooth out the movement
    public float stoppingDistance;      //How close the any gets before stopping

    public int spiritOrbsDrop;          //How many spririt orbs get dropped

    public bool hasWings;
    public bool patrols;
    public bool attackLinkedToAnimation;
    //public GameObject enemyPrefabRigged;

    //public Sprite monsterImage;
    //public Vector2 triggerCapsuleSize;
}
