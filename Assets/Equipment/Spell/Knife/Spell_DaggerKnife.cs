using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class Spell_DaggerKnife : SpellFunction
{
    [SerializeField] Projectile projectile;
    private void OnValidate()
    {
        //prefabindex = projectileManager.GetIndex(prefabname);

    }

    public override void Fire(Vector2 pos,Vector2 dir)
    {
        projectileManager.Fire(projectile, pos, dir, 400, player);
    }
}
