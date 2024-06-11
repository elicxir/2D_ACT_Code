using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EA_SD_Walk : EntityAction
{
    [SerializeField] AnimationCurve curve;
    [SerializeField] float speed;
    public override IEnumerator Act()
    {
        Stop();
        entity.BaseVelocity = Vector2.zero;

        Play(ActionName);

        while (GetState.normalizedTime < 1)
        {
            entity.BaseVelocity.x = entity.FrontVector.x * speed*curve.Evaluate(GetState.normalizedTime);

            yield return null;
        }

        Stop();

    }
}
