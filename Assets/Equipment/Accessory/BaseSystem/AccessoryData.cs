using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Equipment/AccessoryData")]

public class AccessoryData : ScriptableObject
{
    //アイテムの名前
    public string consumablename;
    //アイテムの画像
    public Sprite Sprite;
   
    //アイテムの簡易説明
    [TextArea] public string desc;
    //アイテムのフレーバーテキスト
    [TextArea] public string flavor;
}
