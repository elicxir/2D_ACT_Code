using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class EA_SD_Breathe : EntityAction
{
    [SerializeField] Projectile Smoke;
    [SerializeField] Transform marker1;
    [SerializeField] Transform marker2;

    [SerializeField] SpriteObjectData sod;

    Vector2 Dir
    {
        get
        {
            return (Vector2)marker2.position + Random.insideUnitCircle - (Vector2)marker1.position;
        }
    }

    const float T = 0.0125f;
    const float T2 = 0.1f;

    public override IEnumerator Act()
    {
        Stop();
        entity.BaseVelocity = Vector2.zero;

        Play(ActionName);

        float t = T;

        while (GetState.normalizedTime < 1)
        {
            if (t < 0 && GetState.normalizedTime > (float)16 / 790 && GetState.normalizedTime < (float)112 / 790)
            {
                t = T;
                SpriteObject SO= GM.OBJ.SpriteObjectManager.GenerateByData(sod, marker1.position + Random.onUnitSphere * 48);
                SO.target = marker1;
            }

            if (t < 0&& GetState.normalizedTime>(float)132/790&& GetState.normalizedTime < (float)700 / 790)
            {
                t = T2;
                GM.Projectile.Fire(Smoke, marker1.position, Dir, 56, entity,48);
                GM.Projectile.Fire(Smoke, marker1.position, Dir, 48, entity, 48);
                GM.Projectile.Fire(Smoke, marker1.position, Dir, 40, entity, 48);
                GM.Projectile.Fire(Smoke, marker1.position, Dir, 32, entity, 48);
            }
            t -= entity.OwnDeltaTime;
            yield return null;
        }

        Stop();

    }
}
