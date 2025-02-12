using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataTypes.Functions;
//水平方向に飛んでいく
public class Bat_Horizontal : Bat
{
    const int FlySpeed = 64;//飛行の水平成分
    const int FlySpeedRand = 4;//飛行の水平成分の乱数

    const int FlyHeight = 32;//上下に振動する動きの振幅
    const float FlyTime =1.6f;//上下に振動する動きの一周にかかる時間

    float delta = 0;
    int speedrand = 0;

    public override void Init()
    {
        base.Init();
        delta = 2 * Mathf.PI * 0.125f * Rand.Range(0, 7);
    }

    protected override IEnumerator StateMachine()
    {
        while (isAlive)
        {

            DoAction((int)State.Fly);
            speedrand = Rand.Range(-FlySpeedRand, FlySpeedRand);
            yield return NowAction;
        }
    }

    protected override void EntityUpdater()
    {
        BaseVelocity = FrontVector * (FlySpeed+ speedrand) + FlyHeight * Vector2.up * Mathf.Sin(2 * Mathf.PI * OwnTimeSiceStart / FlyTime+ delta);
    }


    public  enum State
    {
        Fly,
    }
    public  State ActionState
    {
        get
        {
            return (State)ActionIndex;
        }
    }
}
