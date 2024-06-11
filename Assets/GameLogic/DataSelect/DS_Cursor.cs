using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DS_Cursor : MonoBehaviour
{
    [SerializeField] RectTransform rect;

    [SerializeField] RectTransform left;
    [SerializeField] RectTransform right;
    [SerializeField] RectTransform center;

    [SerializeField][Range(20,200)] int delta = 20;

    private void OnValidate()
    {
        left.anchoredPosition = Vector2Int.left * delta;
        right.anchoredPosition = Vector2Int.right * delta;
    }

    public Vector2 Pos
    {
        set
        {
            rect.anchoredPosition = new Vector2Int(Mathf.RoundToInt(value.x), Mathf.RoundToInt(value.y));
        }
    }

    public int Haba
    {
        set
        {
            center.sizeDelta = new Vector2(value, 26);
        }
    }

    int cursor_width = 120;

    public int Width
    {
        set
        {
            cursor_width = value;
        }
    }

    int wavedelta = 0;
    public int SetDelta {
        set
        {
            wavedelta = cursor_width + value;

            left.anchoredPosition = Vector2Int.left * wavedelta;
            right.anchoredPosition = Vector2Int.right * wavedelta;
        }
    }



}
