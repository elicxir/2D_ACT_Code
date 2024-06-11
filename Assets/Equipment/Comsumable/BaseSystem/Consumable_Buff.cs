using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable_Buff : ConsumableFunction
{
    [SerializeField] protected  AC_DataSet BuffData;

    public override void Active()
    {
        player.buffManager.ADD(BuffData.buff);
    }

}
