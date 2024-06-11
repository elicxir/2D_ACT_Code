using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Memo_Title : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image image;

    [SerializeField] Color[] selectedcolor;
    [SerializeField] Color[] notselectedcolor;

    [SerializeField] float ListUpLine;
    [SerializeField] float ListDownLine;
    [SerializeField] float OpacitySpace;


    public bool log = false;

    private void Update()
    {
        if (log)
        {
            print(transform.position);
        }
    }



    public string str;

    private void OnValidate()
    {
        TextUpdate();
    }

    void SetOpacity(float val)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, val);
        image.color = new Color(image.color.r, image.color.g, image.color.b, val);

    }

    public void OpacityUpdate()
    {
        float y = transform.position.y;
        
        if (y > ListUpLine + OpacitySpace||y<ListDownLine-OpacitySpace)
        {
            SetOpacity(0);
        }
        else if (y > ListUpLine)
        {
            SetOpacity(1-(y-ListUpLine) / OpacitySpace);
        }
        else if (y < ListDownLine)
        {
            SetOpacity(1-(ListDownLine-y) / OpacitySpace);
        }
        else
        {
            SetOpacity(1);
        }
        
        //transform.position
    }

    public void TextUpdate()
    {
        text.text = str;
    }

    public RectTransform rectTransform;

    public bool Selected
    {
        set
        {
            if (value)
            {
                text.color = selectedcolor[0];
                image.color = selectedcolor[1];

            }
            else
            {
                text.color = notselectedcolor[0];
                image.color = notselectedcolor[1];
            }
        }
    }
}
