using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneGameLoader : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerStats playerstats;

    private void OnEnable()
    {
        playerstats = FindObjectOfType<PlayerStats>();
        playerstats.NewGame();
        SceneManager.UnloadSceneAsync("OpeningCutscene");
    }
}
