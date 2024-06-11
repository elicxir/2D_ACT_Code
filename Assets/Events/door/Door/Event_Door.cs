using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Door : Event
{
    enum Mode
    {
        OpenOnly
    }
    [SerializeField] Mode mode = Mode.OpenOnly;

    [SerializeField] Door door;

    public override bool EventFlag()
    {
        switch (mode)
        {
            case Mode.OpenOnly:
                return !door.isOpened;
            default:
                return false;
        }
    }
}
