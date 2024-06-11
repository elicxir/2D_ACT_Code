using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

[CreateAssetMenu(menuName = "MyScriptable/SpriteObjectData/Create SOD_FlamingParts")]
public class SOD_FlamingParts : SpriteObjectData
{
    public override Sprite UpdateSprite(float time, SpriteObject so)
    {
        so.spriteRenderer.color = color;
        return so.spriteRenderer.sprite;
    }

    int getRadius(SpriteObject so)
    {
        Vector3 size= so.spriteRenderer.bounds.size;
        return Mathf.RoundToInt(Mathf.Min(size.x,size.y));
    }

    public override void SpecificMove(SpriteObject so)
    {
        while (so.index*(0.84f/ getRadius(so)) <so.OwnTimeSiceStart)
        {
            SOM.GenerateByData(SOM.basicSODs[(int)SpriteObjectManager.DataIndex.BurnFire], so.Position + Random.insideUnitCircle * getRadius(so));

            so.index++;
        }
        

    }

}
