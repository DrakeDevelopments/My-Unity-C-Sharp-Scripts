using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteractionUnlock : MonoBehaviour
{
    //This script is attached to each of the warp buttons and also the on click functions to warp

    //Purpose - Makes warp buttons interactable when resting at 'bonfire'.

    public Button myButton; //Drag intended button here
    public GameObject Player;

    public GameObject bonfire1;
    public GameObject bonfire2;
    public GameObject bonfire3;
    public GameObject bonfire4;
    public GameObject bonfire5;
    public GameObject bonfire6;
    public GameObject bonfire7;
    public GameObject bonfire8;

    public Vector2 bonfiretransform1;
    public Vector2 bonfiretransform2;
    public Vector2 bonfiretransform3;
    public Vector2 bonfiretransform4;
    public Vector2 bonfiretransform5;
    public Vector2 bonfiretransform6;
    public Vector2 bonfiretransform7;
    public Vector2 bonfiretransform8;

    private PlayerStats playerstats;
    private CinemachineWarping cinemachineWarping;

    private int warpNumber;

    private void Start()
    {
        playerstats = FindObjectOfType<PlayerStats>();
        cinemachineWarping = FindObjectOfType<CinemachineWarping>();
    }

    void Update()
    {
        int warpNumber = gameObject.name[5] - '0'; //This gets the number from the level button e.g. level5button = 5 to be used in if statement.

        if (playerstats.unlockWarps[warpNumber-1] == true)
        {
            myButton.interactable = true;
        }

        else
        {
            myButton.interactable = false;
        }
    }

    //Would be nice to simplify the code below in future so that it is one script rather than 8


    public void warp1()
    {
        bonfiretransform1 = bonfire1.transform.position;

        Player.transform.position = bonfiretransform1;

        playerstats.WarpPlayer();

    }

    public void warp2()
    {

        bonfiretransform2 = bonfire2.transform.position;

        Player.transform.position = bonfiretransform2;

        playerstats.WarpPlayer();
    }

    public void warp3()
    {

        bonfiretransform3 = bonfire3.transform.position;

        Player.transform.position = bonfiretransform3;

        playerstats.WarpPlayer();
    }

    public void warp4()
    {
        bonfiretransform4 = bonfire4.transform.position;

        Player.transform.position = bonfiretransform4;

        playerstats.WarpPlayer();
    }

    public void warp5()
    {
        bonfiretransform5 = bonfire5.transform.position;

        Player.transform.position = bonfiretransform5;

        playerstats.WarpPlayer();
    }

    public void warp6()
    {
        bonfiretransform6 = bonfire6.transform.position;

        Player.transform.position = bonfiretransform6;

        playerstats.WarpPlayer();
    }

    public void warp7()
    {
        bonfiretransform7 = bonfire7.transform.position;

        Player.transform.position = bonfiretransform7;

        playerstats.WarpPlayer();
    }

    public void warp8()
    {
        bonfiretransform8 = bonfire8.transform.position;

        Player.transform.position = bonfiretransform8;

        playerstats.WarpPlayer();
    }

}
