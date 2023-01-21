using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoadingManager : MonoBehaviour
//This script is attached to each of the Level Triggers
{
    public bool isLoaded;
    public bool shouldLoad;
    public LayerMask playerMask;
    public Vector2 tr = new Vector2(5f, 17.5f);
    public Vector2 tl = new Vector2(5f, 17.5f);
    public Vector2 br = new Vector2(5f, 17.5f);
    public Vector2 bl = new Vector2(5f, 17.5f);


    private void Start()
    {
        if (SceneManager.sceneCount > 0)
        {
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == gameObject.name)
                {
                    isLoaded = true;
                }
            }
        }
    }

    private void Update()
    {
        if (PlayerStats.loadCompleted)
        {
            TriggerCheck();
        }
    }

    private void TriggerCheck()
    {
        if (shouldLoad)
        {
            LoadScene();
        }

        else
        {
            UnloadScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Collector" & PlayerStats.loadCompleted)
        {
            shouldLoad = true;
        }
    }


        private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Collector" & PlayerStats.loadCompleted)
        {
            shouldLoad = false;      
        }
    }

    void LoadScene()
    {
        if(!isLoaded)
        {
            SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            isLoaded = true;
            //Debug.Log(gameObject.name + "Level Loading manager");
            PlayerStats.currentlyOpenScenes.Add(gameObject.name);
        }
    }

    void UnloadScene()
    {
        if (isLoaded == true)
        {
            SceneManager.UnloadSceneAsync(gameObject.name);
            isLoaded = false;
            PlayerStats.currentlyOpenScenes.Remove(gameObject.name);
        }
    }
}
