using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_MultThread : EventCommand
{
    [SerializeField] List<EventCommand> commands;

    Coroutine[] threads;

    public override IEnumerator Command()
    {
        threads = new Coroutine[commands.Count];
        int i = 0;

        foreach (EventCommand item in commands)
        {
            threads[i]=StartCoroutine(item.Command());
            i++;
        }

        foreach(Coroutine coroutine in threads)
        {
            yield return coroutine;
        }
    }

}
