using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "MyScriptable/SpriteObjectData/Create SOD_Gather")]

//target‚É‹ß‚Ã‚¢‚Ä‚¢‚­
public class SOD_Gather : SpriteObjectData
{
    public int radius=8;
    public float accel = 42;

    public override void SpecificMove(SpriteObject so)
    {
        base.SpecificMove(so);

        if (so.target != null)
        {
            so.BaseVelocity = accel * ((Vector2)so.target.position - so.Position).normalized;
        }

        if(((Vector2)so.target.position - so.Position).sqrMagnitude < radius * radius)
        {
            so.Deactivate();
        }
    }

}
