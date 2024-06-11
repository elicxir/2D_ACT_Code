using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingBlock : Gimmick
{

    public override int UpdateRange
    {
        get
        {
            return 12;
        }
    }

    [SerializeField] float breakTime = 2;

    float T
    {
        get
        {
            return breakTime / pattern.Length;
        }
    }

    [SerializeField] Sprite[] pattern;

    [SerializeField] SpriteRenderer spriteRenderer1;

    Vector2 Pos
    {
        get
        {
            return transform.position;
        }
    }

    Vector2 LeftUp
    {
        get
        {
            return Pos + Vector2.left * (8 - 0.5f) + Vector2.up * (8 - 0.5f);
        }
    }
    Vector2 RightUp
    {
        get
        {
            return Pos + Vector2.right * (8 - 0.5f) + Vector2.up * (8 - 0.5f);
        }
    }

    bool isOnPlayer()
    {
        Vector2 start;
        Vector2 end;


        start = LeftUp + Vector2.up;
        end = RightUp + Vector2.up;

        RaycastHit2D collider = Physics2D.Linecast(start, end, PlayerLayer);
        return collider;
    }





    float Timer = 0;

    int num = 0;


    public override bool Updater()
    {
        if (T <= Timer)
        {
            spriteRenderer1.enabled = true;
            spriteRenderer1.sprite = pattern[num];

            num++;

            Timer = 0;


            if (num >= pattern.Length)
            {
                gameObject.SetActive(false);
                return true;
            }

        }



        if (isOnPlayer())
        {
            Timer += Time.deltaTime;
        }



        return false;
    }

    private void LateUpdate()
    {
    }

    public override void Init()
    {
        spriteRenderer1.enabled = false;
        Timer = 0;
        num = 0;
    }



}
