using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusUP_Diffuse : Event_StatusUP
{
    public Transform renderer_transform1;
    public Transform renderer_transform2;
    public Transform renderer_transform3;
    public Transform renderer_transform4;

    public override void UpdateFunction(float t)
    {
        float counter = t *12;
        renderer_transform1.localPosition = Vector2.down * 1 + Vector2.left * 4 + delta(counter, -2, 1, 0.3f, 0.2f);
        renderer_transform2.localPosition = Vector2.down * 1 + Vector2.right * 4 + delta(counter, 2, 1, 0.3f, 0.2f);
        renderer_transform3.localPosition = Vector2.down * 6 + delta(counter, 0, -1, 0.1f, 0.3f);
        renderer_transform4.localPosition = Vector2.up * 6 + delta(counter, 1, 4, 0.3f, 0.4f);
    }

    float s = 0.5f;
    float s2 = 0.2f;

    int y1 = 2;
    int x1 = 1;

    Vector2 delta(float counter, int x, int y, float sx, float sy)
    {
        return new Vector2(x + Mathf.RoundToInt(x * Mathf.Sin(counter * sx)), y + Mathf.RoundToInt(y * Mathf.Sin(counter * sy)));

    }

}
