using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
using DataTypes.GameData;

public class Equipment : SubPanelExecuter
{
    [SerializeField] EQ_icon icon;
    [SerializeField] EQ_desc desc;
    [SerializeField] EQ_cursor cursor;

    [SerializeField] ItemListPanel itempanel;

    ItemType type(int num)
    {
        if (num >= 0 && num < 3)
        {
            return ItemType.Consumable;
        }
        else if (num < 7)
        {
            return ItemType.Accessory;
        }
        else if (num < 11)
        {
            return ItemType.Spell;
        }
        return ItemType.Consumable;
    }

    enum Mode
    {
        View,
        Select,
        Detail,
    }

    Mode nowMode = Mode.View;

    public enum Selected
    {
        Consumable1,
        Consumable2,
        Consumable3,

        Accessory1,
        Accessory2,
        Accessory3,
        Accessory4,

        Spell1,
        Spell2,
        Spell3,
        Spell4,

    }
    Selected nowSelect = Selected.Consumable1;

    public override IEnumerator Finalizer(gamestate after)
    {
        //EquipData equipData = new EquipData { duration = duration, consumableID = consumableID, spellID = spellID, accessoryID = accessoryID };

        //GM.Game.PlayData.equipData = equipData;

        GM.Player.Player.SetEquipmentData();

        yield return StartCoroutine(Out(0.3f));

        yield break;
    }

    public override IEnumerator Init(gamestate before)
    {
        nowMode = Mode.View;
        nowSelect = Selected.Consumable1;

        consumableID = GM.Game.PlayData.equipData.consumableID;

        spellID = GM.Game.PlayData.equipData.spellID;
        accessoryID = GM.Game.PlayData.equipData.accessoryID;

        IconUpdate();
        DescUpdate();

        itempanel.wave_timer = 0;
        wave_timer = 0;

        Wave();
        itempanel.Wave();

        cursor.Pos = icon.GetPos((int)nowSelect);

        canselect = true;

        yield return StartCoroutine(In(0.3f));

    }

    public override void Updater()
    {
        Wave();
        itempanel.Wave();
        if (canselect)
        {
            switch (nowMode)
            {
                case Mode.View:
                    SelectSystem_View();
                    break;


                case Mode.Select:
                    SelectSystem_Select();
                    switch (nowSelect)
                    {
                        case Selected.Consumable1:
                        case Selected.Consumable2:
                        case Selected.Consumable3:
                            itempanel.IconUpdate(ItemType.Consumable); break;

                        case Selected.Accessory1:
                        case Selected.Accessory2:
                        case Selected.Accessory3:
                        case Selected.Accessory4:
                            itempanel.IconUpdate(ItemType.Accessory); break;

                        case Selected.Spell1:
                        case Selected.Spell2:
                        case Selected.Spell3:
                        case Selected.Spell4:
                            itempanel.IconUpdate(ItemType.Spell); break;
                    }

                    itempanel.PosUpdate();

                    break;
                case Mode.Detail:
                    break;
                default:
                    break;
            }


        }

    }

    bool canselect = true;
    float MoveTime = 0.12f;

    [SerializeField] AnimationCurve curve;

    IEnumerator MoveCursor(Selected from, Selected to)
    {
        canselect = false;

        bool updateflag = true;

        Vector2 pos1 = icon.GetPos((int)from);
        Vector2 pos2 = icon.GetPos((int)to);

        float timer = 0;

        while (MoveTime > timer)
        {
            timer += Time.deltaTime;

            timer = Mathf.Clamp(timer, 0, MoveTime);
            float progress = curve.Evaluate(timer / MoveTime);

            Vector2 p = Vector2.Lerp(pos1, pos2, progress);

            cursor.Pos = p;

            if (progress > 0.5f && updateflag)
            {
                DescUpdate();
                updateflag = false;
            }

            yield return null;

        }
        canselect = true;
    }

