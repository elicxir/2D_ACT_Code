using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EA_Skelton_Step : EntityAction
{
    public const float stepSpeed=54;

    public override IEnumerator Act()
    {
        float dir = Mathf.Sign(entity.FrontVector.x);

        entity.BaseVelocity.x = 0;


        Play(ActionName);

        while (GetState.normalizedTime < 1)
        {
            float absVelocity = stepSpeed*(1- GetState.normalizedTime);
            entity.BaseVelocity.x = -dir* absVelocity;

            yield return null;
        }

        entity.BaseVelocity.x = 0;

        Stop();
        SettingReset();

    }
}
