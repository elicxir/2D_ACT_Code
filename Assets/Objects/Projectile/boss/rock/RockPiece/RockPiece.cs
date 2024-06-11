using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPiece : Projectile
{
    [SerializeField] Sprite[] sprites;

    protected override void OnFireInit()
    {
        base.OnFireInit();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }

}
