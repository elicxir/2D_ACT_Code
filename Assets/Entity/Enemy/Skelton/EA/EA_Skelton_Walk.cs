using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EA_Skelton_Walk : EntityAction
{
    public override IEnumerator Act()
    {

        entity.BaseVelocity.x = entity.FrontVector.x * 32;

        string _currentStateName = ActionName;

        //entity.Velocity = entity.FrontVector * 8;
        Play(_currentStateName);

        while (GetState.normalizedTime < 1)
        {
            yield return null;
        }

        entity.BaseVelocity.x =0;

        Stop();
        SettingReset();

    }

}
