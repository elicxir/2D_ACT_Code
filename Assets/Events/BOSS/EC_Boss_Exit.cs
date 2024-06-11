using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_Boss_Exit : EventCommand
{
    [SerializeField] List<Boss_Door> boss_Doors;

    [SerializeField] List<EventCommand> Commands_Exit;

    public override IEnumerator Command()
    {







        return base.Command();
    }




}
