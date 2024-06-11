using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Bar : MonoBehaviour
{
    [SerializeField] Image bar;

    public float fillamount
    {
        set
        {
            bar.fillAmount = value;
        }
    }

}
