using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrippleDagger : SpellFunction
{
    [SerializeField] Projectile projectile;
    
    public override void Fire(Vector2 pos, Vector2 dir)
    {
        projectileManager.Fire(projectile, pos+Vector2.up*6, player.FrontVector, 400, player);
        projectileManager.Fire(projectile, pos+ player.FrontVector*6, player.FrontVector, 400, player);
        projectileManager.Fire(projectile, pos + Vector2.down * 6, player.FrontVector, 400, player);

        /*

        Vector2 GetAngle(float a)
        {
            return new Vector2(Mathf.Cos(a*Mathf.Deg2Rad), Mathf.Sin(a * Mathf.Deg2Rad));
        }

        void Shot(float angle)
        {
            projectileManager.Fire(projectile, pos, GetAngle(angle-3), 400, player);
            projectileManager.Fire(projectile, pos, GetAngle(angle), 400, player);
            projectileManager.Fire(projectile, pos, GetAngle(angle + 3), 400, player);
        }


            if (Define.PM.Player.FrontVector.x > 0)
            {
                Shot(0);
            }
            else if (Define.PM.Player.FrontVector.x < 0)
            {
                Shot(180);
            }*/

    }
}
