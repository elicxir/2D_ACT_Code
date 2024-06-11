using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger_of_Loyalty : SpellFunction
{
    int prefabindex = 0;
    protected override void Start()
    {
        prefabindex = projectileManager.GetIndex("ReturnDagger");
    }

    public override void Fire(Vector2 pos, Vector2 dir)
    {

        Vector2 GetAngle(float a)
        {
            return new Vector2(Mathf.Cos(a * Mathf.Deg2Rad), Mathf.Sin(a * Mathf.Deg2Rad));
        }

        void Shot(float angle)
        {
            //projectileManager.Fire(prefabindex, Define.PM.Player.Position + Vector2.up * 5, GetAngle(angle - 16), 600, player);
            //projectileManager.Fire(prefabindex, Define.PM.Player.Position + Vector2.up * 5, GetAngle(angle), 600, player);
            //projectileManager.Fire(prefabindex, Define.PM.Player.Position + Vector2.up * 5, GetAngle(angle + 16), 600, player);
            //projectileManager.Fire(prefabindex, Define.PM.Player.Position + Vector2.up * 5, GetAngle(angle - 8), 600, player);
            //projectileManager.Fire(prefabindex, Define.PM.Player.Position + Vector2.up * 5, GetAngle(angle + 8), 600, player);

        }

        if (Define.PM.Player.FrontVector.x > 0)
        {
            Shot(0);
        }
        else if (Define.PM.Player.FrontVector.x < 0)
        {
            Shot(180);
        }


    }

}
