using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
public class EC_Save : EventCommand
{

    public string SavePointName;

    public override IEnumerator Command()
    {
        GM.Game.PlayData.FillConsumable();
        GM.Game.PlayData.StartPoint = GM.Player.Player.Position;
        GM.Player.Player.Onsave();
        GM.Game.PlayDataSave(SavePointName);

        yield break;
    }

}
