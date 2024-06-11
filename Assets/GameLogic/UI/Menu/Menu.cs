using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
using System;
using TMPro;
public class Menu : GameStateExecuter
{
    bool canselect = true;

    [SerializeField] Menu_Cursor cursor;

    [SerializeField] Image[] rune;

    void Color(float val)
    {
        for(int i = 0; i < rune.Length; i++)
        {
            float s = 0.5f + 0.5f * Mathf.Sin(val + i *2* Mathf.PI / 3);
            rune[i].color = new Color((40+95* s)/255, (40+180* s) / 255, (40+176* s) / 255, 1);
        }
    }



    float wave_timer = 0;
    void Wave()
    {
        wave_timer += Time.deltaTime * 11;
        Color(wave_timer*0.13f);
        cursor.SetDelta = Mathf.RoundToInt(Mathf.Sin(wave_timer));
    }


    enum Selected
    {
        Status,
        Equipment,
        KeyItem,
        Memo,
        Setting,
        Quit,
    }

    Selected nowSelect = Selected.Status;




    [SerializeField] Menu_Content[] Contents;

    float MoveTime = 0.16f;
    IEnumerator MoveCursor(Selected from, Selected to)
    {
        canselect = false;

        bool updateflag = true;

        Vector2 pos1 = Contents[(int)from].Pos;
        Vector2 pos2 = Contents[(int)to].Pos;

        float timer = 0;

        while (MoveTime > timer)
        {
            timer += Time.deltaTime;

            timer = Mathf.Clamp(timer, 0, MoveTime);
            float progress = curve.Evaluate(timer / MoveTime);

            Vector2 p = Vector2.Lerp(pos1, pos2, progress);

            cursor.Pos = p;


            if (progress > 0.5f&&updateflag)
            {
                SetGraphColor(to);
                SetText(to);
                updateflag = false;
            }

            yield return null;

        }
        canselect = true;
    }

    void SetGraphColor(Selected selected)
    {
        for (int i = 0; i < Contents.Length; i++)
        {
            Contents[i].Selected = false;
        }

        Contents[(int)selected].Selected = true;
    }

    void SetText(Selected selected)
    {
        Text1.text = Contents[(int)selected].MenuName;
        Text2.text = Contents[(int)selected].inst1;
    }

    [SerializeField] CanvasGroup menuCanvas;

    [SerializeField] CanvasGroup bg;
    [SerializeField] RectTransform menuPanel;





    public override IEnumerator Finalizer(gamestate after)
    {
        if (after == gamestate.MainGame)
        {
            yield return StartCoroutine(Out(0.3f));
        }
        else if(after == gamestate.DataSelect)
        {
            yield return StartCoroutine(GAME.FadeOut(0.5f));
            yield return StartCoroutine(Out(0.01f));
        }
        else
        {

        }

        yield break;
    }

    public override IEnumerator Init(gamestate before)
    {
        if (before == gamestate.MainGame)
        {
            nowSelect = Selected.Status;
            menuCanvas.alpha = 1;
            wave_timer = 0;
            Wave();
            cursor.Pos = Contents[(int)nowSelect].Pos;
            SetGraphColor(nowSelect);
            SetText(nowSelect);

            yield return StartCoroutine(In(0.3f));
        }
        else
        {
        }


    }

    [Header("In Out アニメーション用")]
    [SerializeField] AnimationCurve curve;
    [SerializeField] float SlideInLangth;

    IEnumerator In(float time)
    {
        float timer = 0;
        float progress;

        do
        {
            timer = Mathf.Min(timer + Time.deltaTime, time);

            progress = curve.Evaluate(timer / time);

            menuPanel.anchoredPosition = Vector2.left * SlideInLangth + Vector2.right * SlideInLangth * progress;
            bg.alpha = timer / time;

            yield return null;

        } while (timer < time);

    }

    IEnumerator Out(float time)
    {
        float timer = 0;
        float progress;

        do
        {
            timer = Mathf.Min(timer + Time.deltaTime, time);

            progress = curve.Evaluate(timer / time);

            menuPanel.anchoredPosition = Vector2.left * SlideInLangth * progress;
            bg.alpha = 1 - timer / time;

            yield return null;

        } while (timer < time);
        menuCanvas.alpha = 0;
    }



    public override void Updater()
    {
        Wave();
        SelectSystem();
    }

    void Right()
    {
        Selected val = (Selected)(((int)nowSelect + 1) % Enum.GetNames(typeof(Selected)).Length);
        StartCoroutine(MoveCursor(nowSelect, val));
        nowSelect = val;
    }

    void Left()
    {
        Selected val = (Selected)(((int)nowSelect + Enum.GetNames(typeof(Selected)).Length - 1) % Enum.GetNames(typeof(Selected)).Length);
        StartCoroutine(MoveCursor(nowSelect, val));
        nowSelect = val;
    }
    [SerializeField] Sprite waku1;
    [SerializeField] Sprite waku2;

    [SerializeField] TextMeshProUGUI Text1;
    [SerializeField] TextMeshProUGUI Text2;

    //
    float quitcount = 0;

    void SelectSystem()
    {
        if (canselect)
        {
            if (INPUT.ButtonDown(Control.Right))
            {
                Right();
            }
            else if (INPUT.ButtonDown(Control.Left))
            {
                Left();
            }

            if (INPUT.ButtonDown(Control.Decide))
            {
                switch (nowSelect)
                {
                    case Selected.Status:
                        GAME.StateQueue((int)gamestate.Status);
                        break;
                    case Selected.Equipment:
                        GAME.StateQueue((int)gamestate.Equipments);
                        break;
                    case Selected.Memo:
                        GAME.StateQueue((int)gamestate.Memo);
                        break;
                    case Selected.Setting:
                        GAME.StateQueue((int)gamestate.Settings);
                        break;
                    case Selected.KeyItem:
                        GAME.StateQueue((int)gamestate.KeyItems);

                        break;
                }
            }
            else if (INPUT.Button(Control.Decide) &&nowSelect== Selected.Quit )
            {
                quitcount+=Time.deltaTime;

                if (quitcount>0.8f)
                {
                    GAME.ResetPlayData();
                    GAME.StateQueue((int)gamestate.DataSelect);
                }

            }

            else if (INPUT.ButtonDown(Control.Menu) || Define.IM.ButtonDown(Control.Cancel))
            {
                GAME.StateQueue((int)gamestate.MainGame);
            }
            else
            {
                quitcount = 0;
            }
        }


    }

}