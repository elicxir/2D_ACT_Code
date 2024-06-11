using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_BossDoor_Open : EventCommand
{
    [SerializeField] Boss_Door boss_door;
    [SerializeField] BossDoor_ValueData datas;

    public override IEnumerator Command()
    {
        float timer1 = 0;

        while (datas.animate_timer > timer1)
        {
            timer1 += Time.deltaTime;
            timer1 = Mathf.Clamp(timer1, 0, datas.animate_timer);

            boss_door.SetSprite = datas.sprite[Mathf.Clamp(Mathf.FloorToInt(datas.sprite.Length * timer1 / datas.animate_timer), 0, datas.sprite.Length - 1)];

            yield return null;
        }

        float timer2 = 0;

        while (datas.open_timer > timer2)
        {
            timer2 += Time.deltaTime;
            timer2 = Mathf.Clamp(timer2, 0, datas.open_timer);

            float val = datas.open_curve.Evaluate(timer2 / datas.open_timer);
            boss_door.GraphUpdate(Mathf.RoundToInt(80 * val));

            yield return null;
        }
        boss_door.isOpened = true;
    }

}
