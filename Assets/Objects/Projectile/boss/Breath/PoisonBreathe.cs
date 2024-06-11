using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class PoisonBreathe : Projectile
{
    Vector2 BaseV;
    [SerializeField] AnimationCurve curve;
    [SerializeField] Color color;

    float f;
    protected override void OnFireInit()
    {
        base.OnFireInit();
        spriteRenderer.color = color;
        BaseV = BaseVelocity;
        f = Random.Range(3.2f, 4.2f);
    }


    protected override void UpdateFunction()
    {
        BaseVelocity = BaseV * (1 + curve.Evaluate(Mathf.Clamp(OwnTimeSiceStart, 0, 1)));

        if (OwnTimeSiceStart > f||(!spriteRenderer.isVisible&&!GM.MAP.InSameSectionForActivate(player.NowMapGrid,Position)))
        {
            DeactivateCollider();
            Deactivate();
        }
    }
}
