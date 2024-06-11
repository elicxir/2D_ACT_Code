using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DataTypes.GameData;
using Managers;


/// <summary>
/// 設定項目は
/// マスターボリューム
/// 効果音ボリューム
/// BGMボリューム
/// 画面モード設定
/// キーボード操作設定
/// コントローラー設定
/// 言語設定
/// 初期値
/// </summary>


public class Setting : SubPanelExecuter
{
    ConfigData configData;

    [SerializeField] ST_item[] Items;

    [SerializeField] Text[] value = new Text[6];

    [SerializeField] Color selected;
    [SerializeField] Color notselected;

    

    enum Selected
    {
        MasterVol,
        BGMvol,
        SEvol,
        Resolution,
        KeyBoard,
        GamePad,
        Language,
        Default,
        Exit,
    }

    Selected nowSelect = Selected.MasterVol;

    void OnSelectUpdate()
    {
        

        for(int i = 0; i < Items.Length; i++)
        {
            if (i == (int)nowSelect)
            {
                Items[i].selected = true;
            }
            else
            {
                Items[i].selected = false;
            }
        }
    }




    public override void Updater()
    {
        SelectSystem();
        SelectSystem2();
    }

    public override IEnumerator Finalizer(gamestate after)
    {
        GM.Game.ConfigData = configData;
        yield return StartCoroutine(Out(0.3f));
    }

    public override IEnumerator Init(gamestate before)
    {
        configData = GM.Game.ConfigData;
        OnSelectUpdate();
        Value_Update();

        yield return StartCoroutine(In(0.3f));

    }


    void SelectSystem()
    {
        if (GM.Inputs.ButtonDown(Control.Up))
        {
            UP();
        }
        else if (GM.Inputs.ButtonDown(Control.Down))
        {
            DOWN();
        }
    }
    void UP()
    {
        nowSelect = (Selected)(((int)nowSelect + Enum.GetNames(typeof(Selected)).Length - 1) % Enum.GetNames(typeof(Selected)).Length);
        OnSelectUpdate();
    }
    void DOWN()
    {
        nowSelect = (Selected)(((int)nowSelect + 1) % Enum.GetNames(typeof(Selected)).Length);
        OnSelectUpdate();
    }


    void SelectSystem2()
    {
        if (GM.Inputs.ButtonDown(Control.Decide))
        {
            DECIDE();
        }
        else if (GM.Inputs.ButtonDown(Control.Left))
        {
            LEFT();
        }
        else if (GM.Inputs.ButtonDown(Control.Right))
        {
            RIGHT();
        }

        configData.Validate();
        Value_Update();
    }

    void Value_Update()
    {
        ST_Item_Volume M_vol = (ST_Item_Volume)Items[0];
        ST_Item_Volume BGM_vol = (ST_Item_Volume)Items[1];
        ST_Item_Volume SE_vol = (ST_Item_Volume)Items[2];
        ST_item_text Screen = (ST_item_text)Items[3];
        ST_item_text Language = (ST_item_text)Items[6];


        M_vol.Value = configData.MasterVolume * 5;
        BGM_vol.Value = configData.BGM * 5;
        SE_vol.Value = configData.SE * 5;

        Screen.Value = configData.ScreenModeName.ToString();
        Language.Value = configData.language.ToString();

        M_vol.DataUpdate();
        BGM_vol.DataUpdate();
        SE_vol.DataUpdate();

        Screen.DataUpdate();
        Language.DataUpdate();

    }


    void LEFT()
    {
        switch (nowSelect)
        {
            case Selected.MasterVol:
                configData.MasterVolume--;

                break;
            case Selected.SEvol:
                configData.SE--;
                break;
            case Selected.BGMvol:
                configData.BGM--;
                break;
            case Selected.Resolution:
                configData.screenMode = (ConfigData.ScreenMode)(((int)configData.screenMode + Enum.GetNames(typeof(ConfigData.ScreenMode)).Length - 1) % Enum.GetNames(typeof(ConfigData.ScreenMode)).Length);
                break;

            case Selected.Language:
                configData.language = (ConfigData.Language)(((int)configData.language + Enum.GetNames(typeof(ConfigData.Language)).Length - 1) % Enum.GetNames(typeof(ConfigData.Language)).Length);
                break;
        }
    }
    void RIGHT()
    {
        switch (nowSelect)
        {
            case Selected.MasterVol:
                configData.MasterVolume++;
                break;
            case Selected.SEvol:
                configData.SE++;
                break;
            case Selected.BGMvol:
                configData.BGM++;
                break;
            case Selected.Resolution:
                configData.screenMode = (ConfigData.ScreenMode)(((int)configData.screenMode + 1) % Enum.GetNames(typeof(ConfigData.ScreenMode)).Length);
                break;

            case Selected.Language:
                configData.language = (ConfigData.Language)(((int)configData.language + 1) % Enum.GetNames(typeof(ConfigData.Language)).Length);
                break;
        }
    }
    void DECIDE()
    {
        switch (nowSelect)
        {
            case Selected.Default:
                SetDefault();
                break;

            case Selected.Exit:
                GM.Game.StateQueue();
                break;
        }
    }

    void SetDefault()
    {
        configData.Init();
    }

}