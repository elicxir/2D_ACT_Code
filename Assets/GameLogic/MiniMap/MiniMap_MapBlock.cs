using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniMap_MapBlock : MonoBehaviour
{
    public bool isVisited
    {
        get
        {
            return isVisitedVar;
        }
        set
        {
            image.enabled = value;

            isVisitedVar = value;
        }
    }
    bool isVisitedVar = false;

    public RectTransform rect;
    [SerializeField] Image image;
    public Color colorChange
    {
        set
        {
            image.color = value;
        }
    }

    /// <summary>
    /// Normal:�ʏ�̃Z��
    /// Hide:�B���G���A�̃Z��
    /// Save:�Z�[�u�|�C���g������
    /// </summary>
    public enum CellType
    {
        Normal,Hide,Save
    }

    public enum BlockState
    {
        Hide,Informed,Visited
    }

    public enum WallState
    {
        None,Path,Wall
    }





}
