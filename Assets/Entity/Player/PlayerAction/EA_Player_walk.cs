using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

//���s�̃A�N�V������S���N���X
public class EA_Player_walk : PlayerAction
{
    //�X�v���C�g�ʒu�̒����p�f�[�^
    [SerializeField] SpriteObjectData sod;

    //���̃G�t�F�N�g�̂��߂̈ʒu���
    [SerializeField] Transform FootMarker;

    //���G�t�F�N�g���o���ʒu
    Vector2 pos
    {
        get
        {
            return (Vector2)FootMarker.position + player.FrontVector * -7 + Vector2.up * 3;
        }
    }

    //�A�N�V�����J�n���ɌĂ΂��
    public override IEnumerator Act()
    {
        bool f1 = true;
        bool f2 = true;

        //�A�j���[�V�����̍Đ�(�A�j���[�V����1���[�v�P�ʂł̏���������)
        Play(ActionName);

        //�A�j���[�V�������I������܂Ń��[�v
        while (GetState.normalizedTime < 1)
        {
            //���s�X�s�[�h�̒���
            float speed = 1 + player.buffManager.GetValue(BuffType.Speed);
            player.CustomPlayingSpeedMult = speed;

            //���s���̑��x�̒���(�������̊����Ȃǂ��Ǘ����Ă���)
            if (Input.InputVector().x > 0)
            {
                entity.BaseAcceleration.x = +player.PAV.WalkAcceleration;

                if (entity.BaseVelocity.x < 0)
                {
                    entity.BaseVelocity.x = 0;
                }
            }
            else if (Input.InputVector().x < 0)
            {
                entity.BaseAcceleration.x = -player.PAV.WalkAcceleration;

                if (entity.BaseVelocity.x > 0)
                {
                    entity.BaseVelocity.x = 0;
                }
            }
            entity.BaseVelocity.x = Mathf.Clamp(entity.BaseVelocity.x, -player.PAV.GroundBaseSpeed * speed, player.PAV.GroundBaseSpeed * speed);

            //�����o��
            if (GetState.normalizedTime > 0.1f && f1)
            {
                f1 = false;
                GM.OBJ.SpriteObjectManager.GenerateByData(sod, pos, player.faceDirection == Entity.FaceDirection.Left);
            }
            //�����o��
            if (GetState.normalizedTime > 0.6f && f2)
            {
                f2 = false;
                GM.OBJ.SpriteObjectManager.GenerateByData(sod, pos,player.faceDirection== Entity.FaceDirection.Left);
            }

            yield return null;
        }
        //�A�j���[�V�����I�����Ɉ�x���x�����Z�b�g
        entity.BaseAcceleration.x = 0;
        //�A�j���[�V�����̒�~
        Stop();
        //�����蔻��̃��Z�b�g
        SettingReset();

    }

}
