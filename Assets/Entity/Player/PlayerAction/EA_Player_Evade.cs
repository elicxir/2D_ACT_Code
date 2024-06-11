using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EA_Player_Evade : PlayerAction
{

    public bool canMoveBack = true;

    const float invincibleStart = 0.08f;
    const float invincibleEnd = 0.9f;

    const float moveStart = 0.04f;
    const float moveEnd = 0.88f;

    const float evadeInvincible = 0.1f;

    const int EvadeSpeed = 240;

    public override IEnumerator Act()
    {
        entity.BaseAcceleration.x = 0;
        entity.BaseVelocity.x = 0;

        Play(ActionName);

        while (GetState.normalizedTime < 1)
        {
            if (GetState.normalizedTime >= invincibleStart && GetState.normalizedTime <= invincibleEnd)
            {
                player.buffManager.ADD(new Buff { contents = new BuffContent[1] { new BuffContent { buffType = BuffType.Invincible, amount = 1 } }, ID = "evade", timer = evadeInvincible });
            }
            else
            {
                player.buffManager.REMOVE("evade");
            }

            if (GetState.normalizedTime >= moveStart && GetState.normalizedTime <= moveEnd)
            {
                if (canMoveBack)
                {
                    float progress = (GetState.normalizedTime - moveStart) / (moveEnd - moveStart);

                    player.BaseVelocity.x = -player.FrontVector.x * EvadeSpeed * (1 - progress);
                }
                else
                {
                    player.BaseVelocity = Vector2.zero;
                }
            }

            yield return null;
        }

        if (!player.isEffected && !canMoveBack)
        {

        }

        Stop();
    }
}
