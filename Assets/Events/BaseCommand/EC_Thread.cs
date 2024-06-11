using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//登録されたイベントコマンドを順番に実行するイベントコマンド
public class EC_Thread : EventCommand
{
    [SerializeField] List<EventCommand> commands;

    public override IEnumerator Command()
    {
        foreach(EventCommand ec in commands)
        {
            yield return StartCoroutine(ec.Command());
        }
    }
}
