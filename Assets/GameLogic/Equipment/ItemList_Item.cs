using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemList_Item : MonoBehaviour
{
    [SerializeField] RectTransform rect;
    [SerializeField] Image image;
    [SerializeField] Image waku;

    public bool isActive
    {
        set
        {
            if (value)
            {
                image.color = Color.white;
                waku.color = Color.white;
            }
            else
            {
                image.color = Color.clear;
                waku.color = Color.clear;
            }
        }
    }

    public Vector2 Pos
    {
        set
        {
            rect.anchoredPosition = value;
        }
    }

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

}
