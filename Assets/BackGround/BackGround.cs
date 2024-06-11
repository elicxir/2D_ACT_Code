using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;


public class BackGround : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;

    private void LateUpdate()
    {
        sr.transform.localPosition += Vector3.right* Time.deltaTime * 5;
    }
}

public class BG_Layer
{
    public Sprite sprite;

    public ScrollMode VerticalMode;
    public ScrollMode HorizonalMode;

}

public enum ScrollMode
{
    Fixed,//å≈íË
    Vertical,//âÊëúÇÃ
    Vertical_Loop,
    Horizontal,
    Horizontal_Loop,//êÖïΩå¸Ç´Å@ÉãÅ[Év
}



/*
 [SerializeField] Transform bottomMarker;
    [SerializeField] Transform topMarker;


    [SerializeField]
    int bottom
    {
        get
        {
            return Mathf.RoundToInt(bottomMarker.position.y);
        }
    }
    int top
    {
        get
        {
            return Mathf.RoundToInt(topMarker.position.y);
        }
    }

    [SerializeField] int bottom_space;
    [SerializeField] int top_space;

    [SerializeField] int height;

    [SerializeField] GameObject Scroller;




    Vector2 getcamera
    {
        get
        {
            return GM.Game.Camera.Position;
        }
    }

    float ratio
    {
        get
        {
            return Mathf.Clamp01((float)(getcamera.y - bottom) / (top - bottom));
        }
    }

    Vector2 delta;

    private void LateUpdate()
    {
        delta = new Vector2(0,bottom_space+(top_space-bottom_space)*ratio);
        Scroller.transform.position = getcamera+delta;
    }
 */