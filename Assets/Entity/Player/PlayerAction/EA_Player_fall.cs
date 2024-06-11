using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EA_Player_fall : PlayerAction
{
    public override IEnumerator Act()
    {

        string AnimationName = string.Empty;

        switch (player.BeforeState)
        {
            case Player.State.Jump:
                AnimationName = GetActionName(3);
                break;


            case Player.State.Stand:
            case Player.State.Walk:
                AnimationName = GetActionName(2);
                break;

            case Player.State.Crouch:
            case Player.State.Sliding:
                AnimationName = GetActionName(1);
                break;

            default:
                AnimationName = GetActionName(0);
                break;
        }

        Play(AnimationName);

        while (GetState.normalizedTime < 1)
        {
            AirControll();
            entity.BaseVelocity.x = Mathf.Clamp(entity.BaseVelocity.x, -player.PAV.AirBaseSpeed, player.PAV.AirBaseSpeed);

            yield return null;
        }

        entity.BaseAcceleration.x = 0;
        Stop();


    }
}
