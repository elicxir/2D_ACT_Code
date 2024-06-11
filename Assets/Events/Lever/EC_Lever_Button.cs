using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_Lever_Button : EventCommand
{
    [SerializeField] Lever lever;

    [SerializeField] float timer;
    [SerializeField] AnimationCurve buttonMode;


    public override IEnumerator Command()
    {
        float timer2 = 0;

        while (timer > timer2)
        {
            timer2 += Time.deltaTime;
            timer2 = Mathf.Clamp(timer2, 0, timer);

            lever.GraphUpdate(buttonMode.Evaluate(timer2/timer));

            yield return null;
        }

    }
}
