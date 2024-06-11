using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class Branch : EventCommand
{

    [SerializeField] List<EventCommand> TRUE;
    [SerializeField] List<EventCommand> FALSE;

    public override int NextCommandIndex(int nowIndex, List<EventCommand> list)
    {
        return Next(nowIndex, list);
    }

    protected virtual bool Condition()
    {
        return false;
    }

    public override IEnumerator Command()
    {
        if (Condition())
        {

            int index = 0;

            print("branch:true");

            if (TRUE.Count > 0)
            {
                do
                {
                    if (TRUE[index] != null)
                    {
                        yield return StartCoroutine(TRUE[index].Command());
                        index = TRUE[index].NextCommandIndex(index, TRUE);

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
        else
        {
            int index = 0;

            print("branch:false");
            if (FALSE.Count > 0)
            {
                do
                {
                    if (FALSE[index] != null)
                    {
                        yield return StartCoroutine(FALSE[index].Command());
                        index = FALSE[index].NextCommandIndex(index, FALSE);

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

}
