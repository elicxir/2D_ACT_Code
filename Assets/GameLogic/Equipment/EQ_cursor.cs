using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EQ_cursor : MonoBehaviour
{
    [SerializeField]RectTransform rect;

    public Vector2 Pos
    {
        set
        {
            rect.position = value;
        }
    }

    public Vector2 AnchoredPos
    {
        set
        {
            rect.anchoredPosition = value;
        }
    }

    [SerializeField] RectTransform leftup;
    [SerializeField] RectTransform rightup;
    [SerializeField] RectTransform leftdown;
    [SerializeField] RectTransform rightdown;

    int wavedelta = 0;
    public int SetDelta
    {
        set
        {
            wavedelta = 11 + value;

            leftup.anchoredPosition = new Vector2Int(-1, 1) * wavedelta;
            rightup.anchoredPosition = new Vector2Int(1, 1) * wavedelta;
            leftdown.anchoredPosition = new Vector2Int(-1, -1) * wavedelta;
            rightdown.anchoredPosition = new Vector2Int(1, -1) * wavedelta;
        }
    }

}
