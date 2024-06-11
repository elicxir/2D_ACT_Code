using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_Door_Animated : EC_Door
{
    public float animate_timer;

    [SerializeField] Sprite[] UP;
    [SerializeField] Sprite[] Down;

    

    public override IEnumerator Command()
    {
        float timer1 = 0;

        while (animate_timer > timer1)
        {
            timer1 += Time.deltaTime;
            timer1 = Mathf.Clamp(timer1, 0, animate_timer);

            door.up_sr.sprite = UP[Mathf.Clamp(Mathf.FloorToInt(UP.Length * timer1 / animate_timer), 0, UP.Length - 1)];
            door.down_sr.sprite = Down[Mathf.Clamp(Mathf.FloorToInt(Down.Length * timer1 / animate_timer), 0, Down.Length - 1)];

            yield return null;
        }


        float timer2 = 0;

        while (timer > timer2)
        {
            timer2 += Time.deltaTime;
            timer2 = Mathf.Clamp(timer2, 0, timer);

            float val;
            if (willOpen)
            {
                val = curve.Evaluate(timer2 / timer);
            }
            else
            {
                val = 1 - curve.Evaluate(timer2 / timer);
            }

            door.GraphUpdate((int)(((door.Height / 2) + 4) * val));

            yield return null;
        }

        door.isOpened = willOpen;
    }
}
