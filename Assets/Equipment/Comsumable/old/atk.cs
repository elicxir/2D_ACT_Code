using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atk : ConsumableFunction
{
    public override bool Require()
    {
        return true;
    }

    public override void Active()
    {
        //Define.PM.Player.buffManager.ADD("ATK", BuffType.ATK, 0.25f, 20*240);
    }

}