    void UP()
    {
        Selected val = Selected.Accessory1;

        switch (nowSelect)
        {
            case Selected.Consumable1:
                val = Selected.Accessory1;
                break;
            case Selected.Consumable2:
                val = Selected.Accessory2;

                break;
            case Selected.Consumable3:
                val = Selected.Accessory4;

                break;
            case Selected.Accessory1:
                val = Selected.Consumable1;

                break;
            case Selected.Accessory2:
                val = Selected.Consumable2;

                break;
            case Selected.Accessory3:
                val = Selected.Consumable3;

                break;
            case Selected.Accessory4:
                val = Selected.Consumable3;

                break;
            case Selected.Spell1:
                val = Selected.Spell3;
                break;
            case Selected.Spell2:
                val = Selected.Spell4;

                break;
            case Selected.Spell3:
                val = Selected.Spell1;

                break;
            case Selected.Spell4:
                val = Selected.Spell2;
                break;
        }

        StartCoroutine(MoveCursor(nowSelect, val));
        nowSelect = val;
    }
    void DOWN()
    {
        Selected val = Selected.Accessory1;

        switch (nowSelect)
        {
            case Selected.Consumable1:
                val = Selected.Accessory1;
                break;
            case Selected.Consumable2:
                val = Selected.Accessory2;

                break;
            case Selected.Consumable3:
                val = Selected.Accessory4;

                break;
            case Selected.Accessory1:
                val = Selected.Consumable1;

                break;
            case Selected.Accessory2:
                val = Selected.Consumable2;

                break;
            case Selected.Accessory3:
                val = Selected.Consumable3;

                break;
            case Selected.Accessory4:
                val = Selected.Consumable3;

                break;
            case Selected.Spell1:
                val = Selected.Spell3;
                break;
            case Selected.Spell2:
                val = Selected.Spell4;

                break;
            case Selected.Spell3:
                val = Selected.Spell1;

                break;
            case Selected.Spell4:
                val = Selected.Spell2;
                break;
        }

        StartCoroutine(MoveCursor(nowSelect, val));
        nowSelect = val;
    }
    void LEFT()
    {
        Selected val = Selected.Accessory1;

        switch (nowSelect)
        {
            case Selected.Consumable1:
                val = Selected.Spell2;
                break;
            case Selected.Consumable2:
                val = Selected.Consumable1;

                break;
            case Selected.Consumable3:
                val = Selected.Consumable2;

                break;
            case Selected.Accessory1:
                val = Selected.Spell4;

                break;
            case Selected.Accessory2:
                val = Selected.Accessory1;

                break;
            case Selected.Accessory3:
                val = Selected.Accessory2;

                break;
            case Selected.Accessory4:
                val = Selected.Accessory3;

                break;
            case Selected.Spell1:
                val = Selected.Consumable3;
                break;
            case Selected.Spell2:
                val = Selected.Spell1;

                break;
            case Selected.Spell3:
                val = Selected.Accessory4;

                break;
            case Selected.Spell4:
                val = Selected.Spell3;
                break;
        }

        StartCoroutine(MoveCursor(nowSelect, val));
        nowSelect = val;
    }
    void RIGHT()
    {
        Selected val = Selected.Accessory1;

        switch (nowSelect)
        {
            case Selected.Consumable1:
                val = Selected.Consumable2;
                break;
            case Selected.Consumable2:
                val = Selected.Consumable3;

                break;
            case Selected.Consumable3:
                val = Selected.Spell1;

                break;
            case Selected.Accessory1:
                val = Selected.Accessory2;

                break;
            case Selected.Accessory2:
                val = Selected.Accessory3;

                break;
            case Selected.Accessory3:
                val = Selected.Accessory4;

                break;
            case Selected.Accessory4:
                val = Selected.Spell3;

                break;
            case Selected.Spell1:
                val = Selected.Spell2;
                break;
            case Selected.Spell2:
                val = Selected.Consumable1;

                break;
            case Selected.Spell3:
                val = Selected.Spell4;

                break;
            case Selected.Spell4:
                val = Selected.Accessory1;
                break;
        }

        StartCoroutine(MoveCursor(nowSelect, val));
        nowSelect = val;
    }




    void SelectMode()
    {
        switch (nowSelect)
        {
            case Selected.Consumable1:
            case Selected.Consumable2:
            case Selected.Consumable3:
                itempanel.GenerateList(ItemType.Consumable, consumableID[(int)nowSelect], consumableID);
                break;

            case Selected.Accessory1:
            case Selected.Accessory2:
            case Selected.Accessory3:
            case Selected.Accessory4:
                itempanel.GenerateList(ItemType.Accessory, accessoryID[(int)nowSelect - 3], accessoryID);
                break;


            case Selected.Spell1:
            case Selected.Spell2:
            case Selected.Spell3:
            case Selected.Spell4:
                itempanel.GenerateList(ItemType.Spell, spellID[(int)nowSelect - 7], spellID);

                break;
        }

        itempanel.Show();
        nowMode = Mode.Select;
    }

