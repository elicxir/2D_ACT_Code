using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EA_Skelton_Throw : EntityAction
{
    [Header("EA")]
    [SerializeField] Transform marker1;

    [SerializeField] Projectile projectile;
    float casttiming = 0.57f;
    public int power = 100;


    public void Skelton_Throw()
    {
        Projectile.Fire(projectile, marker1.position, entity.FrontVector+Vector2.up*2, power, entity);
    }



    public override IEnumerator Act()
    {
        Stop();
        bool f = true;

        entity.BaseVelocity = Vector2.zero;

        string _currentStateName = ActionName;

        Play(_currentStateName);

        while (GetState.normalizedTime < 1)
        {
            if (GetState.normalizedTime > casttiming && f)
            {
                Skelton_Throw();
                f = false;
            }

            yield return null;
        }

        entity.BaseVelocity = Vector2.zero;

        Stop();
        SettingReset();

    }
}
