using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell0 : SpellFunction
{
    public override void Fire(Vector2 pos, Vector2 dir)
    {
        Vector2 rand = Random.insideUnitCircle.normalized * Random.Range(5, 9);

        Vector2 start = player.Position + Define.PM.Player.FrontVector * 15 + rand;
        //Projectile projectile = Define.OBJ.ProjectileManager.Fire("IZBullet", start, Define.PM.Player.FrontVector * (400));
    }

}
