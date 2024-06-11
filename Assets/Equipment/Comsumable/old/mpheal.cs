using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mpheal : ConsumableFunction
{
    

    public override bool Require()
    {
        return (stats.MaxMP > stats.MP&& !Define.PM.Player.buffManager.CHECK("MPheal"));
    }

    public override void Active()
    {
        //Define.PM.Player.buffManager.ADD("MPheal", BuffType.MPreg, stats.MaxMP * 0.35f,  240);
    }

}