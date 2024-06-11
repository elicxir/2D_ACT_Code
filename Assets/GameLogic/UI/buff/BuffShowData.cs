using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "MyScriptable/Create BuffShowData")]

public class BuffShowData : ScriptableObject
{
    public BuffImage[] buffImages;


    [System.Serializable]
    public class BuffImage
    {
        public string id;
        public Sprite sprite;
    }
}

