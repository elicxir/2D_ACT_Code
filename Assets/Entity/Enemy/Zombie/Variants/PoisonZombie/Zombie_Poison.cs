using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataTypes.Functions;

public class Zombie_Poison : Zombie
{
    protected void Breath(int power)
    {
        EA_PZombie_Breath throw_ = (EA_PZombie_Breath)Actions[(int)State.Breath];
        throw_.power = power;
        DoAction((int)State.Breath);
    }


    protected override IEnumerator StateMachine()
    {
        while (isAlive)
        {
            if (isChase)
            {
                Walk(DashSpeed);
            }
            else
            {
                Walk(WalkSpeed);
            }
            yield return NowAction;

            if (FacePlayer&&inRange(20,70))
            {
                while(Rand.Range(0,3)<3&& MPConsume(30))
                {
                    Breath(Rand.Range(0, 2)*10+90);
                    yield return NowAction;
                }                
            }

            if (isChase)
            {
                if (!FacePlayer)
                {
                    DoAction((int)State.Turn);
                    yield return NowAction;
                }
            }
            else
            {
                if (FaceOuter && !inExtent_X)
                {
                    DoAction((int)State.Turn);
                    yield return NowAction;
                }
            }




        }
    }

}
