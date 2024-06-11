using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap_PlayerMarker : MonoBehaviour
{
    [SerializeField] Image image;

    public Vector2 Pos
    {
        set
        {
            image.rectTransform.anchoredPosition = value;
        }
        get
        {
            return image.rectTransform.anchoredPosition;
        }
    }
}
