using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EA_Player_Rapier : PlayerAction
{
    public float castspeed = 1;
    public int index = 0;

    int time = 0;
    public Action<Vector2> action;
    const float casttiming = 0.53f;

    public AC_DataSet dataset;


    public override IEnumerator Act()
    {

        Stop();

        foreach (var item in player.AC_Groups)
        {
            foreach (var ac in item.attackColiders)
            {
                ac.dataset = dataset;
            }
        }

        bool f = true;
        bool aircast = !player.isGround;

        bool isCrouch = player.PlayerState == Player.State.KnockBack_Crouch || player.PlayerState == Player.State.Cast1_Crouch;

        player.CustomPlayingSpeedMult = castspeed;

        entity.BaseAcceleration.x = 0;

        if (player.isGround)
        {
            entity.BaseVelocity.x = 0;
        }

        string AnimationName = GetActionName(index);

        Play(AnimationName);

        while (GetState.normalizedTime < 1)
        {
            switch (player.PlayerState)
            {
                case Player.State.Rapier:

                    break;
                case Player.State.Rapier_Crouch:

                    break;

                case Player.State.Rapier_AirDown:
                case Player.State.Rapier_AirUp:
                    JumpControl();
                    AirControll();
                    entity.BaseVelocity.x = Mathf.Clamp(entity.Velocity.x, -player.PAV.AirBaseSpeed, player.PAV.AirBaseSpeed);

                    break;

            }


            if (GetState.normalizedTime > casttiming && f)
            {
                if (action != null)
                {
                    switch (player.PlayerState)
                    {
                        case Player.State.Rapier:
                            action(player.markers[2].position);
                            break;

                        case Player.State.Rapier_Crouch:
                            action(player.markers[3].position);
                            break;

                        case Player.State.Rapier_AirUp:
                            action(player.markers[4].position);
                            break;

                        case Player.State.Rapier_AirDown:
                            action(player.markers[5].position);
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
