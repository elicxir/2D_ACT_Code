using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;

public class UI_ArtsPallet : MonoBehaviour
{
    [SerializeField] Color Selected;
    [SerializeField] Color NotSelected;

    [SerializeField] Image A;
    [SerializeField] Image B;

    [SerializeField] Canvas canvas_A;
    [SerializeField] Canvas canvas_B;

    [SerializeField] AnimationCurve curve;

    Coroutine coroutine;

    public bool Pallet()
    {
        coroutine = StartCoroutine(Change(!nowSelected));
        return true;

        if (coroutine == null)
        {
            return true;
            /*
            StopCoroutine(coroutine);
            coroutine = null;*/
        }
        else
        {
            return false;
        }


    }

    public void SetNowSelected(bool state)
    {
        nowSelected = state;
    }

    bool nowSelected = true;








    void SetWidth(int width)
    {
        A.rectTransform.anchoredPosition = Vector2.left * width;
        B.rectTransform.anchoredPosition = Vector2.right * width;
    }












    /// <summary>
    /// false‚ÅA true‚ÅB
    /// </summary>
    /// <param name="state"></param>
    void SetState(bool state)
    {
        if (state)
        {
            canvas_B.sortingOrder = -4;
            B.color = Selected;
            A.color = NotSelected;
        }
        else
        {
            canvas_B.sortingOrder = -6;
            A.color = Selected;
            B.color = NotSelected;
        }
    }

    const float T=0.16f;
    const int maxWidth=10;
    const int minWidth = 3;

    IEnumerator Change(bool state)
    {
        float timer = 0;

        while (timer<T)
        {
            if (GM.Game.GameState == gamestate.MainGame)
            {
                timer += Time.deltaTime;
            }

            float progress = Mathf.Clamp01(timer/T);

            SetWidth(Mathf.RoundToInt(Mathf.Lerp(minWidth, maxWidth, curve.Evaluate(progress))));
            if (progress > 0.5f)
            {
                SetState(state);
            }

            yield return null;
        }

    }
    

}
