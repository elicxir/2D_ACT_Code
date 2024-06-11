using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;

public class UI_Buff : MonoBehaviour
{
    [SerializeField] Image[] buffimage;
    [SerializeField] BuffShowData BuffShowData;

    [ContextMenu("white")]
    void SetWhite()
    {
        foreach (Image image in buffimage)
        {
            image.rectTransform.sizeDelta = Vector2.one * 12;
            image.color = Color.white;
        }
    }


    Sprite GetSprite(string id, bool positive = true)
    {/*
        foreach (BuffImage data in datas)
        {
            if (data.id == id && data.positive == positive)
            {
                return data.sprite;
            }
        }*/

        return null;
    }

    public void SetBuffGraph()
    {
        foreach (Image data in buffimage)
        {
            data.sprite = null;
        }

        int num = 0;

        foreach (BuffShowData.BuffImage image in BuffShowData.buffImages)
        {
            if (GM.Player.Player.buffManager.CHECK(image.id))
            {
                buffimage[num].sprite = image.sprite;
                num++;
                if (num > 8)
                {
                    break;
                }
            }
        }

        foreach (Image data in buffimage)
        {
            if (data.sprite == null)
            {
                data.color = Color.clear;
            }
            else
            {
                data.color = Color.white;
            }
        }


    }
}

