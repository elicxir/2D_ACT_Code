using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "MyScriptable/AreaData")]

public class AreaData : ScriptableObject
{
    public string AreaName;
    public Color AreaColor = Color.white;
    public List<SectionData> SectionDatas;

}

[System.Serializable]
public class SectionData
{
    public RectInt Extent;
    public bool isHidden = false;//‰B‚µ•”‰®‚©‚Ç‚¤‚©

    

}
