using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
public class EC_TimeStop : EventCommand
{
    public override IEnumerator Command()
    {
        GM.Game.mainGame.enemyManager.GameExit();
        GM.Game.mainGame.playerManager.GameExit();
        GM.Game.mainGame.tileMapManager.GameExit();
        GM.Game.mainGame.eventManager.GameExit();

        yield break;
    }
}
