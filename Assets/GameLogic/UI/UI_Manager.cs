using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] UI_exp UI_Exp;
    [SerializeField] UI_Bar HP;
    [SerializeField] UI_Bar MP;
    public  UI_Bar2 BossHP;
    [SerializeField] UI_Buff Buff;

    [SerializeField] UI_Condition Condition;

    public  UI_Warning UI_Warning;
    [SerializeField] UI_SystemText UI_SystemText;
    public UI_StatusUP UI_StatusUP;

    public TalkWindow talkWindow;

    public UI_ItemGetWindow itemGetWindow;

    public UI_StageName StageName;
    public UI_ArtsPallet ArtsPallet;
    public UI_Item Item;
    Vector2 r_pos;

    private void LateUpdate()
    {
        r_pos = GM.Player.R_Pos;

        Condition.SetPos(r_pos);
    }


    /// <summary>
    /// HP,MP‚Ì•\Ž¦
    /// </summary>

    public void Updater(EntityStats player)
    {
        HP.fillamount = (float)player.HP / player.MaxHP;
        MP.fillamount = (float)player.MP / player.MaxMP;

        Condition.SetData(player);

        Buff.SetBuffGraph();

        BossHP.Updater();

    }


    public void Warning(string mes, Color color)
    {
        UI_Warning.SHOW_WARNING(mes, color);
    }


    public void SHOW_SYSTEMTEXT(string mes, Color color)
    {
        UI_SystemText.SHOW_SYSTEMTEXT(mes, color);
    }

    public void HIDE_SYSTEMTEXT()
    {
        UI_SystemText.HIDE_SYSTEMTEXT();
    }

}
