using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class EC_StatusUP : EventCommand
{
    public StatusType statusType;
    public int ID;

    public override IEnumerator Command()
    {
        yield return StartCoroutine(GM.UI.UI_StatusUP.StatusUpText(statusType));

        switch (statusType)
        {
            case StatusType.HP:
                if (GM.Game.PlayData.AddStatusUpFlag(ID))
                {
                    GM.Game.PlayData.HP_UP++;
                    print("HP UP");
                }
                else
                {
                    print("failed to HP UP");
                }
                break;

            case StatusType.MP:
                if (GM.Game.PlayData.AddStatusUpFlag(ID))
                {
                    GM.Game.PlayData.MP_UP++;
                    print("MP UP");
                }
                else
                {
                    print("failed to MP UP");
                }

                break;

            case StatusType.ATK:
                if (GM.Game.PlayData.AddStatusUpFlag(ID))
                {
                    GM.Game.PlayData.ATK_UP++;
                    print("atk UP");
                }
                else
                {
                    print("failed to atk UP");
                }
                break;

            default:
                break;

        }

    }


}

public enum StatusType
{
    HP,
    MP,
    ATK
}