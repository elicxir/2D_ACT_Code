using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// b’è“I‚É…•½•ûŒü‚Ì‚İ
/// </summary>
public class BG_scroll : MonoBehaviour
{
    [SerializeField] Sprite sprite;
    [SerializeField] BG_sprite[] BG;

    public float scrollspeed=20;

    [SerializeField] GameObject Scroller;

    int width
    {
        get
        {
            return Mathf.RoundToInt(sprite.bounds.size.x);
        }

    }

    private void OnValidate()
    {
        set();
    }

    void set()
    {
        BG[0].SetSprite(sprite);
        BG[1].SetSprite(sprite);
        BG[2].SetSprite(sprite);

        BG[0].transform.localPosition = new Vector2(-width, 0);
        BG[1].transform.localPosition = new Vector2(0, 0);
        BG[2].transform.localPosition = new Vector2(width, 0);
    }

    private void Update()
    {
        Scroller.transform.localPosition += new Vector3(scrollspeed, 0) * Time.deltaTime;

        if (Scroller.transform.localPosition.x > width)
        {
            Scroller.transform.localPosition += Vector3.left * width;
        }
        else if (Scroller.transform.localPosition.x < -width)
        {

            Scroller.transform.localPosition += Vector3.right * width;
        }
    }



}