    void ViewMode()
    {
        itempanel.Hide();
        nowMode = Mode.View;
    }

    void SelectSystem_View()
    {
        if (INPUT.ButtonDown(Control.Decide))
        {
            SelectMode();
        }
        else if (INPUT.ButtonDown(Control.Right))
        {
            RIGHT();
        }
        else if (INPUT.ButtonDown(Control.Left))
        {
            LEFT();
        }
        else if (INPUT.ButtonDown(Control.Up))
        {
            UP();
        }
        else if (INPUT.ButtonDown(Control.Down))
        {
            DOWN();
        }

        else if (Define.IM.ButtonDown(Control.Menu) || Define.IM.ButtonDown(Control.Cancel))
        {
            GAME.StateQueue((int)gamestate.Menu);
        }
    }

    void SelectSystem_Select()
    {
        if (INPUT.ButtonDown(Control.Decide))
        {
            ChangeEquipment();
            IconUpdate();
            ViewMode();
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
            ViewMode();
        }
    }


    float wave_timer = 0;
    void Wave()
    {
        wave_timer += Time.deltaTime * 11;
        cursor.SetDelta = Mathf.RoundToInt(Mathf.Sin(wave_timer));
    }






    void IconUpdate()
    {
        Sprite[] sprite = new Sprite[11];

        for (int i = 0; i < 3; i++)
        {
            sprite[i] = GM.EDM.GET_CONSUMABLE_FUNCTION(consumableID[i]).Sprite;
        }

        for (int i = 0; i < 4; i++)
        {
            sprite[i + 3] = GM.EDM.GET_ACCESSORY_FUNCTION(accessoryID[i]).Sprite;
        }

        for (int i = 0; i < 4; i++)
        {
            sprite[i + 7] = GM.EDM.GET_SPELL_FUNCTION(spellID[i]).Sprite;
        }

        icon.IconUpdate(sprite);
    }




    public void ChangeEquipment()
    {
        switch (nowSelect)
        {
            case Selected.Consumable1:
                consumableID[0] = itempanel.GetSelectedIndex;
                break;
            case Selected.Consumable2:
                consumableID[1] = itempanel.GetSelectedIndex;

                break;
            case Selected.Consumable3:
                consumableID[2] = itempanel.GetSelectedIndex;

                break;
            case Selected.Accessory1:
                accessoryID[0] = itempanel.GetSelectedIndex;
                break;
            case Selected.Accessory2:
                accessoryID[1] = itempanel.GetSelectedIndex;

                break;
            case Selected.Accessory3:
                accessoryID[2] = itempanel.GetSelectedIndex;

                break;
            case Selected.Accessory4:
                accessoryID[3] = itempanel.GetSelectedIndex;

                break;
            case Selected.Spell1:
                spellID[0] = itempanel.GetSelectedIndex;
                break;
            case Selected.Spell2:
                spellID[1] = itempanel.GetSelectedIndex;

                break;
            case Selected.Spell3:
                spellID[2] = itempanel.GetSelectedIndex;

                break;
            case Selected.Spell4:
                spellID[3] = itempanel.GetSelectedIndex;

                break;
            default:
                break;
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
                    int selectnum = (int)nowSelect;

                    switch (type(selectnum))
                    {
                        case ItemType.Consumable:
                            ConsumableFunction consumableData = GM.EDM.GET_CONSUMABLE_FUNCTION(consumableID[selectnum]);
                            sp = consumableData.Sprite;
                            Title = consumableData.consumablename;
                            Content = consumableData.description;
                            break;

                        case ItemType.Accessory:
                            AccessoryFunction accessoryData = GM.EDM.GET_ACCESSORY_FUNCTION(accessoryID[selectnum - 3]);
                            sp = accessoryData.Sprite;
                            Title = accessoryData.accessoryname;
                            Content = accessoryData.description;
                            break;

                        case ItemType.Spell:
                            SpellFunction spellData = GM.EDM.GET_SPELL_FUNCTION(spellID[selectnum - 7]);
                            sp = spellData.Sprite;
                            Title = spellData.spellname;
                            Content = spellData.description;
                            break;
                    }
                }
                break;

            case Mode.Select:
                {
                    switch (type((int)nowSelect))
                    {
                        case ItemType.Consumable:
                            ConsumableFunction consumableData = GM.EDM.GET_CONSUMABLE_FUNCTION(itempanel.GetSelectedIndex);
                            sp = consumableData.Sprite;
                            Title = consumableData.consumablename;
                            Content = consumableData.description;
                            break;

                        case ItemType.Accessory:
                            AccessoryFunction accessoryData = GM.EDM.GET_ACCESSORY_FUNCTION(itempanel.GetSelectedIndex);
                            sp = accessoryData.Sprite;
                            Title = accessoryData.accessoryname;
                            Content = accessoryData.description;
                            break;

                        case ItemType.Spell:
                            SpellFunction spellData = GM.EDM.GET_SPELL_FUNCTION(itempanel.GetSelectedIndex);
                            sp = spellData.Sprite;
                            Title = spellData.spellname;
                            Content = spellData.description;
                            break;
                    }



                }

                break;

            case Mode.Detail:


                break;
        }

