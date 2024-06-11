using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EA_Player_Cast1 : PlayerAction
{
    public Action<Vector2> action;

    public float castspeed = 1;

    const float casttiming = 0.45f;

    public override IEnumerator Act()
    {
        Stop();
        bool f = true;

        player.CustomPlayingSpeedMult = castspeed;

        entity.BaseAcceleration.x = 0;

        if (player.PlayerState!= Player.State.Cast1_Air)
        {
            entity.BaseVelocity.x = 0;
        }

        string AnimationName = ActionName;

        Play(AnimationName);

        while (GetState.normalizedTime < 1)
        {
            switch (player.PlayerState)
            {

                case Player.State.Cast1:
                    break;
                case Player.State.Cast1_Air:
                    {
                        JumpControl();

                        AirControll();
                        entity.BaseVelocity.x = Mathf.Clamp(entity.Velocity.x, -player.PAV.AirBaseSpeed, player.PAV.AirBaseSpeed);

                    }
                    break;
                case Player.State.Cast1_Crouch:
                    break;
            }


            if (GetState.normalizedTime > casttiming&&f)
            {
                if (action != null)
                {
                    switch (player.PlayerState)
                    {
                        case Player.State.Cast1:
                        case Player.State.Cast1_Air:
                            action(player.markers[0].position);
                            break;

                        case Player.State.Cast1_Crouch:
                            action(player.markers[1].position);
                            break;
                    }
                    action = null;
                }
                f = false;
            }

            yield return null;
        }

        entity.BaseAcceleration.x = 0;

        Stop();

    }
}
