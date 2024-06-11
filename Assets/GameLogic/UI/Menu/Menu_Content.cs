using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Content : MonoBehaviour
{
    [SerializeField] RectTransform rect;
    [SerializeField] Image image;
    [SerializeField] MenuData data;

    private void OnValidate()
    {
        image.sprite = data.sprite;

    }

    public string MenuName
    {
        get
        {
            return data.menuname;
        }
    }
    public string inst1
    {
        get
        {
            return data.inst;
        }
    }

    public bool Selected
    {
        set
        {
            if (value)
            {
                image.color = Color.white;
            }
            else
            {
                image.color = Color.gray;
            }
        }
    }

    public Vector2 Pos
    {
        set
        {
            rect.anchoredPosition = value;
        }
        get
        {
            return rect.anchoredPosition;
        }
    }



}
