using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class KeyItems : SubPanelExecuter
{
    [SerializeField] ItemListPanel itempanel;
    [SerializeField] EQ_desc desc;

    bool canselect = true;

    enum Mode
    {
        View,
        Detail,
    }

    Mode nowMode = Mode.View;

    public override IEnumerator Finalizer(gamestate after)
    {
        yield return StartCoroutine(Out(0.3f));

        yield break;
    }

    public override IEnumerator Init(gamestate before)
    {
        nowMode = Mode.View;

        itempanel.IconUpdate(ItemType.KeyItem);
        itempanel.wave_timer = 0;
        itempanel.Wave();

        itempanel.GenerateList(ItemType.KeyItem);

        canselect = true;

        yield return StartCoroutine(In(0.3f));

    }


    public override void Updater()
    {
        if (canselect)
        {
            if (INPUT.ButtonDown(Control.Menu) || INPUT.ButtonDown(Control.Cancel))
            {
                GAME.StateQueue((int)gamestate.Menu);
            }
        }

    }

    void SelectSystem_View()
    {
        if (INPUT.ButtonDown(Control.Decide))
        {
            //ChangeEquipment();
            //IconUpdate();
            //ViewMode();
            DescUpdate();

        }
        else if (INPUT.ButtonDown(Control.Right))
        {
            itempanel.ListRIGHT();
            DescUpdate();
        }
        else if (INPUT.ButtonDown(Control.Left))
        {
            itempanel.ListLEFT();
            DescUpdate();

        }
        else if (INPUT.ButtonDown(Control.Up))
        {
            itempanel.ListUP();
            DescUpdate();

        }
        else if (INPUT.ButtonDown(Control.Down))
        {
            itempanel.ListDOWN();
            DescUpdate();

        }
        if (Define.IM.ButtonDown(Control.Menu) || Define.IM.ButtonDown(Control.Cancel))
        {
            //ViewMode();
        }
    }

    void DescUpdate()
    {
        Sprite sp = null;
        string Title = string.Empty;
        string Content = string.Empty;

        switch (nowMode)
        {
            case Mode.View:
                {
                    KeyItemsData itemData = GM.EDM.GET_KEYITEM_DATA(0);
                    sp = itemData.Sprite;
                    Title = itemData.Itemname;
                    Content = itemData.desc;
                    break;
                }

            case Mode.Detail:


                break;
        }

        desc.Desc_Update(sp, Title, Content);
    }

}
