using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EA_Player_jump : PlayerAction
{
    public override IEnumerator Act()
    {
        string AnimationName = string.Empty;

        switch (player.BeforeState)
        {
            case Player.State.Jump:
            case Player.State.Cast1_Air:
            case Player.State.KnockBack_Air:
            case Player.State.Rapier_AirDown:
            case Player.State.Rapier_AirUp:
                AnimationName = GetActionName(1);
                break;

            default:
                AnimationName = GetActionName(0);
                entity.BaseVelocity.y = player.PAV.JumpPower;
                break;
        }

        Play(AnimationName);

        while (GetState.normalizedTime < 1)
        {
            JumpControl();


            AirControll();
            entity.BaseVelocity.x = Mathf.Clamp(entity.BaseVelocity.x, -player.PAV.AirBaseSpeed, player.PAV.AirBaseSpeed);

            yield return null;
        }
        entity.BaseAcceleration.x = 0;
        Stop();
    }
}
