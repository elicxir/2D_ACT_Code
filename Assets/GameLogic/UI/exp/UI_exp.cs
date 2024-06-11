using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_exp : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    public void EXP_Value(int val)
    {
        text.text = val.ToString();
    } 

}
