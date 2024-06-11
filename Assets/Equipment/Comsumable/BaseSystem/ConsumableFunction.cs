using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableFunction : SpecialFunction
{

    protected Player player
    {
        get
        {
            return Define.PM.Player;
        }
    }

    [SerializeField] ConsumableData data;

    public string consumablename { get { return data.consumablename; } }
    public Sprite Sprite { get { return data.Sprite; } }

    public int useCount = 8;

    public int MaxUseCount { get { return data.MaxUseCount; } }

    public string description { get { return data.desc; } }

    public string flavor { get { return data.flavor; } }


    public EntityStats stats
    {
        get
        {
            return Define.PM.Player.entityStats;
        }
    }

    //アイテム使用可かどうか
    public virtual bool Require()
    {
        return true;
    }

    //アイテム使用時の効果
    public virtual void Active()
    {

    }

}
