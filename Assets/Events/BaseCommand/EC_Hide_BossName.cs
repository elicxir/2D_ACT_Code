using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
public class EC_Hide_BossName : EventCommand
{
    public override IEnumerator Command()
    {

        GM.UI.BossHP.UI_Hide();

        yield break;
    }
}
