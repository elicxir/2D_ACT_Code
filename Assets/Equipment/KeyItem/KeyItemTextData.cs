using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create KeyItemTextData")]

public class KeyItemTextData : ScriptableObject
{
    public string Itemname;
    [TextArea] public string desc;
}
