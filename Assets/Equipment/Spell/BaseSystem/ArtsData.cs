using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Equipment/ArtsData")]

public class ArtsData : ScriptableObject
{
    //技の名前
    public string artsname;

    //技の画像
    public Sprite Sprite;

    public float castspeed = 1;

    //技の簡易説明
    [TextArea] public string desc;

    //技のフレーバーテキスト
    [TextArea] public string flavor;
}
