using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFunction : MonoBehaviour
{
    protected Player player
    {
        get
        {
            return Define.PM.Player;

        }
    }

    public ItemDataSheet ItemDataSheet;

    [SerializeField] [Range(0, 9)] int index = 0;

    void OnValidate()
    {
        itemname = ItemDataSheet.sheets[0].list[index].name;
        description1 = ItemDataSheet.sheets[0].list[index].desc1;
        flavor1 = ItemDataSheet.sheets[0].list[index].flavor1;
        flavor2 = ItemDataSheet.sheets[0].list[index].flavor2;
        flavor3 = ItemDataSheet.sheets[0].list[index].flavor3;
        flavor4 = ItemDataSheet.sheets[0].list[index].flavor4;
        flavor5 = ItemDataSheet.sheets[0].list[index].flavor5;
        duration = ItemDataSheet.sheets[0].list[index].count;

    }


    public string itemname;
    public Sprite Sprite;
    public int duration;
    public string description1;

    public string flavor1;
    public string flavor2;
    public string flavor3;
    public string flavor4;
    public string flavor5;

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
