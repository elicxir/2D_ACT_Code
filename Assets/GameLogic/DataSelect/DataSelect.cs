using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
using System;
using DataTypes.GameData;

public class DataSelect : GameStateExecuter
{
    bool canselect = true;

    enum Selected
    {
        data1,
        data2,
        data3,
        data4,
        setting,
        quit,
    }

    Selected nowSelect = Selected.data1;

    [SerializeField] DS_Panel[] Panels;
    [SerializeField] DS_Cursor cursor;

    [SerializeField] DataPanel[] dataPanels;

    [SerializeField] CanvasGroup group;

    public override void Updater()
    {
        Wave();
        SelectSystem();
    }

    public override IEnumerator Finalizer(gamestate after)
    {
        yield return StartCoroutine(GAME.FadeOut(0.3f));
        group.alpha = 0;
    }

    public override IEnumerator Init(gamestate before)
    {

        nowSelect = Selected.data1;

        cursor.Haba = 144;
        cursor.Pos = Panels[(int)nowSelect].Pos; ;
        cursor.Width = 137;
        wave_timer =0;

        cursor.SetDelta = 0;

        for (int i = 0; i < 4; i++) {
            dataPanels[i].SetData(SaveData.GetClass<FileData>("FileData", new FileData(), i + 1));
        } 

        group.alpha = 1;

        yield return StartCoroutine(GAME.FadeIn(0.3f));
    }

    void SelectSystem()
    {
        if (canselect)
        {
            if (INPUT.ButtonDown(Control.Decide))
            {
                DECIDE();
                print("decide");

            }
            else if (INPUT.ButtonDown(Control.Cancel))
            {
            }
            else if (INPUT.ButtonDown(Control.Up))
            {
                UP();
            }
            else if (INPUT.ButtonDown(Control.Down))
            {
                DOWN();
            }
        }

    }
    void UP()
    {
        Selected val= (Selected)(((int)nowSelect + Enum.GetNames(typeof(Selected)).Length - 1) % Enum.GetNames(typeof(Selected)).Length);

        StartCoroutine(MoveCursor(nowSelect, val));

        nowSelect = val;



    }
    void DOWN()
    {
        Selected val = (Selected)(((int)nowSelect + 1) % Enum.GetNames(typeof(Selected)).Length);

        StartCoroutine(MoveCursor(nowSelect, val));

        nowSelect = val;

    }

    [SerializeField] AnimationCurve curve;
    float MoveTime = 0.1f;
    IEnumerator MoveCursor(Selected from, Selected to)
    {
        canselect = false;
        int width1, width2;
        int haba1, haba2;
        Vector2 pos1= Panels[(int)from].Pos;
        Vector2 pos2 = Panels[(int)to].Pos;

        switch (from)
        {
            case Selected.data1:
            case Selected.data2:
            case Selected.data3:
            case Selected.data4:
                width1 = 137;
                haba1 = 144;
                break;

            case Selected.setting:
            case Selected.quit:
                width1 = 31;
                haba1 = 20;

                break;

            default:
                width1 = 10;
                haba1 = 20;

                break;
        }

        switch (to)
        {
            case Selected.data1:
            case Selected.data2:
            case Selected.data3:
            case Selected.data4:
                width2 = 137;
                haba2 = 144;
                break;

            case Selected.setting:
            case Selected.quit:
                width2 = 31;
                haba2 = 20;

                break;

            default:
                width2 = 10;
                haba2 = 20;
                break;
        }

        float timer = 0;

        while (MoveTime>timer)
        {
            timer += Time.deltaTime;

            timer=Mathf.Clamp(timer, 0, MoveTime);
            float progress = curve.Evaluate( timer / MoveTime);

            int haba= Mathf.RoundToInt(Mathf.Lerp(haba1, haba2, progress));
            int width = Mathf.RoundToInt( Mathf.Lerp(width1, width2, progress));
            Vector2 p = Vector2.Lerp(pos1, pos2, progress);

            cursor.Haba = haba;
            cursor.Pos = p;
            cursor.Width = width;

            yield return null;

        }
        canselect = true;
    }



    float wave_timer = 0;
    void Wave()
    {
        wave_timer += Time.deltaTime*6;
        cursor.SetDelta = Mathf.RoundToInt( Mathf.Sin(wave_timer));
    }



    void DECIDE()
    {
        switch (nowSelect)
        {
            case Selected.data1:
                GM.Game.MAIN_GAME_START(1);
                GM.Game.StateQueue((int)gamestate.MainGame);
                break;
            case Selected.data2:
                GM.Game.MAIN_GAME_START(2);
                GM.Game.StateQueue((int)gamestate.MainGame);
                break;
            case Selected.data3:
                GM.Game.MAIN_GAME_START(3);
                GM.Game.StateQueue((int)gamestate.MainGame);
                break;
            case Selected.data4:
                GM.Game.MAIN_GAME_START(4);
                GM.Game.StateQueue((int)gamestate.MainGame);
                break;

            case Selected.setting:
                GM.Game.StateQueue((int)gamestate.Settings);
                break;
            case Selected.quit:
                GM.Game.QUIT();
                break;

        }
    }
}