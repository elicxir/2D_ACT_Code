using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpreg : ConsumableFunction
{
    public override bool Require()
    {
        return !player.buffManager.CHECK("HPreg");
    }

    public override void Active()
    {

        //player.buffManager.ADD("HPreg",BuffType.HPreg, stats.MaxHP  * 0.006f, 15*240);
    }

}