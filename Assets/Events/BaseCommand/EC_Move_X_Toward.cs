using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
public class EC_Move_X_Toward : EC_Move
{
    public override IEnumerator Command()
    {
        if (Object.Position.x > destination.position.x)
        {
            GM.Player.OnForcedControll = true;

            while (Object.Position.x > destination.position.x)
            {

                Object.BaseVelocity.x = -Speed;
                yield return null;
            }
        }
        else if (Object.Position.x < destination.position.x)
        {
            GM.Player.OnForcedControll = true;

            while (Object.Position.x < destination.position.x)
            {
                Object.BaseVelocity.x = Speed;
                yield return null;
            }
        }
        else
        {

        }
        GM.Player.OnForcedControll = false;

        Object.BaseVelocity.x = 0;

    }
}
