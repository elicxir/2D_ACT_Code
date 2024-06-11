using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EA_SD_Wait : EntityAction
{
    public override IEnumerator Act()
    {
        entity.BaseVelocity = Vector2.zero;
        Play(ActionName);

        while (GetState.normalizedTime < 1)
        {
            yield return null;
        }

        entity.BaseVelocity = Vector2.zero;

        Stop();
    }
}
