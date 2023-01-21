using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineSwitcher : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SwitchStateDown()
    {
        animator.Play("LookDownCamera");
    }

    public void SwitchStateDefault()
    {
        animator.Play("DefaultCamera");
    }

    public void SwitchStateUp()
    {
        animator.Play("LookUpCamera");
    }
}
