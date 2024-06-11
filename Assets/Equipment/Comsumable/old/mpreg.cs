using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mpreg : ConsumableFunction
{
    public override bool Require()
    {
        return true;
    }

    public override void Active()
    {
        //Define.PM.Player.buffManager.ADD("MPreg", BuffType.MPreg, 40,20*240);

    }

}