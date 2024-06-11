using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EA_PZombie_Breath : EntityAction
{
    [Header("EA")]
    [SerializeField] Transform marker1;

    [SerializeField] Projectile projectile;

    float casttiming = 0.47f;

    public int power = 80;


    public void PZombie_Poison_Breath()
    {
        Projectile.Fire(projectile, marker1.position,new Vector2(entity.FrontVector.x,-0.15f), power, entity);
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
                PZombie_Poison_Breath();
                f = false;
            }
            yield return null;
        }

        entity.BaseVelocity = Vector2.zero;

        Stop();
        SettingReset();

    }

}
