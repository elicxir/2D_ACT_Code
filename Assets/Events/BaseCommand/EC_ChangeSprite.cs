using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_ChangeSprite : EventCommand
{
    [SerializeField] Sprite[] Front;
    [SerializeField] Sprite[] Back;

    [SerializeField] Event_ChangeSprite event_;

    [SerializeField] float animate_timer;

    public override IEnumerator Command()
    {
        float timer1 = 0;

        while (animate_timer > timer1)
        {
            timer1 += Time.deltaTime;
            timer1 = Mathf.Clamp(timer1, 0, animate_timer);

            if (Front.Length > 0)
            {
                event_.spriteRenderer_front.sprite = Front[Mathf.Clamp(Mathf.FloorToInt(Front.Length * timer1 / animate_timer), 0, Front.Length - 1)];
            }
            if (Back.Length > 0)
            {
                event_.spriteRenderer_back.sprite = Back[Mathf.Clamp(Mathf.FloorToInt(Back.Length * timer1 / animate_timer), 0, Back.Length - 1)];
            }

            yield return null;
        }
    }


}
