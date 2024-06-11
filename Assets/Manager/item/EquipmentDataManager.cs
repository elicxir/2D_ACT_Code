using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

//revised

public class EquipmentDataManager : MonoBehaviour
{
    [SerializeField] GameManager manager;

    void OnValidate()
    { DataInit(); }

    [SerializeField] Sprite NoEquip;

    [SerializeField] GameObject Items;
    [SerializeField] GameObject Spells;
    [SerializeField] GameObject Accessories;

    [SerializeField] ConsumableFunction[] ConsumableFunction;
    [SerializeField] SpellFunction[] SpellFunction;
    [SerializeField] AccessoryFunction[] AccessoryFunction;

    [SerializeField] ConsumableFunction ConsumableFunction_null;
    [SerializeField] SpellFunction SpellFunction_null;
    [SerializeField] AccessoryFunction AccessoryFunction_null;

    public ConsumableDataSheet ConsumableDataSheet;
    public SpellDataSheet SpellDataSheet;
    public AccessoryDataSheet AccessoryDataSheet;


    [SerializeField] KeyItemsData[] KeyItemData;

    void Start()
    {
        DataInit();
    }

    void DataInit()
    {
        ConsumableFunction = Items.GetComponentsInChildren<ConsumableFunction>();
        SpellFunction = Spells.GetComponentsInChildren<SpellFunction>();
        AccessoryFunction = Accessories.GetComponentsInChildren<AccessoryFunction>();
    }

    public KeyItemsData GET_KEYITEM_DATA(int index)
    {
        KeyItemData[index].LanguageIndex = (int)manager.Language;
        return KeyItemData[index];
    }

    public ConsumableFunction GET_CONSUMABLE_FUNCTION(int index)
    {
        if (index == -1)
        {
            return ConsumableFunction_null;
        }
        return ConsumableFunction[Mathf.Clamp(index, 0, ConsumableFunction.Length - 1)];
    }

    public SpellFunction GET_SPELL_FUNCTION(int index)
    {
        if (index == -1)
        {
            return SpellFunction_null;
        }
        return SpellFunction[Mathf.Clamp(index, 0, SpellFunction.Length - 1)];
    }

    public AccessoryFunction GET_ACCESSORY_FUNCTION(int index)
    {
        if (index == -1)
        {
            return AccessoryFunction_null;
        }
        return AccessoryFunction[Mathf.Clamp(index, 0, AccessoryFunction.Length - 1)];

    }





    public bool ValidID_Consumable(int index)
    {
        return (index >= 0 && index < ConsumableFunction.Length);
    }

    public bool ValidID_Spell(int index)
    {
        return (index >= 0 && index < SpellFunction.Length);
    }
    public bool ValidID_Accessory(int index)
    {
        return (index >= 0 && index < AccessoryFunction.Length);
    }
}

[System.Serializable]
public class KeyItemData
{
    public string Itemname;
    public Sprite Sprite;
}

public enum SpellType
{

    Trigger,//単純なショット
    Switch,//オンオフの切り替え
    Increase,//いわゆるためうち
    Undefined,
}

public enum CastType
{
    Rapier,//レイピアで刺すモーション
    Cast1,//投げるモーション
}



public enum ItemType
{
    Consumable, Accessory, Spell, KeyItem
}