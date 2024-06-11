using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class Event_BossBattle : Event
{
    public string BossID
    {
        get
        {
            return "Boss" + MapGrid;
        }
    }



    private void OnValidate()
    {
        Flags.data.FlagID= BossID;
    }



    [SerializeField] EC_GameFlag Flags;
    /// <summary>
    /// progressは
    /// 0:ボス戦開始前
    /// 1:ボス戦中
    /// 2:ボス戦終了後
    /// </summary>
    public override bool EventFlag()
    {
        return !GM.Game.PlayData.GetGameFlag(BossID).isTrue;
    }
    public override bool AutoeventFlag
    {
        get
        {
            bool c1 = GM.Game.PlayData.GetGameFlag(BossID).progress == 0;
            return c1;
        }
    }




    /*
    public override bool CheckTouchEvent(Vector2 vector2)
    {
        if (EventFlag())
        {
            foreach (Boss_Door p in boss_Doors)
            {
                if((Mathf.Abs(vector2.y - (p.Pos.y)) <= 0.5 * Hitbox.y) && (Mathf.Abs(vector2.x - (p.Pos.x)) <= 0.5 * Hitbox.x))
                {
                    return true;
                }
            }

            return false;
        }
        else
        {
            return false;
        }
    }
    */


    public override IEnumerator DoEvent()
    {
        int index = 0;

        print(this.name + " start");

        if (eventCommands.Count > 0)
        {
            do
            {
                if (eventCommands[index] != null)
                {
                    yield return StartCoroutine(eventCommands[index].Command());
                    index = eventCommands[index].NextCommandIndex(index, eventCommands);

                }
                else
                {

                    yield break;
                }
            }
            while (index != -1);
        }
        else
        {

            yield break;
        }



    }

}
