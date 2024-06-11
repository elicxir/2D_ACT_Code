using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
public class FallRock : Projectile
{

    [SerializeField] RockPiece Piece;

    protected override IEnumerator OnHitTerrain(Collider2D c2)
    {
        if (OwnTimeSiceStart > 0.6f)
        {
            GM.Projectile.Fire(Piece, Position + new Vector2(6, 6), new Vector2(3, 6), 80, User);
            GM.Projectile.Fire(Piece, Position + new Vector2(-6, 6), new Vector2(-3, 6), 80, User);
            GM.Projectile.Fire(Piece, Position + new Vector2(-6, -6), new Vector2(-6, 6), 60, User);
            GM.Projectile.Fire(Piece, Position + new Vector2(6, -6), new Vector2(6, 6), 60, User);
            DeactivateCollider();
            Deactivate();
        }



        /*
        Vector2[] poses = GeneratePos(c2.ClosestPoint(Position), 8, 3);

        float timer = 0;
        float DeleteTime = 0.2f;

        BaseVelocity = Vector2.zero;
        gravity = 0;

        foreach (AttackColider ac in attackColider)
        {
            ac.setAC_Active(false);
        }

        int FixedID = ATK_ID;

        foreach (Vector2 pos in poses)
        {
            projectileManager.Fire(Burn, pos + Vector2.up * 8, Vector2.zero, 0, player, FixedID);
        }

        while (timer < DeleteTime)
        {
            float progress = timer / DeleteTime;

            spriteRenderer.color = new Color(1, 1, 1, 1 - progress * progress);

            timer += Time.deltaTime;
            yield return null;
        }

        */

        yield return null;


    }
}
