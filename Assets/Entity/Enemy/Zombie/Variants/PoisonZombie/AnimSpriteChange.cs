using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimSpriteChange : MonoBehaviour
{
    [SerializeField] ReplaceInfo[] replaceInfo;
    [SerializeField]SpriteRenderer sr;

    void LateUpdate()
    {
        
        sprChange();
    }

    void sprChange()
    {


        string sprName = sr.sprite.name;

        int pos = sprName.LastIndexOf('_');
        if (pos >= 0)
        {
            string pre = sprName.Substring(0, pos);
            int index;
            try
            {
                index = int.Parse(sprName.Substring(pos + 2, sprName.Length));
            }
            catch (FormatException)
            {
                return;
            }
            
            foreach (ReplaceInfo nfo in replaceInfo)
            {
                if (nfo.fromName == pre)
                {
                    sr.sprite = nfo.sprite[index];

                    break;
                }
            }
        }
    }
}


[System.Serializable]
public class ReplaceInfo
{
    public Sprite[] sprite;
    public string fromName;
}