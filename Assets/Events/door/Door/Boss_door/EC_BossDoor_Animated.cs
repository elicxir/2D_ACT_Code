using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_BossDoor_Animated : EventCommand
{





    public enum Mode
    {
        Enter, Enter_Close, Open
    }
    public Mode mode;


    float animate_timer = 1;
    float open_timer = 3;
    float close_timer =0.7f;

    [SerializeField] Sprite[] sprite;

    [SerializeField] Boss_Door boss_door;

    [SerializeField] AnimationCurve open_curve;
    [SerializeField] AnimationCurve close_curve;


    IEnumerator Enter()
    {
        float timer1 = 0;

        while (animate_timer > timer1)
        {
            timer1 += Time.deltaTime;
            timer1 = Mathf.Clamp(timer1, 0, animate_timer);

            boss_door.SetSprite = sprite[Mathf.Clamp(Mathf.FloorToInt(sprite.Length * timer1 / animate_timer), 0, sprite.Length - 1)];

            yield return null;
        }

        float timer2 = 0;

        while (open_timer > timer2)
        {
            timer2 += Time.deltaTime;
            timer2 = Mathf.Clamp(timer2, 0, open_timer);

            float val = open_curve.Evaluate(timer2 / open_timer);
            boss_door.GraphUpdate(Mathf.RoundToInt(80 * val));

            yield return null;
        }
        boss_door.isOpened = true;
        mode = Mode.Enter_Close;
    }

    IEnumerator Open()
    {
        float timer2 = 0;

        while (open_timer > timer2)
        {
            timer2 += Time.deltaTime;
            timer2 = Mathf.Clamp(timer2, 0, open_timer);

            float val = open_curve.Evaluate(timer2 / open_timer);
            boss_door.GraphUpdate(Mathf.RoundToInt(80 * val));

            yield return null;
        }
        boss_door.isOpened = true;

    }



    IEnumerator Close()
    {
        float timer2 = 0;

        while (close_timer > timer2)
        {
            timer2 += Time.deltaTime;
            timer2 = Mathf.Clamp(timer2, 0, close_timer);

            float val = close_curve.Evaluate(timer2 / close_timer);
            boss_door.GraphUpdate(Mathf.RoundToInt(80 * val));

            yield return null;
        }
        boss_door.isOpened = false;
    }




    public override IEnumerator Command()
    {
        switch (mode)
        {
            case Mode.Enter:
                yield return StartCoroutine(Enter());
                break;

            case Mode.Enter_Close:
                yield return StartCoroutine(Close());
                break;

            case Mode.Open:
                yield return StartCoroutine(Open());
                break;

            default:
                break;
        }

        yield break;

    }


}
