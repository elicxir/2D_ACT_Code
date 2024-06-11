using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EA_Bat_fly : EntityAction
{
    public override IEnumerator Act()
    {

        entity.BaseVelocity = entity.FrontVector * 0;
        string _currentStateName = ActionName;

        Play(_currentStateName);

        while (GetState.normalizedTime < 1)
        {
            yield return null;
        }

        entity.BaseVelocity = Vector2.zero;

        Stop();
    }
}
