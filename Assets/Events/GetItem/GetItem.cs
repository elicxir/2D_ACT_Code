using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    EquipmentDataManager EDM;

    public ItemType type;
    [Range(0,31)]
    public int num;

    public string ItemName;
    public Sprite sprite;

    [SerializeField] EC_Branch_Item branch_Item;
    [SerializeField] EC_Obtain obtain;
    [SerializeField] EC_Add add;

    void SetData(ItemType type, int num)
    {
        if (branch_Item != null)
        {
            branch_Item.type = type;
            branch_Item.type = type;
            branch_Item.num = num;
        }

        if (obtain != null)
        {
            obtain.ItemName = ItemName;
            obtain.Sprite = sprite;
        }
        if (add != null)
        {
            add.num = num;
            add.type = type;
            add.ItemName = ItemName;
        }
    }  

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
                    sprite= EDM.GET_CONSUMABLE_FUNCTION(num).Sprite;
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

    }
 
}
