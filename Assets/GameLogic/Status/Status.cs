using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : SubPanelExecuter
{
    [SerializeField] MainStatus mainStatus;



    public override void Updater()
    {
        if (Define.IM.ButtonDown(Control.Menu) || Define.IM.ButtonDown(Control.Cancel))
        {
            GAME.StateQueue((int)gamestate.Menu);
        }
    }

    public override IEnumerator Init(gamestate before)
    {
        DataRefresh();
        return base.Init(before);
    }

    void DataRefresh()
    {
        EntityStats stats = Define.PM.Player.entityStats;
        mainStatus.StatusSet(stats.HP, stats.MaxHP, stats.MP, stats.MaxMP, stats.MPreg, 0);
    }
}
