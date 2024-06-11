using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class EC_Branch_Item : Branch
{
    public ItemType type;
    public int num;

    public string ItemName;
    private void OnValidate()
    {
        EquipmentDataManager EDM = FindObjectOfType<EquipmentDataManager>();
      
        if (EDM != null)
        {
            switch (type)
            {
                case ItemType.Consumable:
                    ItemName = EDM.GET_CONSUMABLE_FUNCTION(num).consumablename;
                    break;

                case ItemType.Accessory:
                    ItemName = EDM.GET_ACCESSORY_FUNCTION(num).accessoryname;
                    break;

                case ItemType.Spell:
                    ItemName = EDM.GET_SPELL_FUNCTION(num).spellname;
                    break;

                case ItemType.KeyItem:
                    ItemName = EDM.GET_KEYITEM_DATA(num).Itemname;
                    break;

                default:
                    break;
            }
        }
    }


    protected override bool Condition()
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

        return condition;
    }
}
