using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : Projectile
{

    protected override void OnHitGround(Collider2D collision)
    {
        this.transform.parent = collision.gameObject.transform;
        BaseVelocity = Vector2.zero;
        foreach (AttackColider ac in attackColider)
        {
            ac.setAC_Active(false);
        }
    }
}
