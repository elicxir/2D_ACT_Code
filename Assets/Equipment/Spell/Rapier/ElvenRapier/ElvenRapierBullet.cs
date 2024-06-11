using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElvenRapierBullet : Projectile
{
    [SerializeField] AnimationCurve curve;
    const float time=0.4f;
    Vector2 startvelocity;
    protected override void OnFireInit()
    {
        base.OnFireInit();
        startvelocity = BaseVelocity;
    }

    protected override void UpdateFunction()
    {
        print(Mathf.Clamp01(OwnTimeSiceStart / time));
        BaseVelocity = startvelocity-startvelocity * Mathf.Clamp01(OwnTimeSiceStart / time);

        spriteRenderer.color = new Color(1, 1, 1, curve.Evaluate(Mathf.Clamp01(OwnTimeSiceStart / time)));
        if (OwnTimeSiceStart > time)
        {
            Deactivate();
        }
    }

}
