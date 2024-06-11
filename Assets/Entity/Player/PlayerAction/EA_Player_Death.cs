using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class EA_Player_Death : PlayerAction
{
    [SerializeField] Sprite sprite;

    public override IEnumerator Act()
    {
        entity.BaseAcceleration.x = 0;
        entity.BaseVelocity.x = 0;

        GM.Game.GameOver_Image(true, GM.Game.R_Pos(player.sprite_renderer.transform.position), player.sprite_renderer.sprite, player.FrontVector.x < 0);

        string AnimationName = string.Empty;


        if (player.BeforeState == Player.State.KnockBack_Crouch)
        {
            AnimationName = GetActionName(1);
        }
        else
        {
            AnimationName = GetActionName(0);
        }

        Play(AnimationName);

        while (GetState.normalizedTime < 1)
        {
            GM.Game.GameOver_Image(true, GM.Game.R_Pos(player.sprite_renderer.transform.position), player.sprite_renderer.sprite, player.FrontVector.x < 0);

            entity.BaseAcceleration.x = 0;
            entity.BaseVelocity.x = 0;

            yield return null;
        }

        Stop();
        GM.Game.GameOver_Image(false, GM.Game.R_Pos(player.sprite_renderer.transform.position), player.sprite_renderer.sprite, player.FrontVector.x < 0);

        player.sprite_renderer.enabled = false;
    }
}