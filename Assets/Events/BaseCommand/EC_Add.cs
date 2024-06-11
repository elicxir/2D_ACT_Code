using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class EC_Add : EventCommand
{
    public ItemType type;
    public int num;

    public string ItemName;

    EquipmentDataManager EDM;

    private void OnValidate()
    {
        if (EDM == null)
        {
            EDM = FindObjectOfType<EquipmentDataManager>();
        }

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

    public override IEnumerator Command()
    {
        switch (type)
        {
            case ItemType.Consumable:
                if (GM.Game.PlayData.AddConsumable(num))
                {
                    GM.EDM.GET_CONSUMABLE_FUNCTION(num).useCount = GM.EDM.GET_CONSUMABLE_FUNCTION(num).MaxUseCount;
                    print("Obtain:" + ItemName);
                }
                else
                {
                    print("failed to add " + ItemName);
                }
                break;

            case ItemType.Accessory:
                if (GM.Game.PlayData.AddAccessory(num))
                {
                    print("Obtain:" + ItemName);
                }
                else
                {
                    print("failed to add " + ItemName);
                }
                break;

            case ItemType.Spell:
                if (GM.Game.PlayData.AddSpell(num))
                {
                    print("Obtain:" + ItemName);
                }
                else
                {
                    print("failed to add " + ItemName);
                }
                break;

            case ItemType.KeyItem:
                if (GM.Game.PlayData.AddKeyItem(num))
                {
                    print("Obtain:" + ItemName);
                }
                else
                {
                    print("failed to add " + ItemName);
                }
                break;

            default:
                break;
        }

        yield break;
       }
}
