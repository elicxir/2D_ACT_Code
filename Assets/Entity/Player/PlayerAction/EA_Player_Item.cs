using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EA_Player_Item : PlayerAction
{
    public Action action;

    public float castspeed = 1;

    float casttiming = 0.50f;

    public override IEnumerator Act()
    {
        Stop();
        bool f = true;

        player.CustomPlayingSpeedMult = castspeed;

        bool isCrouch = player.PlayerState == Player.State.Item_Crouch;

        entity.BaseAcceleration.x = 0;
        if (player.isGround)
        {
            entity.BaseVelocity.x = 0;
        }

        string AnimationName = ActionName;

        Play(AnimationName);

        while (GetState.normalizedTime < 1)
        {

            if (GetState.normalizedTime > casttiming && f)
            {
                if (action != null)
                {
                    if (isCrouch)
                    {
                        action();
                    }
                    else
                    {
                        action();
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
