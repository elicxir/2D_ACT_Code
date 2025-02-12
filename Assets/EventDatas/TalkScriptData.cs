using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create TalkScriptData")]
public class TalkScriptData : ScriptableObject
{
    [TextArea]
    public string content;
    public int speed = 18;
    public bool isBubble = false;//吹き出しかどうか
    public  Vector2Int offset = new Vector2Int(0, 16);

    public Speaker speaker = Speaker.Eventer;

    public enum Speaker
    {
        Player,
        Eventer,//Event+offsetの座標
    }
}
