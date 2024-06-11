using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class Managers_MainGame : MonoBehaviour
{
    /// <summary>
    /// ゲーム開始時のawakeで呼ばれるロード
    /// </summary>
    public virtual void OnGameStart()
    {

    }


    protected MapManager Map
    {
        get
        {
            return GM.Game.Map_Manager;
        }
    }


    public virtual void OnSectionChanged(Vector2Int newSction)
    {

    }

    public virtual void ManagerLoadInit(MainGame caller)
    {
        Vector2Int grid = GM.Player.Player.NowMapGrid;

    }

    public virtual void ManagerUpdater(MainGame caller)
    {

    }

    public virtual void ManagerLateUpdater(MainGame caller)
    {

    }


    public virtual void GameExit()
    {
        //時間停止処理
    }
    public virtual void GameEnter()
    {
        //時間進行速度を1にする
    }

}
