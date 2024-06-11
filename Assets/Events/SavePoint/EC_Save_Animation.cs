using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_Save_Animation : EventCommand
{
    public float animate_timer =0.7f;

    [SerializeField] Event_SavePoint savePoint;

    [SerializeField] Sprite[] anim;

    public override IEnumerator Command()
    {
        float timer1 = 0;

        while (animate_timer > timer1)
        {
            timer1 += Time.deltaTime;
            timer1 = Mathf.Clamp(timer1, 0, animate_timer);

            savePoint.sr.sprite = anim[Mathf.Clamp(Mathf.FloorToInt(anim.Length * timer1 / animate_timer), 0, anim.Length - 1)];

            yield return null;
        }

    }
}
