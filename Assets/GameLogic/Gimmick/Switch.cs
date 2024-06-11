using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//対象のイベントのEventFlagに応じて
public class Switch : Gimmick
{
    public override int UpdateRange => 80;

    [SerializeField] GameObject Switchable;//EventFlagによってアクティブ状態が変わる
    [SerializeField] Event Flag;

    public override bool Updater()
    {
        if (Flag.EventFlag())
        {
            if (Switchable != null)
            {
                if (!Switchable.activeSelf)
                {
                    Switchable.SetActive(true);
                    return true;
                }
            }

        }
        else
        {
            if (Switchable != null)
            {
                if (Switchable.activeSelf)
                {
                    Switchable.SetActive(false); return true;

                }
            }
        }

        return false;



    }
}
