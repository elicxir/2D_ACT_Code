using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
using System;
using TMPro;
public class GameOver : SubPanelExecuter
{

    [SerializeField] Image cursor;

    [SerializeField] Transform[] point;

    [SerializeField] CanvasGroup group;


    enum Selected
    {
        Continue,
        Title,
        Quit
    }

    Selected nowSelect = Selected.Continue;

    float wave_timer = 0;
    void Wave()
    {
        wave_timer += Time.deltaTime * 5;
        cursor.rectTransform.anchoredPosition = new Vector2(-44+2*Mathf.Sin(wave_timer), cursor.rectTransform.anchoredPosition.y);
    }

    public override IEnumerator Init(gamestate before)
    {
        GM.UI.BossHP.UI_Hide(true);

        nowSelect = Selected.Continue;
        wave_timer = 0;
        Wave();
        cursor.rectTransform.anchoredPosition = new Vector2(cursor.rectTransform.anchoredPosition.x, rects[(int)nowSelect].anchoredPosition.y);

        yield return StartCoroutine(In(1));
        GM.Game.transitionpanel.alpha = 0;

        group.alpha = 1;

    }

    public override IEnumerator Finalizer(gamestate after)
    {

        GM.Game.transitionpanel.alpha = 1;
        group.alpha = 0;

        yield break;
    }


    public override void Updater()
    {
        Wave();
        SelectSystem();
    }

    void SelectSystem()
    {
        if (INPUT.ButtonDown(Control.Up)&&canselect)
        {
            UP();
        }
        else if (INPUT.ButtonDown(Control.Down) && canselect)
        {
            DOWN();
        }
        else if (INPUT.ButtonDown(Control.Decide))
        {
            switch (nowSelect)
            {
                case Selected.Continue:
                    GM.Game.Continue();
                    GM.Game.StateQueue((int)gamestate.MainGame);
                    break;

                case Selected.Title:
                    GM.Game.StateQueue((int)gamestate.Title);
                    break;

                case Selected.Quit:
                    GM.Game.QUIT();
                    break;
            }
        }
    }


    void UP()
    {
        Selected val = (Selected)(((int)nowSelect + Enum.GetNames(typeof(Selected)).Length - 1) % Enum.GetNames(typeof(Selected)).Length);
        StartCoroutine(MoveCursor(nowSelect, val));
        nowSelect = val;
    }

    void DOWN()
    {
        Selected val = (Selected)(((int)nowSelect + 1) % Enum.GetNames(typeof(Selected)).Length);
        StartCoroutine(MoveCursor(nowSelect, val));
        nowSelect = val;
    }

    bool canselect=true;
    float MoveTime = 0.12f;

    [SerializeField] RectTransform[] rects;
    [SerializeField] AnimationCurve curve;

    IEnumerator MoveCursor(Selected from, Selected to)
    {
        canselect = false;

        int y1 = Mathf.RoundToInt( rects[(int)from].anchoredPosition.y);
        int y2 = Mathf.RoundToInt(rects[(int)to].anchoredPosition.y);

        float timer = 0;

        while (MoveTime > timer)
        {
            timer += Time.deltaTime;

            timer = Mathf.Clamp(timer, 0, MoveTime);
            float progress = curve.Evaluate(timer / MoveTime);

            int y =Mathf.RoundToInt(Mathf.Lerp(y1, y2, progress))  ;

            cursor.rectTransform.anchoredPosition = new Vector2(cursor.rectTransform.anchoredPosition.x,y);

            yield return null;

        }
        canselect = true;
    }

}
