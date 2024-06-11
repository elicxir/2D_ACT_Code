using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ST_Item_Volume : ST_item
{
    public int Value
    {
        set
        {
            v = (int)Mathf.Clamp(value, 0, 100);
        }
    }

    int v=0;

    [SerializeField] Image volume;
    [SerializeField] TextMeshProUGUI val;

    public void DataUpdate()
    {
        volume.fillAmount = (float)v / 100;
        val.text = v.ToString() + "%";
    }


}
