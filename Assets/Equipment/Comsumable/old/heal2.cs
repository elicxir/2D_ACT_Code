using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heal2 : ConsumableFunction
{
    public override bool Require()
    {
        return (stats.MaxHP > stats.HP && !Define.PM.Player.buffManager.CHECK("HPheal2"));
    }

    public override void Active()
    {
        //Define.PM.Player.buffManager.ADD("HPheal2", BuffType.HPreg, stats.MaxHP * 0.60f, 240);

    }
}