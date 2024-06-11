using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Managers;
using DataTypes.Functions;
using DataTypes.Element;
public class Zombie : Enemy
{
    public enum State
    {
        Walk, Turn, Die, Breath
    }
    public State ActionState
    {
        get
        {
            return (State)ActionIndex;
        }
    }

    public State ActionStateBefore
    {
        get
        {
            return (State)BeforeActionIndex;
        }
    }

    protected bool isChase = false;



    protected const int WalkSpeed = 10;
    protected const int DashSpeed = 17;

    public override void Init()
    {
        base.Init();
        isChase = false;
    }

    protected void Walk(int speed)
    {
        EA_Zombie_Walk ea = (EA_Zombie_Walk)Actions[(int)State.Walk];

        ea.speed = speed + Rand.Range(-1, 1);

        DoAction((int)State.Walk);
    }

    protected override IEnumerator StateMachine()
    {
        while (isAlive)
        {
            if (isChase)
            {
                Walk(DashSpeed);
            }
            else
            {
                Walk(WalkSpeed);
            }

            yield return NowAction;

            if (isChase)
            {
                if (!FacePlayer)
                {
                    DoAction((int)State.Turn);
                    yield return NowAction;
                }
            }
            else
            {
                if (FaceOuter && !inExtent_X)
                {
                    DoAction((int)State.Turn);
                    yield return NowAction;
                }
            }

        }
    }


    protected override void OnFaceWall(FaceDirection direction)
    {
        if (ActionState != State.Turn && direction == faceDirection)
        {
            OverrideAction((int)State.Turn);
            isChase = false;
        }
    }

    protected override void OnFaceHole(FaceDirection direction)
    {
        if (!CanFall || !isChase)
        {
            if (ActionState != State.Turn)
            {
                OverrideAction((int)State.Turn);
                isChase = false;
            }
        }
    }

    public bool CanFall;

    void ChaseStart()
    {
        isChase = true;

        if (ActionStateBefore == State.Walk && ActionState == State.Walk)
        {
            StopAction();
        }
    }

    protected override void OnDamageDealt(Element damage, Vector2 pos)
    {
        base.OnDamageDealt(damage, pos);
        ChaseStart();
    }

    protected override IEnumerator DeathCoRoutine()
    {
        DoAction((int)State.Die);
        yield return NowAction;

        DisAppear();
    }

    protected override void EntityUpdater()
    {
        if (!isChase && inRange(0, 64))
        {
            ChaseStart();
        }
    }
}
