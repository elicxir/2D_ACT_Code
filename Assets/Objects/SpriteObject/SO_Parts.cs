using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SO_Parts : SpriteObject
{
    Vector2Int StartGrid;

    public void UniqueInit(Sprite sprite,Vector2 Pos,Vector2 velocity,bool isRight)
    {
        spriteRenderer.sprite = sprite;
        Position = Pos;

        BaseVelocity = velocity;
        if (!isRight)
        {
            spriteRenderer.flipX = true;
        }
        StartGrid = NowMapGrid;
    }

    protected override void SO_Function()
    {
        if ((NowMapGrid - StartGrid).sqrMagnitude > 3)
        {
            Deactivate();
        }
    }


}
