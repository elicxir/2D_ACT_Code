using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class Door : Gimmick
{
    public override int UpdateRange => 80;
    public string DoorID
    {
        get
        {
            return "Door" + transform.position;
        }
    }

    public int Height
    {
        get
        {
            return (int)Hitbox.size.y;
        }
    }


    public override void Init()
    {
        Hitbox.enabled = !isOpened;

        if (Hitbox.enabled)
        {
            Door_UP.transform.localPosition = Vector2.zero;
            Door_DOWN.transform.localPosition = Vector2.zero;
        }
        else
        {

        }
    }


    public int delta2 = 0;

    enum Dir
    {
        Vertical, Horizontal
    }

    [SerializeField] Dir dir = Dir.Vertical;

    public GameObject Door_UP;
    public GameObject Door_DOWN;

    public SpriteRenderer up_sr;
    public SpriteRenderer down_sr;


    [SerializeField] BoxCollider2D Hitbox;

    public bool isOpened
    {
        set
        {

                Hitbox.enabled = !value;
                GameFlag gameFlag = new GameFlag
                {
                    FlagID = DoorID,
                    isTrue = value
                };
                GM.Game.PlayData.SetGameFlag(gameFlag);
        }
        get
        {
            GameFlag gameFlag = GM.Game.PlayData.GetGameFlag(DoorID);
            return gameFlag.isTrue;

        }
    }

    bool f;
    public override bool Updater()
    {
        bool res = f == Hitbox.enabled;
        f = Hitbox.enabled;

        return res;
    }


    private void OnValidate()
    {
        GraphUpdate(delta2);
    }

    public int GraphUpdate(int delta)
    {
        delta2 = delta;
        switch (dir)
        {
            case Dir.Vertical:
                Door_UP.transform.localPosition = Vector2.up * delta;
                Door_DOWN.transform.localPosition = Vector2.down * delta;
                break;

            case Dir.Horizontal:
                Door_UP.transform.localPosition = Vector2.right * delta;
                Door_DOWN.transform.localPosition = Vector2.left * delta;
                break;

            default:
                break;
        }

        return 0;
    }

}