        desc.Desc_Update(sp, Title, Content);
    }

    int[] duration = { 0, 0, 0 };
    int[] consumableID = {-1,-1,-1};

    int[] spellID = {-1,-1,-1,-1};
    int[] accessoryID = {-1,-1,-1,-1};
}













/*
 






    void TextUpdate()
    {/*
        if (cursorpos >= 5)
        {
            AccessoryTextUpdate(accessoryDatas[cursorpos - 5]);
        }
        else if (cursorpos >= 3)
        {
            SpellTextUpdate(spellDatas[cursorpos - 3]);
        }
        else
        {
            ItemTextUpdate(consumableDatas[cursorpos], duration[cursorpos]);
        }

        for (int q = 0; q < 8; q++)
        {
            texts[q].text = strings[q];
        }
    }

    void ItemTextUpdate(ConsumableFunction data, int count)
{
    equipname.text = data.consumablename;

    counter.text = count.ToString() + "/" + data.duration.ToString();

    strings[0] = "[Consumables]";
    strings[1] = data.description1;

    strings[3] = data.flavor1;
    strings[4] = data.flavor2;
    strings[5] = data.flavor3;
    strings[6] = data.flavor4;
    strings[7] = data.flavor5;


}

void SpellTextUpdate(SpellFunction data)
{
    equipname.text = data.spellname;

    switch (data.spellType)
    {
        case SpellType.Trigger:
            strings[0] = "[TriggerSpell]";
            counter.text = "MP:" + data.MP.ToString();

            break;

        case SpellType.Switch:
            strings[0] = "[SwitchSpell]";
            counter.text = "MP:" + data.MP.ToString() + "/sec";

            break;

        case SpellType.Increase:
            strings[0] = "[IncreaseSpell]";
            counter.text = "MP:" + data.MP.ToString() + "-" + (data.EmpoweredMP + data.MP).ToString();

            break;
    }

    strings[1] = data.description;

    strings[2] = "";

    strings[3] = "";

    strings[4] = "";
    strings[5] = "";


    strings[6] = data.flavor1;
    strings[7] = data.flavor2;


}



void AccessoryTextUpdate(AccessoryFunction data)
{
    equipname.text = data.accessoryname;

    counter.text = "";

    strings[0] = "[PassiveEffect]";
    strings[1] = data.description;

    strings[2] = "";

    strings[3] = data.flavor1;

    strings[4] = data.flavor2;
    strings[5] = data.flavor3;

    strings[6] = data.flavor4;
    strings[7] = "";

}



void MoveCursor()
{/*
        //→
        if (Define.IM.ButtonDown(Control.Right))
        {
            cursorvar++;
            DataUpdate();

        }
        //←
        else if (Define.IM.ButtonDown(Control.Left))
        {
            cursorvar--;
            DataUpdate();
        }
        

    //cursorimage.position = Pos[cursorpos].position;
}

public void EnterMenu()
{/*
        IDUpdate();
        cursorvar = 700;
        DataUpdate();
        //Define.GM.GameState = gamestate.Equipments;

        
}

public void ExitMenu()
{

    //Define.GM.GameState = pre;

}

*/