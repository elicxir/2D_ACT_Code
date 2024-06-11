using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EA_Zombie_Walk : EntityAction
{
    public int speed = 10;

    public override IEnumerator Act()
    {
        entity.BaseVelocity.x = entity.FrontVector.x * speed;

        //entity.Velocity = entity.FrontVector * 8;
        Play(ActionName);

        entity.CustomPlayingSpeedMult = (float)speed/10;

        while (GetState.normalizedTime< 1)
        {
            yield return null;
        }

        entity.BaseVelocity.x = 0;

        Stop();
        SettingReset();

    }
}
