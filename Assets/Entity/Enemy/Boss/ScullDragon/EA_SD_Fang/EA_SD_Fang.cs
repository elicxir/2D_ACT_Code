using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class EA_SD_Fang : EntityAction
{
    float t = 0;

    bool flag = false;

    const float T = 2.4f;
    public override IEnumerator Act()
    {
        flag = false;
        Stop();
        entity.BaseVelocity = Vector2.zero;

        Play(GetActionName(0));
        while (GetState.normalizedTime < 0.96f)
        {
            yield return null;
        }
        SetTarget();

        while (GetState.normalizedTime < 1)
        {
            yield return null;
        }
        Stop();


        SetDefault();

        flag = true;
        t = 0;
        Play(GetActionName(1));
        while (t < T)
        {

            t += entity.OwnDeltaTime;


            yield return null;
        }

        flag = false;

        Stop();

        Play(GetActionName(2));

        while (GetState.normalizedTime < 1)
        {
            yield return null;
        }
        Stop();

    }


    private void LateUpdate()
    {
        if (!entity.isActive)
        {
            flag = false;
        }

        if (flag)
        {
            headpos.position = head + dir * curve.Evaluate(t/ T);
            headpos.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(angle, targetangle, curve2.Evaluate(t/ T)));
        }
    }

    [SerializeField] AnimationCurve curve;
    [SerializeField] AnimationCurve curve2;


    [SerializeField] Transform headpos;
    [SerializeField] Transform NeckRoot;
    Vector2 head;
    Vector2 dir = new Vector2(-200, -100);

    float angle;
    float targetangle;

    Vector2 pPos;


    public void SetTarget()
    {
        pPos = GM.Player.Player.Position;


    }

    public void SetDefault()
    {
        angle = headpos.eulerAngles.z;
        head = headpos.position;

        Vector2 v = pPos - (Vector2)NeckRoot.position;
        Vector2 v2 = pPos - head;

        float aa = Mathf.Atan2(v2.y, v2.x) * 180 / Mathf.PI;

        aa=Mathf.Clamp(aa, -240, -145);

        Vector2 v3 = new Vector2(Mathf.Cos(aa*Mathf.PI/180), Mathf.Sin(aa * Mathf.PI / 180));

        targetangle = Mathf.Atan2(v.y, v.x) * 180 / Mathf.PI + 180;

        dir = Mathf.Clamp(v2.magnitude*1.2f,200,300) * v3;

    }

}
