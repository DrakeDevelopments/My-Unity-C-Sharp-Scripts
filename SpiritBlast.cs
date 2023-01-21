using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritBlast : MonoBehaviour
{
    public Transform firePoint;                                                 //Lets you drag the firepoint object into the inspector. Is used to take the transform coordinates.
    public GameObject bulletPrefab;                                             //Lets you drag the prefabs into the inspector. Is used instantiated in the Shoot function.

    // Update is called once per frame

    public void Shoot()
    {
        //shooting logic
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);      //Bullet is created.
    }

}
