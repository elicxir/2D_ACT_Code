using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DS_Panel : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    public Vector2 Size
    {
        get
        {
            return rectTransform.sizeDelta;
        }
    }

    public Vector2 Pos
    {
        get
        {
            return rectTransform.anchoredPosition;
        }
    }

}
