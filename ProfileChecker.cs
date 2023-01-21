using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class ProfileChecker : MonoBehaviour
{
    private PlayerStats playerstats;

    private void Start()
    {
        playerstats = FindObjectOfType<PlayerStats>();
    }
    public void CheckProfile1()
    {
        PlayerStats.fileName = "/profile1.data";
        FindObjectOfType<PlayerStats>();

        if (File.Exists(Application.persistentDataPath + "/profile1.data"))
        {
            playerstats.LoadPlayer();
        }

        else
        {
            playerstats.NewGameCutscene();
        }
    }

    public void CheckProfile2()
    {
        PlayerStats.fileName = "/profile2.data";
        FindObjectOfType<PlayerStats>();

        if (File.Exists(Application.persistentDataPath + "/profile2.data"))
        {
            playerstats.LoadPlayer();
        }

        else
        {
            playerstats.NewGameCutscene();
        }
    }

    public void CheckProfile3()
    {
        PlayerStats.fileName = "/profile3.data";
        FindObjectOfType<PlayerStats>();

        if (File.Exists(Application.persistentDataPath + "/profile3.data"))
        {
            playerstats.LoadPlayer();
        }

        else
        {
            playerstats.NewGameCutscene();
        }
    }
}
