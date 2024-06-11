using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using TMPro;
public class MiniMap : GameStateExecuter
{
    [SerializeField] MiniMap_MapGrid MiniMap_MapGrid;

    [SerializeField] CanvasGroup CanvasGroup;

    public TextMeshProUGUI achievement;

    public override IEnumerator Finalizer(gamestate after)
    {
        CanvasGroup.alpha = 1;
        yield return StartCoroutine(Out(0.3f));
        CanvasGroup.alpha = 0;
        MiniMap_MapGrid.canvas.enabled = false;

    }
    public override IEnumerator Init(gamestate before)
    {
        UpdateMapData();



        controlFlag = true;
        nowScale = Scale.x1;

        MiniMap_MapGrid.canvas.enabled = true;
        MiniMap_MapGrid.Init(1);
        cursorbuffer = Vector2.zero;
        cursorPos = Vector2Int.zero;
        CursorPosValidate();


        CanvasGroup.alpha = 0;
        yield return StartCoroutine(In(0.3f));
        CanvasGroup.alpha = 1;
    }
    IEnumerator In(float time)
    {
        float timer = 0;
        do
        {
            timer = Mathf.Min(timer + Time.deltaTime, time);
            CanvasGroup.alpha = timer / time;

            yield return null;

        } while (timer < time);

    }
    IEnumerator Out(float time)
    {
        float timer = 0;
        do
        {
            timer = Mathf.Min(timer + Time.deltaTime, time);
            CanvasGroup.alpha = 1 - timer / time;

            yield return null;

        } while (timer < time);

        CanvasGroup.alpha = 0;
    }



    void UpdateMapData()
    {
        MiniMap_MapGrid.UpdateGrid(GM.Game.PlayData.VisitedGrid);
        achievement.text = MiniMap_MapGrid.Achievement+"%";
    }



    enum Scale
    {
        //x1は16x12
        x1, x2, x3, x4, x5
    }
    Scale nowScale = Scale.x2;

    [SerializeField] AnimationCurve curve;
    float time = 0.2f;
    IEnumerator ChangeScale(int from, int to)
    {
        controlFlag = false;

        MiniMap_MapGrid.rect.localScale = new Vector3(from, from, 1);
        Vector2 CenterPos = MiniMap_MapGrid.rect.anchoredPosition / from;

        float timer = 0;

        while (timer < time)
        {
            timer += Time.deltaTime;

            float progress = Mathf.Clamp01(timer / time);

            float scale = from + (to - from) * curve.Evaluate(progress);

            MiniMap_MapGrid.rect.localScale = new Vector3(scale, scale, 1);
            MiniMap_MapGrid.rect.anchoredPosition = CenterPos * scale;

            yield return null;
        }

        controlFlag = true;
        MiniMap_MapGrid.rect.localScale = new Vector3(to, to, 1);
    }










    bool controlFlag = true;



    public override void Updater()
    {
        MoveMiniMap();
        ChangeGameState();
    }


    void ChangeGameState()
    {
        if (controlFlag)
        {
            if (Define.IM.ButtonDown(Control.Menu))
            {
                GAME.StateQueue((int)gamestate.Menu);
            }
            else if (Define.IM.ButtonDown(Control.Map))
            {
                GAME.StateQueue((int)gamestate.MainGame);
            }
        }
    }



    /// <summary>
    /// 十字でマップ移動
    /// ジャンプでプレイヤー位置にリセット
    /// スペル1で拡大
    /// スペル2で縮小
    /// </summary>
    void MoveMiniMap()
    {/*
        if (controlFlag)
        {
            if (Define.IM.ButtonDown(Control.Spell1))
            {
                ScaleUp();
            }
            else if (Define.IM.ButtonDown(Control.Spell2))
            {
                ScaleDown();
            }
            else if (Define.IM.InputVector() != Vector2.zero)
            {
                cursorbuffer += Define.IM.InputVector() * Time.deltaTime * 120;
                CursorPosValidate();
            }
        }*/
    }

    [SerializeField] RectTransform cursor;
    Vector2 cursorbuffer;
    Vector2Int cursorPos;

    void CursorPosValidate()
    {
        {
            if (cursorbuffer.x > 1)
            {
                cursorPos.x++;
                cursorbuffer.x--;
            }
            if (cursorbuffer.x < -1)
            {
                cursorPos.x--;
                cursorbuffer.x++;
            }
            if (cursorbuffer.y > 1)
            {
                cursorPos.y++;
                cursorbuffer.y--;
            }
            if (cursorbuffer.y < -1)
            {
                cursorPos.y--;
                cursorbuffer.y++;
            }

        }
        {
            if (GameConsts.Game.width / 2 < cursorPos.x)
            {
                cursorPos.x = GameConsts.Game.width / 2;
                cursorbuffer.x = 0;
            }
            else if (-GameConsts.Game.width / 2 > cursorPos.x)
            {
                cursorPos.x = -GameConsts.Game.width / 2;
                cursorbuffer.x = 0;
            }

            if (GameConsts.Game.height / 2 < cursorPos.y)
            {
                cursorPos.y = GameConsts.Game.height / 2;
                cursorbuffer.y = 0;
            }
            else if (-GameConsts.Game.height / 2 > cursorPos.y)
            {
                cursorPos.y = -GameConsts.Game.height / 2;
                cursorbuffer.y = 0;
            }
        }

        cursor.anchoredPosition = cursorPos;
    }



    public void ScaleUp()
    {
        switch (nowScale)
        {
            case Scale.x1:
            case Scale.x2:
            case Scale.x3:
            case Scale.x4:

                StartCoroutine(ChangeScale(((int)nowScale + 1), ((int)nowScale + 2)));
                nowScale = (Scale)((int)nowScale + 1);
                break;
        }
    }
    public void ScaleDown()
    {
        switch (nowScale)
        {
            case Scale.x2:
            case Scale.x3:
            case Scale.x4:
            case Scale.x5:
                StartCoroutine(ChangeScale(((int)nowScale + 1), ((int)nowScale)));
                nowScale = (Scale)((int)nowScale - 1);
                break;
        }
    }







    public bool SimpleMiniMap
    {
        get
        {
            return isSimpleMiniMap;
        }

        set
        {
            isSimpleMiniMap = value;

            if (SimpleMiniMap)
            {
                CanvasGroup.alpha = 1;

            }
            else
            {
                CanvasGroup.alpha = 0;

            }
        }
    }

    bool isSimpleMiniMap;



    public void MiniMapReflesh()
    {
        //MiniMapGrid.RefleshWalkData();
    }


    public void MakePath(Vector2Int sec1, Vector2Int sec2)
    {
        //Define.SDM.GameData.AddPathData(sec1, sec2);
        //MiniMapGrid.MakePath(sec1,sec2);
    }

    public void Init()
    {
        //MiniMapGrid.Init();
    }
}
