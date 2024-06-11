using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
public class EventCommand : MonoBehaviour
{
    protected InputSystemManager INPUT
    {
        get
        {
            return GM.Game.Input_Manager;
        }
    }


    protected int Next(int nowIndex, List<EventCommand> list)
    {
        int result;

        result = nowIndex + 1;

        if (result > list.Count - 1)
        {
            result = -1;
        }

        return result;

    }

    public virtual int NextCommandIndex(int nowIndex,List<EventCommand> list)
    {
        return Next(nowIndex,list);
    }

    public virtual IEnumerator Command()
    {
        yield break;
    }
}
