using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TrapDoorPlatform : MonoBehaviour
{
    private bool SolidPlatform = true;
    public GameObject TrapDoorChild;
    public float TimeBeforeFall = 1.00f;
    public float TimeBeforeSolid = 1.00f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        CharacterController2D controller = collision.GetComponent<CharacterController2D>();

        if (controller != null & SolidPlatform == true)
        {
            StartCoroutine(Flip());
            //Debug.Log("triggered");
        }
    }
    IEnumerator Flip()
    {
        SolidPlatform = false;
        yield return new WaitForSeconds(TimeBeforeFall);
        TrapDoorChild.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(TimeBeforeSolid);
        SolidPlatform = true;
        TrapDoorChild.GetComponent<BoxCollider2D>().enabled = true;

    }
}
