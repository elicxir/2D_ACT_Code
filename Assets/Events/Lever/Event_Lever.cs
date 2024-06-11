using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Managers;

public class Event_Lever : Event
{
    enum Mode
    {
        ButtonOnce,Button,Toggle
    }
    [SerializeField] Mode mode = Mode.ButtonOnce;

    public string LeverID
    {
        get
        {
            return "Lever" + transform.position;
        }
    }

    public string FlagName = string.Empty;

    private void OnValidate()
    {
        if (FlagName == string.Empty)
        {
            FlagName = LeverID;
        }

        EventCommand[] ecs = this.gameObject.GetComponents<EventCommand>();
        List<EventCommand> list = ecs.ToList<EventCommand>();

        foreach(EventCommand ec in list)
        {
            if (ec is EC_Branch_Flag)
            {
                EC_Branch_Flag ecp = (EC_Branch_Flag)ec;
                ecp.Flag = FlagName;
            }

            if (ec is EC_GameFlag)
            {
                EC_GameFlag ecp = (EC_GameFlag)ec;
                ecp.data.FlagID = FlagName;
            }
        }
    }

    public override bool EventFlag()
    {
        switch (mode)
        {
            case Mode.ButtonOnce:
                return !GM.Game.PlayData.GetGameFlag(FlagName).isTrue;
        }

        return true;
    }

}
