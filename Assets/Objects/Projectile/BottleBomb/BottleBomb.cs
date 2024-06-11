using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BottleBomb : Projectile
{
    [SerializeField] Projectile Burn;

    protected override void OnFireInit()
    {
        base.OnFireInit();
    }

    protected override IEnumerator OnHitTerrain(Collider2D c2)
    {
        Vector2[] poses = GeneratePos(c2.ClosestPoint(Position), 8, 3);

        float timer = 0;
        float DeleteTime = 0.2f;

        BaseVelocity = Vector2.zero;
        BaseAcceleration = Vector2.zero;

        foreach (AttackColider ac in attackColider)
        {
            ac.setAC_Active(false);
        }

        int FixedID = ATK_ID;

        foreach (Vector2 pos in poses)
        {
            projectileManager.Fire(Burn, pos + Vector2.up * 8, Vector2.zero, 0, player, FixedID);
        }
        DeactivateCollider();

        while (timer < DeleteTime)
        {
            float progress = timer / DeleteTime;

            spriteRenderer.color = new Color(1, 1, 1, 1 - progress * progress);

            timer += Time.deltaTime;
            yield return null;
        }

        Deactivate();
    }


}
