using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Status/Create Status_Desc_Data")]

public class Status_Desc_Data : ScriptableObject
{
    public string statsName;

    [TextArea]
    public string desc;
}
