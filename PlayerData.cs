using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //Levels
    public int currentExp;
    public int HealthLvl;
    public int StrengthLvl;
    public int DexterityLvl;
    public int DefenceLvl;
    public int ManaLvl;
    public int AgilityLvl;

    public float currentHealth;

    //Scenes that will be loaded
    public List<string> currentlyOpenScenes = new List<string>();

    //Death spirit orb location and amount
    public float[] spiritOrbPosition = new float[2];
    public int deathSpiritOrbs;

    //Warp locations unlocked
    public List<bool> unlockWarps;

    //Abilities
    public bool doubleJump;

    //Shortcuts
    public bool shortcut1;

    //Bosses
    public bool boss1;

    //Respawn Coordinates
    public float[] respawnPoint = new float[2];

    //Loading Coordinates
    public float[] playerPosition = new float[2];

    public PlayerData(PlayerStats playerstats)
    {
        //Levels
        currentExp = PlayerStats.currentExp;
        HealthLvl = PlayerStats.HealthLvl;
        StrengthLvl = PlayerStats.StrengthLvl;
        DexterityLvl = PlayerStats.DexterityLvl;
        DefenceLvl = PlayerStats.DefenceLvl;
        ManaLvl = PlayerStats.ManaLvl;
        AgilityLvl = PlayerStats.AgilityLvl;

        currentHealth = HealthManager.currentHealth;

        //Scenes that will be loaded
        currentlyOpenScenes = PlayerStats.currentlyOpenScenes;

        //Death spirit orb location and amount
        spiritOrbPosition[0] = PlayerStats.spiritOrbPosition.x;
        spiritOrbPosition[1] = PlayerStats.spiritOrbPosition.y;

        deathSpiritOrbs = PlayerStats.deathSpiritOrbs;
        
        //Unlocked Warps
        unlockWarps = playerstats.unlockWarps;

        //Abilities
        doubleJump = PlayerStats.doubleJump;

        //Unlocked Shortcuts
        shortcut1 = PlayerStats.shortcut1;

        //Bosses Killed
        boss1 = PlayerStats.boss1;

        //Respawn Coordinates
        respawnPoint[0] = PlayerStats.respawnPoint.x;
        respawnPoint[1] = PlayerStats.respawnPoint.y;

        //Loading Coordinates
        playerPosition[0] = playerstats.playerPosition.x;
        playerPosition[1] = playerstats.playerPosition.y;
    }
}