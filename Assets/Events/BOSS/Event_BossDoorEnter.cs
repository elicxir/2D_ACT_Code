using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_BossDoorEnter : Event
{
    [SerializeField] Boss_Door boss_door;

    public override bool EventFlag()
    {
        return !boss_door.isOpened;

    }
}
