using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
using Functions;
using TMPro;

public class TitleScreen : GameStateExecuter
{

    [SerializeField] CanvasGroup group;
    [SerializeField] Image image;
    [SerializeField] RectTransform rect;

    [SerializeField] AnimationCurve curve;

    [SerializeField] float scrolltime;
    [SerializeField] float totaltime;

    [SerializeField] TextMeshProUGUI ver;

    float progress;
    int scrollheight
    {
        get
        {
            return Mathf.Max(((int)rect.sizeDelta.y-240),0);
        }
    }

    public override IEnumerator Init(gamestate before)
    {
        progress = 0;
        rect.anchoredPosition = new Vector2(0, scrollheight / 2);

        group.alpha = 1;
        ver.text = "ver "+Application.version;

        yield return StartCoroutine(GAME.FadeIn(1,()=> {
            progress = Mathf.Min(progress + Time.deltaTime, totaltime);
            rect.anchoredPosition = new Vector2(0, scrollheight / 2 - scrollheight * curve.Evaluate(progress / totaltime));
        }));

    }



    public override void Updater()
    {

        progress = Mathf.Min(progress+Time.deltaTime,totaltime);
        rect.anchoredPosition = new Vector2(0, scrollheight / 2- scrollheight*curve.Evaluate(progress/totaltime));
        

        if (INPUT.ButtonDown(Control.Decide))
        {
            if (progress < totaltime) {
                progress = totaltime;

            }
            else
            {
               GAME.StateQueue((int)gamestate.DataSelect);

            }
        }
    }

    public override IEnumerator Finalizer(gamestate after)
    {
        yield return StartCoroutine(GAME.FadeOut(1.2f));     
        group.alpha = 0;


        yield break;

    }

}
