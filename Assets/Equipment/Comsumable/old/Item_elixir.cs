using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_elixir : ConsumableFunction
{
    public override bool Require()
    {
        return (Define.PM.Player.buffManager.CHECK("Elixir_HP"));
    }

    public override void Active()
    {
        //Define.PM.Player.buffManager.ADD("Elixir_MP", BuffType.MPreg, stats.MaxMP * 0.7f, 720);
        //Define.PM.Player.buffManager.ADD("Elixir_HP", BuffType.HPreg, stats.MaxHP * 0.7f, 720);

    }
}
