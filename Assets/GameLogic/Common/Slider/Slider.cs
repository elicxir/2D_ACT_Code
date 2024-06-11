using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Slider : MonoBehaviour
{
    [SerializeField] Image nob;
    [SerializeField] RectTransform rect;


    [SerializeField][Range(1,6)] int width;

    /// <summary>
    /// 縦に並ぶリストの数
    /// </summary>
    public int vertical;

    /// <summary>
    /// リストの総数
    /// </summary>
    public int allnum;


    private void OnValidate()
    {
        REFRESH();
    }




    int allnum2
    {
        get
        {
            return Mathf.Max(vertical, allnum);
        }
    }


    public int cell;

    int num {
        get
        {
            if (cell < 0)
            {
                return 0;
            }
            else if (cell > allnum2 - vertical)
            {
                return allnum2 - vertical;
            }
            else
            {
                return cell;
            }

           
        }
    }

    float onecell
    {
        get
        {
            return (float)180/ allnum2;

        }
    }

    int top
    {
        get
        {
            return Mathf.Min(allnum2 - vertical, num);      
        }
    }

    int bottom
    {
        get
        {
            return Mathf.Min(allnum2 - vertical, allnum2 -(num+ vertical));

        }
    }

    private void Update()
    {
        REFRESH();
    }

    // Update is called once per frame
    void REFRESH()
    {
        rect.offsetMax = new Vector2(width, -onecell*top);
        rect.offsetMin = new Vector2(-width, onecell * bottom);
    }
}





/*
 public class Slider : MonoBehaviour
{
    [SerializeField] Image nob;
    [SerializeField] RectTransform rect;

    public int vertical;

    public int allnum;


    private void OnValidate()
    {
        REFRESH();
    }




    int allnum2
    {
        get
        {
            return Mathf.Max(4, allnum);
        }
    }


    public int cell;

    int num {
        get
        {
            if (cell < 0)
            {
                return 0;
            }
            else if (cell > allnum2 - 4)
            {
                return allnum2 - 4;
            }
            else
            {
                return cell;
            }

           
        }
    }

    float onecell
    {
        get
        {
            return 180/ allnum2;

        }
    }

    int top
    {
        get
        {
            return Mathf.Min(allnum2 - 4,num);      
        }
    }

    int bottom
    {
        get
        {
            return Mathf.Min(allnum2 - 4, allnum2 - 1-(num+3));

        }
    }

    private void Update()
    {
        REFRESH();
    }

    // Update is called once per frame
    void REFRESH()
    {
        rect.offsetMax = new Vector2(6, -onecell*top);
        rect.offsetMin = new Vector2(-6, onecell * bottom);
    }
}

 */