using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Equipment/ConsumableData")]
public class ConsumableData : ScriptableObject
{
    //アイテムの名前
    public string consumablename;
    //アイテムの画像
    public Sprite Sprite;
    //アイテムの使用回数
    public int MaxUseCount;
    //アイテムの簡易説明
    [TextArea]public string desc;
    //アイテムのフレーバーテキスト
    [TextArea]public string flavor;

}
