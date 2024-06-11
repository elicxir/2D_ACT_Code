using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class PlayerAction : EntityAction
{
    protected Player player
    {
        get
        {
            return GM.Player.Player;
        }
    }

    protected InputSystemManager Input
    {
        get
        {
            return GM.Inputs;

        }
    }

    protected void JumpControl()
    {
            if (Input.ButtonUp(Control.Jump) && entity.BaseVelocity.y > player.PAV.JumpPower * 0.4f)
            {
                entity.BaseVelocity.y = player.PAV.JumpPower * 0.4f;
            }
    }



    protected void AirControll()
    {
        if (Input.InputVector().x > 0)
        {
            entity.BaseAcceleration.x = +player.PAV.AirAcceleration*0.4f;
        }
        else if (Input.InputVector().x < 0)
        {
            entity.BaseAcceleration.x = -player.PAV.AirAcceleration * 0.4f;
        }
        else
        {
            if (Mathf.Abs(player.BaseVelocity.x) > 5)
            {
                if (player.BaseVelocity.x > 0)
                {
                    entity.BaseAcceleration.x = -player.PAV.AirStopAcceleration;

                }
                else if (player.BaseVelocity.x < 0)
                {
                    entity.BaseAcceleration.x = player.PAV.AirStopAcceleration;
                }
            }
            else
            {
                player.BaseVelocity.x = 0;
                entity.BaseAcceleration.x = 0;

            }

        }
    }

    protected void StandControll()
    {
        if (Mathf.Abs(player.BaseVelocity.x) > 5)
        {
            if (player.BaseVelocity.x > 0)
            {
                entity.BaseAcceleration.x = -player.PAV.StopAcceleration;

            }
            else if (player.BaseVelocity.x < 0)
            {
                entity.BaseAcceleration.x = player.PAV.StopAcceleration;
            }
        }
        else
        {
            if (Mathf.Abs(player.BaseVelocity.x) > 5)
            {
                if (player.BaseVelocity.x > 0)
                {
                    entity.BaseAcceleration.x = -player.PAV.StopAcceleration;

                }
                else if (player.BaseVelocity.x < 0)
                {
                    entity.BaseAcceleration.x = player.PAV.StopAcceleration;
                }
            }
            else
            {
                player.BaseVelocity.x = 0;
                entity.BaseAcceleration.x = 0;
            }

        }
    }


}
