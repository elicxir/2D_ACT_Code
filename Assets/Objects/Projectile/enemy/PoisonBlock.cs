using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBlock : Projectile
{
    [SerializeField] SpriteObjectData ef;

    protected override void OnFireInit()
    {
        base.OnFireInit();
        transform.localScale = new Vector3(1, 1, 1);
    }

    protected override IEnumerator OnHitTerrain(Collider2D c2)
    {
        Vector2[] poses = GeneratePos(Position,2,4);

        float timer = 0;
        float DeleteTime = 0.4f;

        BaseVelocity = Vector2.zero;
        
        foreach (AttackColider ac in attackColider)
        {
            ac.setAC_Active(false);
        }

        foreach(Vector2 pos in poses)
        {
            SOM.GenerateByData(ef, pos + Vector2.down * 4);
        }

        while (timer < DeleteTime)
        {
            float progress = timer / DeleteTime;

            transform.localScale=new Vector3(1,1-progress,1);

            timer += Time.deltaTime;
            yield return null;
        }



        Deactivate();
    }

}
