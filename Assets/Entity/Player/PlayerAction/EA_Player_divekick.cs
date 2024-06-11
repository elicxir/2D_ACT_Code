using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EA_Player_divekick : PlayerAction
{


    public override IEnumerator Act()
    {
        entity.BaseVelocity = Vector2.zero;




        Play(ActionName);

        entity.BaseVelocity.y = player.PAV.JumpPower;


        while (GetState.normalizedTime < 1)
        {
            JumpControl();


            AirControll();
            entity.BaseVelocity.x = Mathf.Clamp(entity.BaseVelocity.x, -player.PAV.AirBaseSpeed, player.PAV.AirBaseSpeed);


            yield return null;
        }

        //yield return new WaitUntil(() => !player.canMoveDown);
        entity.BaseAcceleration.x = 0;

        Stop();
        SettingReset();

    }

}
