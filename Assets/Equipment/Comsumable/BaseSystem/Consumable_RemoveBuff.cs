using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable_RemoveBuff : Consumable_Buff
{
    [SerializeField] string[] Remove;

    public override void Active()
    {
        foreach(string r in Remove)
        {
            player.buffManager.REMOVE(r);
        }

        player.buffManager.ADD(BuffData.buff);
    }
}
