using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

/// <summary>
/// 取得したら反応しなくなる系統のアイテム取得に用いる
/// </summary>
public class Event_GetItem : Event
{
    [Header("取得するアイテムの設定")]
    public ItemType type;
    [Range(0, 31)]
    public int num;

    public string ItemName;
    public Sprite sprite;

    EquipmentDataManager EDM;

    private void OnValidate()
    {
        if (EDM == null)
        {
            EDM = FindObjectOfType<EquipmentDataManager>();
        }

        branch_Item = GetComponentInChildren<EC_Branch_Item>();
        obtain = GetComponentInChildren<EC_Obtain>();
        add = GetComponentInChildren<EC_Add>();
        if (EDM != null)
        {
            switch (type)
            {
                case ItemType.Consumable:
                    ItemName = EDM.GET_CONSUMABLE_FUNCTION(num).consumablename;
                    sprite = EDM.GET_CONSUMABLE_FUNCTION(num).Sprite;
                    break;

                case ItemType.Accessory:
                    ItemName = EDM.GET_ACCESSORY_FUNCTION(num).accessoryname;
                    sprite = EDM.GET_ACCESSORY_FUNCTION(num).Sprite;

                    break;

                case ItemType.Spell:
                    ItemName = EDM.GET_SPELL_FUNCTION(num).spellname;
                    sprite = EDM.GET_SPELL_FUNCTION(num).Sprite;

                    break;

                case ItemType.KeyItem:
                    ItemName = EDM.GET_KEYITEM_DATA(num).Itemname;
                    sprite = EDM.GET_KEYITEM_DATA(num).Sprite;

                    break;

                default:
                    break;
            }
        }

        SetData(type, num);

        talk_on_itemget.setScript(messages);

    }

    [SerializeField] EC_Talk talk_on_itemget;

    void SetData(ItemType type, int num)
    {
        branch_Item.type = type;
        branch_Item.type = type;
        branch_Item.num = num;
        branch_Item.ItemName= ItemName;

        obtain.ItemName = ItemName;
        obtain.Sprite = sprite;

        add.num = num;
        add.type = type;
        add.ItemName = ItemName;
    }

    [Header("取得時に表示されるメッセージ")]
    [SerializeField] TalkScriptData[] messages;


    [Header("各種参照")]

    [SerializeField] EC_Branch_Item branch_Item;
    [SerializeField] EC_Obtain obtain;
    [SerializeField] EC_Add add;

    public override bool EventFlag()
    {
        bool condition = false;

        switch (type)
        {
            case ItemType.Consumable:
                condition = GM.Game.PlayData.FindConsumableList(num);
                break;
            case ItemType.Accessory:
                condition = GM.Game.PlayData.FindAccessory(num);

                break;
            case ItemType.Spell:
                condition = GM.Game.PlayData.FindSpell(num);

                break;
            case ItemType.KeyItem:
                condition = GM.Game.PlayData.FindKeyItem(num);
                break;
            default:
                break;
        }

        return !condition;
    }
}
