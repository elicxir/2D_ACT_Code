using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataTypes;

[CreateAssetMenu(menuName = "MyScriptable/Create KeyItemsData")]
public class KeyItemsData : ScriptableObject
{
    [SerializeField] KeyItemTextData[] Datas;

    public int LanguageIndex  = 0;
    public string Itemname
    {
        get
        {
            return Datas[Mathf.Clamp(LanguageIndex, 0, Datas.Length - 1)].Itemname;
        }
    }

    public string desc
    {
        get
        {
            return Datas[Mathf.Clamp(LanguageIndex, 0, Datas.Length - 1)].desc;

        }
    }

    public Sprite Sprite;

}

