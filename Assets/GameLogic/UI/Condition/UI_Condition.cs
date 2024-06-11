using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Condition : MonoBehaviour
{
    int poison;
    int hurt;
    int curse;

    [SerializeField] RectTransform rect;

    [SerializeField] CanvasGroup cg;

    [SerializeField] Sprite P;
    [SerializeField] Sprite H;
    [SerializeField] Sprite C;


    enum mode
    {
        Poison,Hurt,Curse,None
    }

    mode[] M = new mode[3];


    [SerializeField] Image[] waku;
    [SerializeField] Image[] bar;

    public void SetData(EntityStats data)
    {
        poison = Mathf.CeilToInt(data.Poison) ;
        hurt = Mathf.CeilToInt(data.Hurt);
        curse = Mathf.CeilToInt(data.Curse);

        bool f_p = (poison > 0);
        bool f_h = (hurt > 0);
        bool f_c = (curse > 0);

        cg.alpha = 0;

        for (int i = 0; i < 3; i++)
        {
            if (f_p)
            {
                f_p = false;
                M[i] = mode.Poison;
                cg.alpha = 1;

            }
            else if(f_h)
            {
                f_h = false;
                M[i] = mode.Hurt;
                cg.alpha = 1;

            }
            else if (f_c)
            {
                f_c = false;
                M[i] = mode.Curse;
                cg.alpha = 1;

            }
            else
            {
                M[i] = mode.None;
            }

        }

        for (int i = 0; i < 3; i++)
        {            
            if (M[i]!=mode.None)
            {
                waku[i].color =Color.white;
                bar[i].color = Color.white;
            }
            else
            {
                waku[i].color = Color.clear;
                bar[i].color = Color.clear;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            switch (M[i])
            {
                case mode.Poison:
                    bar[i].sprite = P;
                    bar[i].fillAmount = poison * 0.01f;

                    break;
                case mode.Hurt:
                    bar[i].sprite = H;
                    bar[i].fillAmount = hurt * 0.01f;

                    break;
                case mode.Curse:
                    bar[i].sprite = C;
                    bar[i].fillAmount = curse * 0.01f;

                    break;
                case mode.None:
                    break;
                default:
                    break;
            }
        }


    }

    public void SetPos(Vector2 Pos)
    {
        rect.anchoredPosition = Pos+Vector2.up*33;
    }

    [ContextMenu("clear")]
    void Clear()
    {
        for (int i = 0; i < 3; i++)
        {

            {
                waku[i].color = Color.clear;
                bar[i].color = Color.clear;
            }
        }
    }
}
