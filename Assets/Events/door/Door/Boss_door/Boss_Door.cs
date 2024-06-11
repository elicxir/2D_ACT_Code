using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class Boss_Door : Gimmick
{
    public string DoorID
    {
        get
        {
            return "BossDoor" + transform.position;
        }
    }

    public override int UpdateRange => 50;
    public override void Init()
    {
        Hitbox.enabled = !isOpened;

        if (isOpened)
        {
            GraphUpdate(80);
        }
        else
        {
            GraphUpdate(0);
        }
    }

    public int delta2 = 0;

    public GameObject Door;

    public Sprite SetSprite
    {
        set{
            foreach (SpriteRenderer rende in spriteRenderer)
            {
                rende.sprite = value;
            }
        }

    
    }


    public BoxCollider2D Hitbox;

    [SerializeField] SpriteRenderer[] spriteRenderer;


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

        Door.transform.localPosition = Vector2.up * delta;

        return 0;
    }



}
