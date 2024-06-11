using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class EC_GameFlag : EventCommand
{
    public GameFlag data;

    public Event @event;

    private void OnValidate()
    {
        if (@event != null)
        {
            data.FlagID = @event.UniqueEventID;
        }
    }

    public override IEnumerator Command()
    {
        GM.Game.PlayData.SetGameFlag(data);
        //print($"SetGameFlag:{data.FlagID}");
        yield break;
    }
}
