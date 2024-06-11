using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAxe : SpellFunction
{
    string prefabname="HandAxe";
    int prefabindex = 0;

    protected override void Start()
    {
        prefabindex = projectileManager.GetIndex(prefabname);
    }


    public override void Fire(Vector2 pos, Vector2 dir)
    {
       // Vector2 dir = Define.PM.Player.FrontVector * (80 + Mathf.Abs(Define.PM.Player.EntityVelocity.x)*0.5f) + Vector2.up * 360;

       // projectileManager.Fire(prefabindex, Define.PM.Player.AttackPoint, dir.normalized,dir.magnitude, Hostility.Enemy);
    }
}
