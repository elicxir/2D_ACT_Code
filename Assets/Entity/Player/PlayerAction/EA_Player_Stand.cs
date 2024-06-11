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

        if (player.BeforeState == Player.State.Crouch)//���O�ɂ��Ⴊ��ł���Ȃ�
        {
            AnimationName = GetActionName(2);//2�Ԗڂ̃A�j���[�V�������Đ�(���Ⴊ�݂��痧���オ��A�j���[�V����)
        }
        else if (player.BeforeState == Player.State.Falling)//���O�ɗ������Ă���Ȃ�
        {
            AnimationName = GetActionName(3);//3�Ԗڂ̃A�j���[�V�������Đ�(�ڒn����A�j���[�V����)
        }
        else
        {
            if (count % 4 == 0)//4��Ɉ����s�����
            {
                AnimationName = GetActionName(1);//4��Ɉ����s�����u��������A�j���[�V����
            }
            else
            {
                AnimationName = GetActionName(0);//�ʏ�̗����A�j���[�V����
            }
        }

        Play(AnimationName);//��̏����őI�����ꂽ�A�j���[�V�������Đ�����

        while (GetState.normalizedTime < 1)
        {           
            StandControll();//�A�j���[�V�����Đ����͗�����Ԃ̍s���Ǘ�������(�����蔻��Ȃ�)
            yield return null;
        }
        Stop();
    }
}
