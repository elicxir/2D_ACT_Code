using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TalkWindow : MonoBehaviour
{ 
    [SerializeField] CanvasGroup cg;

    [SerializeField] RectTransform talkwindow;
    public  TextMeshProUGUI text;

    [SerializeField] RectTransform hukidashi;
    [SerializeField] RectTransform window;

    public IEnumerator Show(float time)
    {
        float mult = 1 / time;
        cg.alpha = 0;

        while (cg.alpha < 1)
        {
            cg.alpha += Time.deltaTime * mult;
            cg.alpha = Mathf.Min(cg.alpha, 1);      
            yield return null;
        }
        cg.alpha = 1;
    }

    public IEnumerator Hide(float time)
    {
        float mult = 1 / time;
        cg.alpha = 1;

        while (cg.alpha > 0)
        {
            cg.alpha -= Time.deltaTime * mult;
            cg.alpha = Mathf.Max(cg.alpha, 0);
            yield return null;
        }
        cg.alpha = 0;
    }

    public void SetText(string content)
    {
        text.text = content;
    }

    public void SetWindowSize(Vector2Int size,bool isBubble=true)
    {
        hukidashi.gameObject.SetActive(isBubble);
        window.sizeDelta = size;
        hukidashi.anchoredPosition = new Vector2(0, -(window.sizeDelta.y/2+8));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="r_pos"> òbé“ÇÃÉJÉÅÉâÇ…ëŒÇ∑ÇÈëäëŒç¿ïW</param>
    public void SetWindowPosition(Vector2 r_pos)
    {
        r_pos= talkwindow.anchoredPosition = r_pos+Vector2.up* (window.sizeDelta.y/2+24)+Vector2.left*12;

        while (WindowRight > 155)
        {
            talkwindow.anchoredPosition += Vector2Int.left;
            hukidashi.anchoredPosition += Vector2Int.right;


        }

        while (WindowLeft < -155)
        {
            talkwindow.anchoredPosition += Vector2Int.right;
            hukidashi.anchoredPosition += Vector2Int.left;

        }
    }

    int WindowRight
    {
        get
        {
            return (int)(talkwindow.anchoredPosition + window.sizeDelta / 2).x;
        }
    }

    int WindowLeft
    {
        get
        {
            return (int)(talkwindow.anchoredPosition - window.sizeDelta / 2).x;
        }
    }

}
