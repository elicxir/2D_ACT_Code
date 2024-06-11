using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_BossDoor_Close : EventCommand
{
    [SerializeField] Boss_Door boss_door;
    [SerializeField] BossDoor_ValueData datas;
    public override IEnumerator Command()
    {
        boss_door.SetSprite = datas.sprite[0];

        float timer1 = 0;

        while (datas.close_wait > timer1)
        {
            timer1 += Time.deltaTime;
            timer1 = Mathf.Clamp(timer1, 0, datas.close_wait);

            yield return null;
        }

        float timer2 = 0;

        while (datas.close_timer > timer2)
        {
            timer2 += Time.deltaTime;
            timer2 = Mathf.Clamp(timer2, 0, datas.close_timer);

            float val = datas.close_curve.Evaluate(timer2 / datas.close_timer);
            boss_door.GraphUpdate(Mathf.RoundToInt(80 * val));

            yield return null;
        }

        boss_door.isOpened = false;
    }
}
