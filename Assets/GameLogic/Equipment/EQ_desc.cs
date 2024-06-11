using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EQ_desc : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI content;
    [SerializeField] Image image;

    public void Desc_Update(Sprite sprite, string Title,string Content)
    {
        image.sprite = sprite;
        title.text = Title;
        content.text = Content;
    }


}
