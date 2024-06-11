using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//‚Í‚¶‚ß‚Í‘Ò‹@‚µ‚Ä‚¨‚è‹ß‚Ã‚­‚Æ”ò‚ñ‚Å‚­‚é
public class Bat_Wait : Bat
{
    public enum State
    {
        Fly,Wait,Trans
    }
    public State ActionState
    {
        get
        {
            return (State)ActionIndex;
        }
    }

    protected override IEnumerator StateMachine()
    {
        while (isAlive)
        {
            switch (ActionState)
            {
                case State.Fly:
                case State.Trans:
                    DoAction((int)State.Fly);
                    yield return NowAction;
                    break;
                case State.Wait:
                    DoAction((int)State.Wait);
                    yield return NowAction;
                    break;

            }

        }
    }


    public override void Init()
    {
        StopAllCoroutines();
        base.Init();
        OverrideAction((int)State.Wait);
    }

    bool detect()
    {
        int N = 3;

        RaycastHit2D[] hit = new RaycastHit2D[2 * N + 1];


        for (int i = 0; i < hit.Length; i++)
        {
            Vector2 dir = new Vector2(i - N, -5).normalized;
            hit[i] = Physics2D.Raycast(Position, dir, 200, PlayerLayer);

            Debug.DrawRay(Position, dir * 200, Color.white, 0.2f);
        }

        foreach (RaycastHit2D data in hit)
        {
            if (data.collider)
            {
                return true;
            }
        }

        return false;
    }



    IEnumerator Fly1()
    {
        Vector2 dir = (Player.Position - Position).normalized;


        float timer = 0;

        while (true)
        {

            timer += OwnDeltaTime;
            if (isAlive)
            {
                BaseVelocity = dir * 84 * Mathf.Clamp(timer, 0.4f, 1);

            }
            yield return null;
        }
    }



    [SerializeField] AnimationCurve curve1;
    IEnumerator FlyCurve()
    {
        Vector2 start = Position;
        Vector2 end = Define.PM.Player.Position;
        Vector2 M2 = new Vector2(start.x, end.y);

        Vector2 M1 = Vector2.Lerp(start, M2, 0.3f);
        Vector2 M3 = Vector2.Lerp(end, M2, 0.3f);


        Vector2[] C_Points = new Vector2[] { start, M1, M2, M3, end };

        BezierCurve curve = new BezierCurve(C_Points, 24);

        float dis = 0;
        float time = 0;
        while (dis < curve.length)
        {
            dis += curve1.Evaluate(Mathf.Min(time, 1)) * 72 * Time.deltaTime;

            Position = curve.GoAlong(dis) + 6 * Vector2.up * Mathf.Sin(time * 0.4f);
            time += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(FlyStraight(end.x - start.x >= 0));
    }

    protected override void EntityUpdater()
    {
        base.EntityUpdater();

        if (ActionState == State.Wait)
        {
            if (detect() || HP != MaxHP)
            {
                if (Player.Position.x < Position.x)
                {
                    Flip();
                }
                OverrideAction((int)State.Trans);

                StartCoroutine(Fly1());
            }
        }
    }


    IEnumerator FlyStraight(bool isRight)
    {


        while (true)
        {
            if (isRight)
            {
                BaseVelocity = Vector2.right * 72 + Vector2.up * Random.Range(-12, 12);
            }
            else
            {
                BaseVelocity = Vector2.left * 72 + Vector2.up * Random.Range(-12, 12);
            }
            yield return null;
        }
    }
}
