using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MiniMap_Wall : MonoBehaviour
{
    public bool isVisited
    {
        get
        {
            return isVisitedVar;
        }
        set
        {
            image1.enabled = value;
            image2.enabled = value;
            image3.enabled = value;

            isVisitedVar = value;
        }
    }
    bool isVisitedVar = false;











    public RectTransform rect;
    public Image image1;
    public Image image2;
    public Image image3;

    public void SetState(WallData.WallType type,Color color)
    {
        switch (type)
        {
            case WallData.WallType.Wall:
                image1.color = Color.white;
                image2.color = Color.white;
                image3.color = Color.white;
                break;
            case WallData.WallType.Path:
                image1.color = Color.white;
                image2.color = color;
                image3.color = Color.white;

                break;
            case WallData.WallType.None:
                image1.color = color;
                image2.color = color;
                image3.color = color;

                break;
            case WallData.WallType.Breakale:
                break;
            default:
                break;
        }
    }

    public WallData.WallDir dir
    {
        set
        {
            switch (value)
            {
                case WallData.WallDir.Vertical:
                    rect.localEulerAngles = new Vector3(0, 0, 0);
                    break;
                case WallData.WallDir.Horizontal:
                    rect.localEulerAngles = new Vector3(0, 0, 90);

                    break;
                default:
                    break;
            }
        }
    }
}
