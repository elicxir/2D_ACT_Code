using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EQ_icon : MonoBehaviour
{
    [SerializeField] Image[] image;

    public Vector2 GetPos(int index)
    {
        return image[index].rectTransform.position;
    }

    public void IconUpdate(Sprite[] sprites)
    {
        for(int i=0;i<11;i++)
        {
            image[i].sprite = sprites[i];
        }
    }
}
