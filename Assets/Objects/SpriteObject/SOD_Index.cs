using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "MyScriptable/SpriteObjectData/Create SOD_Index")]

public class SOD_Index : SpriteObjectData
{
    public Sprite[] sprites;

    public override Sprite UpdateSprite(float time, SpriteObject so)
    {
        so.spriteRenderer.color = color;

        int index = Mathf.Clamp(so.index, 0, sprites.Length - 1);
        return sprites[index];
    }
}
