using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPanelExecuter : GameStateExecuter
{




    [SerializeField] CanvasGroup canvasGroup;
    public override IEnumerator Finalizer(gamestate after)
    {
        yield return StartCoroutine(Out(0.3f));
    }

    public override IEnumerator Init(gamestate before)
    {
        yield return StartCoroutine(In(0.3f));
    }

    protected IEnumerator In(float time)
    {
        float timer = 0;

        do
        {
            timer = Mathf.Min(timer + Time.deltaTime, time);

            canvasGroup.alpha = timer / time;

            yield return null;

        } while (timer < time);
        canvasGroup.alpha =1;


    }

    protected IEnumerator Out(float time)
    {
        float timer = 0;

        do
        {
            timer = Mathf.Min(timer + Time.deltaTime, time);
            canvasGroup.alpha = 1 - timer / time;

            yield return null;

        } while (timer < time);
        canvasGroup.alpha = 0;

    }

}
