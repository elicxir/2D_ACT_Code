using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EA_Skelton_Die : EntityAction
{




    public override IEnumerator Act()
    {

        string _currentStateName = "die";

        entity.BaseVelocity = Vector2.zero;
        Play(_currentStateName);

        while (GetState.normalizedTime < 1)
        {
            yield return null;
        }

        entity.BaseVelocity = Vector2.zero;

        Stop();

        if (entity is Enemy)
        {
            Enemy enemy = (Enemy)entity;
            enemy.DisAppear();
        }
    }
}
