using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Memo_Tab : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image image;

    [SerializeField] Color[] selectedcolor;
    [SerializeField] Color[] notselectedcolor;

    public bool Selected
    {
        set
        {
            if (value)
            {
                text.color = selectedcolor[0];
                image.color = selectedcolor[1];

            }
            else
            {
                text.color = notselectedcolor[0];
                image.color = notselectedcolor[1];
            }
        }
    }


    
}
