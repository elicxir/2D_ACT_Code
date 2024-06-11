using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Cursor : MonoBehaviour
{
    [SerializeField] RectTransform rect;

    [SerializeField] RectTransform leftup;
    [SerializeField] RectTransform rightup;
    [SerializeField] RectTransform leftdown;
    [SerializeField] RectTransform rightdown;

    int wavedelta = 0;
    public int SetDelta
    {
        set
        {
            wavedelta = 15+ value;

            leftup.anchoredPosition = new Vector2Int(-1, 1) * wavedelta;
            rightup.anchoredPosition = new Vector2Int(1, 1) * wavedelta;
            leftdown.anchoredPosition = new Vector2Int(-1, -1) * wavedelta;
            rightdown.anchoredPosition = new Vector2Int(1, -1) * wavedelta;
        }
    }

    public Vector2 Pos
    {
        set
        {
            rect.anchoredPosition = new Vector2Int(Mathf.RoundToInt(value.x), Mathf.RoundToInt(value.y));
        }
    }
}
