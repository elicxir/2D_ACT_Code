using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class Event_StatusUP : Event
{
    public StatusType statusType;

    public int StatusUP_ID;

    public EC_StatusUP statusUP;

    private void OnValidate()
    {
        statusUP.statusType = statusType;

        statusUP.ID = StatusUP_ID;
    }

    public void SetID(int i)
    {
        StatusUP_ID = i;

        statusUP.statusType = statusType;

        statusUP.ID = StatusUP_ID;
    }

    public sealed override bool EventFlag()
    {
        bool condition= GM.Game.PlayData.FindStatusUpFlag(StatusUP_ID);
        return !condition;
    }

    float t = 0;

    public sealed override void Updater()
    {
        t += OwnDeltaTime;
        UpdateFunction(t);
        base.Updater();
    }

    public virtual void UpdateFunction(float t)
    {

    }
}
