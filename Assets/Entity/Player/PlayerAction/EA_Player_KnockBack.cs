using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EA_Player_KnockBack : PlayerAction
{
    public override IEnumerator Act()
    {
        Stop();
        bool f = true;
        bool aircast = !player.isGround;

        bool isCrouch = player.PlayerState == Player.State.KnockBack_Crouch|| player.PlayerState == Player.State.Cast1_Crouch ;

        entity.BaseAcceleration.x = 0;

        if (player.isGround)
        {
            entity.BaseVelocity.x = 0;
        }
        else
        {
            entity.BaseVelocity.x = -entity.FrontVector.x * 50;
            entity.BaseVelocity.y = 0;
        }

        string AnimationName = ActionName;

        Play(AnimationName);

        while (GetState.normalizedTime < 1)
        {

            if (aircast)
            {
                if (GetState.normalizedTime > 0.5f)
                {

                    {
                        AirControll();
                        entity.BaseVelocity.x = Mathf.Clamp(entity.BaseVelocity.x, -player.PAV.AirBaseSpeed, player.PAV.AirBaseSpeed);
                    }
                }
                else
                {
                    entity.BaseVelocity.x = -entity.FrontVector.x * 50;

                }
            }



     
            yield return null;
        }



        entity.BaseAcceleration.x = 0;

        Stop();

    }
}
