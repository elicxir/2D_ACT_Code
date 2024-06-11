using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConsts;

public class Gimmick : OwnTimeMonoBehaviour
{
    public virtual int UpdateRange
    {
        get
        {
            return 0;
        }
    }

    public Vector2 Pos
    {
        get
        {
            return transform.position;
        }
    }


    public Vector2Int MapGrid
    {
        get
        {
            int mapx = (int)transform.position.x / Game.width + 1;
            int mapy = (int)transform.position.y / Game.height + 1;

            return new Vector2Int(mapx, mapy);
        }
    }

    /// <summary>
    /// ギミックが動作する条件(出現条件ではない)
    /// </summary>
    /// <returns></returns>
    public virtual bool Condition()
    {
        return true;
    }


    public virtual void Init()
    {

    }

    public virtual bool Updater()
    {

        return false;
    }




}

public class GimmickTimer
{

    public float roopTime;
    public float Timer;

}
