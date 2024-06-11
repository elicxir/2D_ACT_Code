using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapBlock : MonoBehaviour
{
    public Color color_notVisited;
    public Color color_Visited;

    [SerializeField] CanvasGroup CanvasGroup;

    [SerializeField] RectTransform Base;
    [SerializeField] Image BaseImage;

    [SerializeField] Sprite[] V_Wall;
    [SerializeField] Sprite[] H_Wall;

    [SerializeField] RectTransform[] Wall;
    [SerializeField] Image[] WallImage;

    /// <summary>
    /// 0 壁
    /// 1 通路
    /// 2 壁無し
    /// </summary>
    public int up = 0;
    public int down = 0;
    public int right = 0;
    public int left = 0;

    /*
    Vector2Int WallBlock(int horizontal,int vertical)
    {
        int x=0;
        int y=0;

        switch (horizontal)
        {
            case 0:
                y = BlockRect.y / 2;
                break;

            case 1:
                y = BlockRect.y / 2-3 * Mult;
                break;

            case 2:
                y = Mult;
                break;
        }

        switch (vertical)
        {
            case 0:
                x = BlockRect.x / 2;
                break;

            case 1:
                x = BlockRect.x / 2 - 4* Mult;
                break;

            case 2:
                x =  Mult;
                break;
        }


        if (horizontal == 2)
        {
            if (vertical == 2)
            {
                x = 0;
                y = 0;
            }
        }

        return new Vector2Int(x, y);
    }
    */



    public void SetWall()
    {
        switch (left)
        {
            case 0:
                WallImage[0].color = Color.white;
                WallImage[0].sprite = V_Wall[0];
                break;
            case 1:
                WallImage[0].color = Color.white;
                WallImage[0].sprite = V_Wall[1];
                break;
            default:
                WallImage[0].color = Color.clear;
                WallImage[0].sprite = V_Wall[0];
                break;
        }

        switch (right)
        {
            case 0:
                WallImage[1].color = Color.white;
                WallImage[1].sprite = V_Wall[0];
                break;
            case 1:
                WallImage[1].color = Color.white;
                WallImage[1].sprite = V_Wall[1];
                break;
            default:
                WallImage[1].color = Color.clear;
                WallImage[1].sprite = V_Wall[0];
                break;
        }

        switch (up)
        {
            case 0:
                WallImage[2].color = Color.white;
                WallImage[2].sprite = H_Wall[0];
                break;
            case 1:
                WallImage[2].color = Color.white;
                WallImage[2].sprite = H_Wall[1];
                break;
            default:
                WallImage[2].color = Color.clear;
                WallImage[2].sprite = H_Wall[0];
                break;
        }
        switch (down)
        {
            case 0:
                WallImage[3].color = Color.white;
                WallImage[3].sprite = H_Wall[0];
                break;
            case 1:
                WallImage[3].color = Color.white;
                WallImage[3].sprite = H_Wall[1];
                break;
            default:
                WallImage[3].color = Color.clear;
                WallImage[3].sprite = H_Wall[0];
                break;
        }

    }


    //拡大倍率に合わせて壁を配置する。
    void SetSize()
    {
        Base.sizeDelta = BlockRect;

        Wall[0].sizeDelta = new Vector2Int(1* Mult, BaseRect.y* Mult);
        Wall[0].anchoredPosition = new Vector2((-BlockRect.x + Wall[0].sizeDelta.x)*0.5f,0);

        Wall[1].sizeDelta = new Vector2Int(1 * Mult, BaseRect.y * Mult);
        Wall[1].anchoredPosition = new Vector2((BlockRect.x - Wall[1].sizeDelta.x) * 0.5f,0);

        Wall[2].sizeDelta = new Vector2Int(BaseRect.x * Mult, 1 * Mult);
        Wall[2].anchoredPosition = new Vector2(0, (BlockRect.y -Wall[2].sizeDelta.y) * 0.5f);

        Wall[3].sizeDelta = new Vector2Int(BaseRect.x * Mult, 1 * Mult);
        Wall[3].anchoredPosition = new Vector2(0, (-BlockRect.y + Wall[3].sizeDelta.y) * 0.5f);
    }



    //マップにブロックが表示されているかどうか
    public bool isVisible
    {
        get
        {
            return isVisible_var;
        }

        set
        {
            isVisible_var = value;
            if (isVisible)
            {
                CanvasGroup.alpha = 1;
            }
            else
            {
                CanvasGroup.alpha = 0;

            }

        }
    }

    bool isVisible_var;



    //踏破済みかどうか
    public bool isVisited
    {
        get
        {
            return isVisited_var;
        }

        set
        {
            isVisited_var = value;

            if (isVisited)
            {
                BaseImage.color = new Color(color_Visited.r, color_Visited.g, color_Visited.b, 0.7f);
            }
            else
            {
                BaseImage.color = color_notVisited;
            }

        }
    }

    bool isVisited_var;

    Vector2Int BaseRect = new Vector2Int(24, 18);

    /// <summary>
    /// ブロック拡大倍率 整数値 1-6
    /// </summary>
    public int Mult
    {
        get
        {
            return mult_value;
        }

        set
        {
            mult_value = Mathf.Max(Mathf.Min(6, value), 1);
            SetSize();
            SetWall();
        }
    }

    int mult_value = 1;

    Vector2Int BlockRect
    {
        get
        {
            return BaseRect * Mult;
        }
    }


}
