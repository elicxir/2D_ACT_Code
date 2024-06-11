using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_Door : EventCommand
{
    [SerializeField] protected Door door;

    [SerializeField] protected float timer;

    [SerializeField] protected AnimationCurve curve;

    [SerializeField] protected bool willOpen;


    public override IEnumerator Command()
    {
        float timer2 = 0;

        while (timer>timer2)
        {
            timer2 += Time.deltaTime;
            timer2 = Mathf.Clamp(timer2, 0, timer);

            float val;
            if (willOpen)
            {
                val=curve.Evaluate(timer2 / timer);
            }
            else
            {
                val=1- curve.Evaluate(timer2 / timer);
            }

            door.GraphUpdate((int)(((door.Height / 2)+4) *val ));

            yield return null;
        }

        door.isOpened = willOpen;
    }


}
