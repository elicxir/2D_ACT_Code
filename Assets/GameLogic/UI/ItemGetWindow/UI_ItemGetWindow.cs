using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_ItemGetWindow : MonoBehaviour
{
    [SerializeField] RectTransform rect;
    [SerializeField] TextMeshProUGUI TMPro;
    [SerializeField] Image image;
    [SerializeField] CanvasGroup canvasGroup;

    public void SetPosAlpha(Vector2 pos,float val)
    {
        rect.anchoredPosition = pos;
        canvasGroup.alpha = val;
    }

    public void SetContent(Sprite sprite,string content)
    {
        if (sprite != null)
        {
            image.rectTransform.sizeDelta = sprite.bounds.size;

            image.enabled = true;
            image.sprite = sprite;
            TMPro.rectTransform.anchoredPosition = new Vector2(16, 0);
        }
        else
        {
            image.enabled = false;
            TMPro.rectTransform.anchoredPosition = new Vector2(0, 0);
        }
        TMPro.text = content;

        SetPos();
    }


    void SetPos()
    {
        image.rectTransform.anchoredPosition = new Vector2((int)(16 - TMPro.preferredWidth / 2 - 24), 0);
    }
}
