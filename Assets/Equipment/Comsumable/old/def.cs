﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class def : ConsumableFunction
{
    public override bool Require()
    {
        return true;
    }

    public override void Active()
    {
        //Define.PM.Player.buffManager.ADD("DEF", BuffType.DEF, 0.25f,20*240);
    }

}