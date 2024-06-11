using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerStorm : SpellFunction
{
    string prefabname = "StagnantDagger";
    int prefabindex = 0;

    int poscount = 0;

    Vector2[] knifePos = new Vector2[12];
    StagnantDagger[] daggers = new StagnantDagger[12];



    Vector2 GetAngle(float a)
    {
        return new Vector2(Mathf.Cos(a * Mathf.Deg2Rad), Mathf.Sin(a * Mathf.Deg2Rad));
    }



    protected override void Start()
    {
        prefabindex = projectileManager.GetIndex(prefabname);
    }

    public override void IncreaseStart()
    {
        base.IncreaseStart();

        for (int i = 0; i < 12; i++)
        {
            //daggers[i] = (StagnantDagger)projectileManager.Fire(prefabindex, knifePos[i], Define.PM.Player.FrontVector, 575, player);
            //daggers[i].ST();
        }
    }

    public override void Increasing()
    {
        base.Increasing();

        for (int i = 0; i < 12; i++)
        {
            daggers[i].Position = knifePos[i];
        }

    }

    public override void EmpoweredFire(float ratio)
    {
        base.EmpoweredFire(ratio);
        StartCoroutine("Release");
    }


    public override void FixedUpdater()
    {
        base.FixedUpdater();
        poscount++;

        for (int i = 0; i < 12; i++)
        {
            knifePos[i] = Define.PM.Player.Position + GetAngle(-poscount*2 + 360 * i / knifePos.Length+120*i) * 50;
        }

    }

    IEnumerator Release()
    {
        for (int i = 0; i < 12; i++)
        {
            daggers[i].GO(550);


            for(int u = 0; u < 20; u++)
            {
                for (int g = i + 1; g < 12; g++)
                {
                    daggers[g].Position = knifePos[g];
                }
                yield return new WaitForFixedUpdate();

            }


        }

        yield break;
    }

}
