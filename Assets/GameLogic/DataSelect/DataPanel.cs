using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DataTypes.GameData;

public class DataPanel : DS_Panel
{
    [SerializeField] TextMeshProUGUI time;
    [SerializeField] TextMeshProUGUI map;
    [SerializeField] TextMeshProUGUI filename;
    [SerializeField] Image sprite;

   


    public void SetData(FileData data)
    {
        sprite.sprite = data.sprite;
        map.text = data.map.ToString("N2")+"%";
        time.text = data.playtime;
        filename.text = data.filename;
    }

}
