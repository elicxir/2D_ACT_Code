using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create TalkScriptData")]
public class TalkScriptData : ScriptableObject
{
    [TextArea]
    public string content;
    public int speed = 18;
    public bool isBubble = false;//‚«o‚µ‚©‚Ç‚¤‚©
    public  Vector2Int offset = new Vector2Int(0, 16);

    public Speaker speaker = Speaker.Eventer;

    public enum Speaker
    {
        Player,
        Eventer,//Event+offset‚ÌÀ•W
    }
}
