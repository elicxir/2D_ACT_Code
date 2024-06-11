using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class EA_SD_Smash : EntityAction
{
    int xmax=105;
    int xmin=95;
    int ymin=150;
    int ymax=152;

    [SerializeField] Projectile fallrock;

    public override IEnumerator Act()
    {
        Stop();
        entity.BaseVelocity = Vector2.zero;

        Play(ActionName);
        bool f = true;

        while (GetState.normalizedTime < 1)
        {
            if (GetState.normalizedTime > 0.33f && f)
            {
                f = false;
                FallRock();
            }
                yield return null;
        }

        Stop();

    }



    void FallRock()
    {
        GM.Projectile.Fire(fallrock, new Vector2(GM.Player.Player.Position.x, Random.Range(ymin, ymax) * 16), Vector2.down, 0, entity);

        for (int i = 0; i < Random.Range(5, 9); i++) {

            GM.Projectile.Fire(fallrock, new Vector2(Random.Range(xmin, xmax)*48, Random.Range(ymin, ymax)*16), Vector2.down, 0, entity);

        }


    }
}
