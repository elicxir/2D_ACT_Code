using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrayMenu : MonoBehaviour
{

    int select = 0;
    int select2 = 18000;

    [SerializeField] Color notselected;
    [SerializeField] Color selected;

    [SerializeField] Lean.Gui.LeanToggle Toggle;

    [SerializeField] RectTransform Pos;

    /// <summary>
    /// 休んだかどうか
    /// false:アイテム変更時に補充されない
    /// true:アイテム変更時に補充される
    /// </summary>
    bool rest = false;


    public void REFLESH2()
    {

    }

    public void REFLESH()
    {
        //SelectSystem();

        if (Define.IM.ButtonDown(Control.Right))
        {
        }
        else if (Define.IM.ButtonDown(Control.Up)||Define.IM.ButtonDown(Control.Menu))
        {
            if (rest)
            {
               // Define.SDM.GameData.FillEquip();

            }

            ExitPray();
        }
        else if (Define.IM.ButtonDown(Control.Down))
        {

            //Define.SDM.GameData.FillEquip();
            //やすむ
            rest = true;

        }
        else if (Define.IM.ButtonDown(Control.Left))
        {
            //わーぷ
        }

        
    }


    public void EnterPray()
    {
        //Define.PM.DATA_SAVE();

        rest = false;

        //Define.PM.HP_CHANGE(0, 100);
        //Define.PM.MP_CHANGE(0, 100);

        //Define.GM.GameState = gamestate.SaveMenu;
        Define.GM.TimeScale(0);
        select2 = 18000;
    }

    public void ExitPray()
    {
        Define.PM.DATA_SAVE();


        //Define.GM.GameState = gamestate.inGame;
        Define.GM.TimeScale(1);

    }

    void SelectSystem()
    {
        //Debug.Log(Pos.position);

        //Pos.position = new Vector3Int(192 * Define.GM.GAME_SIZE(), (132 - 30 * select) * Define.GM.GAME_SIZE(), 0);


        if (Define.IM.ButtonDown(Control.Up))
        {
            select2--;
        }
        else if (Define.IM.ButtonDown(Control.Down))
        {
            select2++;
        }

        select = select2 % 3;





    }
}
