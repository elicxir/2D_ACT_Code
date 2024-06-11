using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class EC_Obtain : EventCommand
{
    UI_ItemGetWindow IGW
    {
        get
        {
            return GM.UI.itemGetWindow;
        }
    }

    [SerializeField] float T = 0.4f;

    AnimationCurve curve = AnimationCurve.EaseInOut(
    timeStart: 0f,
    valueStart: 0f,
    timeEnd: 1f,
    valueEnd: 1f
);

    public string ItemName;
    public Sprite Sprite;






    Vector2 pos1(float t)
    {
        return new Vector2(0, -104 - 32 + 32 * curve.Evaluate(t));
    }
    Vector2 pos2(float t)
    {
        return new Vector2(0, -104 - 32 * curve.Evaluate(t));
    }


    public override IEnumerator Command()
    {
        string content = "<color=orange>" + ItemName + "<color=white>" + "‚ðŽè‚É“ü‚ê‚½";
        IGW.SetContent(Sprite, content);

        float timer;

        timer = 0;

        while (timer < T)
        {
            timer += Time.deltaTime;

            timer = Mathf.Clamp(timer, 0, T);

            IGW.SetPosAlpha(pos1(timer / T), 0.7f + 0.3f * timer / T);



            yield return null;
        }

        while (true)
        {
            if (INPUT.ButtonDown(Control.Decide))
            {
                break;
            }
            yield return null;

        }

        timer = 0;

        while (timer < T)
        {
            timer += Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, T);



            IGW.SetPosAlpha(pos2(timer / T), 1 - 0.3f * timer / T);


            yield return null;

        }
    }

}
