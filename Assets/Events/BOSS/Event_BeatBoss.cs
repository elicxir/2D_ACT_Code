using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class Event_BeatBoss : Event
{
    [SerializeField] Event_BossBattle battle;

    public override bool EventFlag()
    {
        return !GM.Game.PlayData.GetGameFlag(battle.BossID).isTrue;
    }

    [SerializeField] EC_GameFlag Flags;

    private void OnValidate()
    {
        Flags.data.FlagID = battle.BossID;
    }

    public override bool AutoeventFlag
    {
        get
        {
            bool c1=GM.Game.PlayData.GetGameFlag(battle.BossID).progress == 1;
            return GM.Enemy.EnemyNumInSection == 0&& c1;
        }
    }


}
