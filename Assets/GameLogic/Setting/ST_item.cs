using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ST_item : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Image image;
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

    public bool selected
    {
        get
        {
            return selected_var;
        }
        set
        {
            selected_var = value;
            if (value)
            {
                image.color = Color.gray;

            }
            else
            {
                image.color = Color.green;
            }
        }

    }
    bool selected_var = false;


    [SerializeField] string itemname;
    [SerializeField] TextMeshProUGUI TMP_name;

    private void OnValidate()
    {
        TMP_name.text = itemname;
    }

}
