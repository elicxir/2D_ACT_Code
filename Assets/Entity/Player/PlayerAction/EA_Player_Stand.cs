using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EA_Player_Stand : PlayerAction
{
    int count = 0;

    public override IEnumerator Act()
    {
        count++;

        string AnimationName=string.Empty;

        if (player.BeforeState == Player.State.Crouch)//直前にしゃがんでいるなら
        {
            AnimationName = GetActionName(2);//2番目のアニメーションを再生(しゃがみから立ち上がるアニメーション)
        }
        else if (player.BeforeState == Player.State.Falling)//直前に落下しているなら
        {
            AnimationName = GetActionName(3);//3番目のアニメーションを再生(接地するアニメーション)
        }
        else
        {
            if (count % 4 == 0)//4回に一回実行される
            {
                AnimationName = GetActionName(1);//4回に一回実行される瞬きをするアニメーション
            }
            else
            {
                AnimationName = GetActionName(0);//通常の立ちアニメーション
            }
        }

        Play(AnimationName);//上の処理で選択されたアニメーションを再生する

        while (GetState.normalizedTime < 1)
        {           
            StandControll();//アニメーション再生中は立ち状態の行動管理をする(当たり判定など)
            yield return null;
        }
        Stop();
    }
}
