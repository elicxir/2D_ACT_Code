using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�o�^���ꂽ�C�x���g�R�}���h�����ԂɎ��s����C�x���g�R�}���h
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
