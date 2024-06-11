using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Rapier : SpellFunction
{
    public int index;

    public AC_DataSet dataset;

    public Projectile projectile;

    public override void Fire(Vector2 pos,Vector2 dir)
    {
        if (projectile != null)
        {
            projectileManager.Fire(projectile, pos, dir, 320, player);
        }
    }
}
