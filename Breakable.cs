using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour, IDamageable
{
    public GameObject SpiritOrbPrefab;
    public GameObject parent;

    public int hitToBreak;
    public bool dropsExp;
    public int expDropped;

    private Vector3 dropTransform;

    public void Damage(int damage)
    {
        hitToBreak --;

        if (hitToBreak <= 0)
        {
            dropTransform = this.gameObject.transform.position;
            ObjectDestroyed();
        }
    }

    private void ObjectDestroyed()
    {
        if (dropsExp == true)
        {
            SpiritOrbPrefab.GetComponent<ItemCollect>().respawnOrbs = false;
            SpiritOrbPrefab.GetComponent<ItemCollect>().spiritOrbs = expDropped;
            Instantiate(SpiritOrbPrefab, dropTransform,  new Quaternion());
        }
            
        Destroy(parent);
    }
}
