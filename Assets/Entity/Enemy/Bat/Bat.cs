using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class Bat : Enemy
{


    void fliptransparent()
    {
        if (sprite_renderer.color == Color.clear)
        {
            sprite_renderer.color = Color.white;

        }
        else
        {
            sprite_renderer.color = Color.clear;
        }
    }

    protected override IEnumerator DeathCoRoutine()
    {
        for (int i = 0; i <12; i++)
        {
            BaseVelocity = Vector2.zero;
            {
                SOM.GenerateByData(SOM.basicSODs[(int)SpriteObjectManager.DataIndex.BurnFire], Position +Random.insideUnitCircle * 12);
                yield return new WaitForSeconds(0.06f);
            }

            fliptransparent();
        }

        DisAppear();
    }







    /*
    protected override bool Detecter(float range)
    {
        int N = 5;

        RaycastHit2D[] hit = new RaycastHit2D[2 * N + 1];


        for (int i = 0; i < hit.Length; i++)
        {
            Vector2 dir = new Vector2(-8, i - N).normalized;
            hit[i] = Physics2D.Raycast(Position, dir, range, PlayerLayer);
        }

        foreach (RaycastHit2D data in hit)
        {
            if (data.collider)
            {

                StartCoroutine(FlyCurve());
                return true;
            }
        }

        return false;
    }

    Action action = Action.Idle;

    public override void Behavior(int timestep)
    {
    }

    [SerializeField] AnimationCurve curve1;
    IEnumerator FlyCurve()
    {
        Vector2 start = Position;
        Vector2 end = Define.PM.Player.Position;
        Vector2 M2 = new Vector2(start.x, end.y);

        Vector2 M1 = Vector2.Lerp(start, M2, 0.7f);
        Vector2 M3 = Vector2.Lerp(end, M2, 0.7f);


        Vector2[] C_Points = new Vector2[] { start, M1, M2, M3, end };

        BezierCurve curve = new BezierCurve(C_Points, 24);

        float dis=0;
        float time = 0;
        while (dis< curve.length)
        {
            dis += curve1.Evaluate(Mathf.Min(time,1)) * 144*Time.fixedDeltaTime;

            Position = curve.GoAlong(dis);
            time += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        StartCoroutine(FlyStraight(end.x - start.x >= 0));
    }

    IEnumerator FlyStraight(bool isRight)
    {

        if (isRight)
        {
            //EntityVelocity = Vector2.right * 144;
        }
        else
        {
            //EntityVelocity = Vector2.left * 144;
        }
        while (true)
        {
            yield return null;
        }
    }
    */
}
