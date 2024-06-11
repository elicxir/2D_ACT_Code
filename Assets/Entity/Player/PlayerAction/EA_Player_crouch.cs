using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EA_Player_crouch : PlayerAction
{
    public override IEnumerator Act()
    {
        string AnimationName = string.Empty;


        if (player.BeforeState== Player.State.Stand)
        {
            AnimationName = GetActionName(1);
        }
        else if(player.BeforeState == Player.State.Falling)
        {
            AnimationName = GetActionName(2);
        }
        else
        {
            AnimationName = GetActionName(0);
        }


        Play(AnimationName);

        while (GetState.normalizedTime<1)
        {
            StandControll();

            yield return null;
        }

        Stop();
    }
}
