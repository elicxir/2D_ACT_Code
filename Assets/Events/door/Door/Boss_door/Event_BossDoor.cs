using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
public class Event_BossDoor : Event
{
    [SerializeField] Event_BossBattle bossBattle;
    [SerializeField] Boss_Door boss_door;

    public override bool EventFlag()
    {
        return !GM.Game.PlayData.GetGameFlag(bossBattle.BossID).isTrue;
        ;
    }




}
