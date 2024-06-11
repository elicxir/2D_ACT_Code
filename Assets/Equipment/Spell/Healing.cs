using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : SpellFunction
{
    public override void OnSwitchOn()
    {
        base.OnSwitchOn();

        //Define.PM.Player.buffManager.ADD("Healing", BuffType.HPreg, stats.MaxHP * 0.02f, 5);


    }
}
