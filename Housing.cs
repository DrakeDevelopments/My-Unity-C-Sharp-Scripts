using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Housing : MonoBehaviour, IInteractable
{

    //This script gets attached to house doors
    public bool shopIsOpen = false;
    public bool canUseDoor;

    private Animator animator;

    public GameObject tileMapToActivate;
    public GameObject shopExterior;
    public GameObject shopInterior;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (shopIsOpen == true)
        {
            animator.SetBool("Open", false);
            Debug.Log("close shop");
            tileMapToActivate.SetActive(false);
            shopIsOpen = false;
            shopExterior.SetActive(true);
            shopInterior.SetActive(false);
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        }

        else if (shopIsOpen == false)
        {
            animator.SetBool("Open", true);
            Debug.Log("open shop");
            tileMapToActivate.SetActive(true);
            shopIsOpen = true;
            shopExterior.SetActive(false);
            shopInterior.SetActive(true);
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PauseMenu player = collision.GetComponent<PauseMenu>();
        if (player != null)
        {
            canUseDoor = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        PauseMenu player = collision.GetComponent<PauseMenu>();
        if (player != null)
        {
            canUseDoor = false;
        }
    }
}
