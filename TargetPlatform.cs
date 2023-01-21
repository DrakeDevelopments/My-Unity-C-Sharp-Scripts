using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlatform : MonoBehaviour, IDamageable
{
    public SpriteRenderer ChildSprite;
    public BoxCollider2D ChildCollider;
    public void Damage(int Damage)
    {
        ChildSprite.enabled = true;
        ChildCollider.enabled = true;
    }
}
