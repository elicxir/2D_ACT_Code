using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainStatus : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] val;

    public void StatusSet(float HP,float maxHP,float MP,float maxMP,float mpreg,float attack)
    {
        val[0].text = ((int)HP).ToString();
        val[1].text = ((int)maxHP).ToString();

        val[2].text = ((int)MP).ToString();

        val[3].text = ((int)maxMP).ToString();

    }



}
