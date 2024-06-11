using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

//2状態をもち、その状態によって見た目が変わるイベント(宝箱とか)
public class Event_ChangeSprite : Event
{
    public SpriteRenderer spriteRenderer_front;
    public SpriteRenderer spriteRenderer_back;

    [SerializeField] Sprite ActiveSprite_front;
    [SerializeField] Sprite ActiveSprite_back;

    [SerializeField] Sprite DeactiveSprite_front;
    [SerializeField] Sprite DeactiveSprite_back;

    public override string UniqueEventID => name + transform.position;

    public override bool EventFlag()
    {
        bool condition = GM.Game.PlayData.GetGameFlag(UniqueEventID).isTrue;
        return !condition;
    }

    public override void Init()
    {
        base.Init();
        if (EventFlag())
        {
            spriteRenderer_front.sprite = ActiveSprite_front;
            spriteRenderer_back.sprite = ActiveSprite_back;

        }
        else
        {
            spriteRenderer_front.sprite = DeactiveSprite_front;
            spriteRenderer_back.sprite = DeactiveSprite_back;

        }
    }

    [ContextMenu("active")]
    void test1()
    {
        spriteRenderer_front.sprite = ActiveSprite_front;
        spriteRenderer_back.sprite = ActiveSprite_back;

    }
    [ContextMenu("deactive")]
    void test2()
    {
        spriteRenderer_front.sprite = DeactiveSprite_front;
        spriteRenderer_back.sprite = DeactiveSprite_back;

    }
}
