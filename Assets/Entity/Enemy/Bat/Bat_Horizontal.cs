using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataTypes.Functions;
//…•½•ûŒü‚É”ò‚ñ‚Å‚¢‚­
public class Bat_Horizontal : Bat
{
    const int FlySpeed = 64;//”òs‚Ì…•½¬•ª
    const int FlySpeedRand = 4;//”òs‚Ì…•½¬•ª‚Ì—”

    const int FlyHeight = 32;//ã‰º‚ÉU“®‚·‚é“®‚«‚ÌU•
    const float FlyTime =1.6f;//ã‰º‚ÉU“®‚·‚é“®‚«‚Ìˆêü‚É‚©‚©‚éŠÔ

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
