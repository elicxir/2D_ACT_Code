using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Managers;

public class UI_Bar2 : MonoBehaviour
{
    [SerializeField] CanvasGroup cg;
    [SerializeField] Image bar;
    [SerializeField] TextMeshProUGUI text;
    public Entity entity;

    public List<Entity> Boss = new List<Entity>();


    public void BossStart(string str)
    {
        text.text = str;
    }

    const float show = 0.56f;
    const float hide = 1.6f;

    IEnumerator Show()
    {
        float timer = 0;
        cg.alpha = 0f;

        while (timer < show)
        {
            if (GM.Game.GameState == gamestate.MainGame)
            {
                timer += Time.deltaTime;
            }

            cg.alpha = Mathf.Clamp(timer / show, 0f, 1f);

            yield return null;
        }
        cg.alpha = 1;

    }

    IEnumerator Hide()
    {
        float timer = 0;

        while (timer < hide)
        {
            if (GM.Game.GameState == gamestate.MainGame)
            {
                timer += Time.deltaTime;
            }

            cg.alpha = Mathf.Clamp(1 - timer / hide, 0f, 1f);

            yield return null;
        }
        cg.alpha = 0f;
    }


    Coroutine coroutine;


    public void UI_Show()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);

        }
        coroutine = StartCoroutine(Show());
    }
    public void UI_Hide(bool forced=false)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);

        }

        if (forced)
        {
            cg.alpha = 0f;
        }
        else
        {
            coroutine = StartCoroutine(Hide());

        }
    }


    public void Updater()
    {
        if (cg.alpha > 0)
        {
            float HP = 0;
            foreach (var item in Boss)
            {
                HP += item.entityStats.HP;
            }

            float MaxHP = 0;
            foreach (var item in Boss)
            {
                MaxHP += item.entityStats.MaxHP;
            }

            bar.fillAmount = (float)(HP) / MaxHP;

        }

    }
}
