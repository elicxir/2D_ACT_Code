using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class antidote : ConsumableFunction
{

    public override void Active()
    {

        Define.PM.Player.buffManager.REMOVE("Poison");

        //Define.PM.Player.buffManager.ADD("Antidote", BuffType.Poison_res, 50 , 240*20);


    }

}