using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "MyScriptable/EventSettings/EventSettings")]
public class EventSettings : ScriptableObject
{

    [Header("イベント判定に入った時のメッセージ")]
    public string message = "Inspect";
    public Color color = Color.white;

    [Header("イベント判定の大きさ")]
    public Rect Hitbox = new Rect(0, 0, 48, 32);

    [Header("範囲内に入ったときに自動でイベントが開始されるかどうか")]
    public bool AutoEvent = false;

    [Header("falseの場合プレイヤーがイベント中でも操作可能(濫用厳禁)")]
    public bool BindEvent = true;

    [Header("プレイヤーの向きがイベント判定に影響するか")]
    public bool Directional;

    [Header("イベントが有効でなくても表示されるか(スイッチなどではオンにする)")]
    public bool ShowOnDeactivated = true;


}
