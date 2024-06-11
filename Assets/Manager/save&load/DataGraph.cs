using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataGraph : MonoBehaviour
{

    [SerializeField] Text placename;
    [SerializeField] Text time;

    [SerializeField] Image mark;

    [SerializeField] Image Panel;

    [SerializeField] Image badge1;
    [SerializeField] Image badge2;
    [SerializeField] Image badge3;
    [SerializeField] Image badge4;
    [SerializeField] Image badge5;


    public void STR(string n1)
    {
        placename.text = n1;
    } 

    public void TIMESTR(int h,int m,int s )
    {
        time.text = h.ToString("##0") + ":" + m.ToString("00")+","+ s.ToString("00");
    }

    public void COLOR(Color c) {
        Panel.color =c;
    }
}
