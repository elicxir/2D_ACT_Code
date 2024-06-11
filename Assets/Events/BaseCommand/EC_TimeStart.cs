using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class EC_TimeStart : EventCommand
{
    public override IEnumerator Command()
    {
        GM.Game.mainGame.enemyManager.GameEnter();
        GM.Game.mainGame.playerManager.GameEnter();
        GM.Game.mainGame.tileMapManager.GameEnter();
        GM.Game.mainGame.eventManager.GameEnter();

        yield break;
    }
}
