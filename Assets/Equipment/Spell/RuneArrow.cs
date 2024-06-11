using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneArrow : SpellFunction
{
    public override void EmpoweredFire(float ratio)
    {
        base.EmpoweredFire(ratio);

        Vector2 start = player.Position + Define.PM.Player.FrontVector * 36;

        //Define.OBJ.BulletManager.Generate("", start, Define.PM.Player.FrontVector, 1.0f+ratio*1.2f, 16, 16);
        //Projectile projectile= Define.OBJ.ProjectileManager.Fire("RuneArrow", start, Define.PM.Player.FrontVector * (400+350* ratio) + Vector2.up * 20);
        //projectile.SetCondition(160-60*ratio,0);

    }
}
