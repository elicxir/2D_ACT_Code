using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Warning : MonoBehaviour
{
    [SerializeField] CanvasGroup group;

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image image;


    [SerializeField] AnimationCurve curve;
    [SerializeField] float time = 0.8f;

    IEnumerator Warning()
    {
        float timer = 0;

        while (true)
        {
            timer = Mathf.Clamp(timer, 0, time);

            group.alpha = curve.Evaluate(timer / time);

            if (timer == time)
            {
                yield break;
            }

            timer += Time.deltaTime;
            yield return null;

        }

    }

    Coroutine coroutine;
    public void SHOW_WARNING(string mes, Color color)
    {

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        text.color = color;
        text.text = mes;
        image.rectTransform.sizeDelta = new Vector2(text.preferredWidth + 6, 18);

        coroutine = StartCoroutine(Warning());
    }
}
