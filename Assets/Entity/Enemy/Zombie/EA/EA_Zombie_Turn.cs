using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EA_Zombie_Turn : EntityAction
{

    public override IEnumerator Act()
    {

        entity.BaseVelocity = Vector2.zero;

        string _currentStateName = "turn";

        Play(_currentStateName);

        while (GetState.normalizedTime < 1)
        {
            yield return null;
        }

        entity.BaseVelocity = Vector2.zero;

        Stop();


        //entity.face_right = !entity.face_right;
        SettingReset();
    }
}