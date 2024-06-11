using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainFire : Projectile
{
    [SerializeField] Sprite[] sprite;

    protected override void OnFireInit()
    {
        base.OnFireInit();      
    }

    public const float time = 0.7f;

    protected override void UpdateFunction()
    {
        spriteRenderer.sprite = sprite[Mathf.Clamp(Mathf.FloorToInt(sprite.Length * OwnTimeSiceStart / time), 0, sprite.Length - 1)];

        if (OwnTimeSiceStart > time)
        {
            Deactivate();
        }
    }

}
