using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class Roundtrip_MoveBlock : MoveBlock
{
    [SerializeField] float LoopTime = 5;

    [SerializeField] float DefaultProgress = 0;

    [SerializeField] Vector2Int Way;

    [SerializeField] Vector2 start;
    [SerializeField] Vector2 end;

    AnimationCurve curve = AnimationCurve.EaseInOut(
    timeStart: 0f,
    valueStart: 0f,
    timeEnd: 1f,
    valueEnd: 1f
);



    private void OnValidate()
    {
        start = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        end = start + Way * 16;
    }

    protected override Vector2Int Calculation(float time)
    {
        float progress = Mathf.PingPong(time + LoopTime * DefaultProgress * 2, LoopTime) / LoopTime;

        Vector2 pos = Vector2.Lerp(start, end, curve.Evaluate(progress));

        return new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, 0.4f);
        Gizmos.DrawCube(start, new Vector3(TerrainHitBox.size.x, 16, 0));

        Gizmos.DrawCube(end, new Vector3(TerrainHitBox.size.x, 16, 0));
        Gizmos.color = Color.white;

        Gizmos.DrawLine(start, end);
    }
}
