using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class EC_BossBattle : EventCommand
{

    public bool isBossBattleContinue;



    public override IEnumerator Command()
    {
        GM.Player.OnEvent = false;

        yield return new WaitWhile(()=>isBossBattleContinue);

        print("�{�X�o�g���I��");

        yield return null;
    }
}
