using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ST_item_text : ST_item
{
    [SerializeField] TextMeshProUGUI val;

    public string Value;
    public void DataUpdate()
    {
        val.text = Value;
    }
}
