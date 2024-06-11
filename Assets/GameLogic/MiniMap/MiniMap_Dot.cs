using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MiniMap_Dot : MonoBehaviour
{
    public bool isVisited
    {
        get
        {
            return isVisitedVar;
        }
        set
        {
            image1.enabled = value;
            isVisitedVar = value;
        }
    }
    bool isVisitedVar = false;

    public RectTransform rect;
    public Image image1;
    public void SetState(bool isWall, Color color)
    {
        if (isWall)
        {
            image1.color = Color.white;
        }
        else
        {
            image1.color = color;

        }
    }
}
