using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ATK_UP HP_UP　用の上下に揺れるやつ
/// </summary>
public class StatusUP_Vertical : Event_StatusUP
{
    public Transform renderer_transform;
    public int h = 4;
    public override void UpdateFunction(float t)
    {
        int y = Mathf.RoundToInt(h * Mathf.Sin(t * Animation.data.Speed));

        renderer_transform.localPosition = new Vector2(0, y);
    }
}
