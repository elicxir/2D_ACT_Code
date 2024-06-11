using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heal1 : ConsumableFunction
{
    public override bool Require()
    {
        return (stats.MaxHP > stats.HP && !Define.PM.Player.buffManager.CHECK("HPheal1"));
    }

    public override void Active()
    {
        //Define.PM.Player.buffManager.ADD("HPheal1", BuffType.HPreg, stats.MaxHP * 0.25f, 240);

    }

}