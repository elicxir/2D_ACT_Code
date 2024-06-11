using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
public class EC_Show_BossName : EventCommand
{
    public string Name;

    public override IEnumerator Command()
    {
        GM.UI.BossHP.BossStart(Name);

        GM.UI.BossHP.UI_Show();

        yield break;
    }

}
