using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConsts;
using Managers;
using System;
using AIE2D;
/// <summary>
/// �n�`�Ƃ̓����蔻��͋����Ŏ��Ȃ���΂Ȃ�Ȃ��B���̏ꍇ���傤�ǒ��S����AdjustedPosition
/// </summary>
public class OwnTimeMonoBehaviour : MonoBehaviour
{
    //�e�탌�C���[�̃}�X�N
    protected LayerMask Terrain = 1 << 8;
    protected LayerMask ATK_Layer = 1 << 13;

    protected LayerMask PlayerLayer = 1 << 10;
    protected LayerMask EnemyLayer = 1 << 11;
    protected LayerMask ProjectileLayer = 1 << 12;


    protected enum bodyType
    {

        Simulated,//
        notTransParent,//�n�`�ɏ]�����d�͂𖳎�
        TransParent,//���S�ɒn�`�𖳎�
    }

    [SerializeField] protected bodyType BodyType;

    [SerializeField] protected DynamicAfterImageEffect2DPlayer DAIE;

    /// <summary>
    /// ��ƂȂ鎞�Ԑi�s���x(�Q�[����:1 ����ȊO:0)
    /// </summary>
    float TimeMultBase = 1;

    public virtual void EnterGame()
    {
        TimeMultBase = 1;
    }
    public virtual void ExitGame()
    {
        TimeMultBase = 0;
    }

    protected SpriteObjectManager SOM
    {
        get
        {
            return GM.Game.mainGame.spriteObjectManager;
        }
    }





    public float TimeMult
    {
        get
        {
            return TimeMult_Var * TimeMultBase;
        }
    }

    protected virtual float TimeMult_Var
    {
        get
        {
            return 1;
        }
    }


    public float OwnDeltaTime
    {
        get
        {
            return TimeMult * Time.deltaTime;
        }
    }

    public float OwnTimeSiceStart
    {
        get
        {
            return OwnTimeSiceStart_Var;
        }
    }
    float OwnTimeSiceStart_Var = 0;

    protected virtual void OnChangeTimeMult(float before, float after)
    {
        print($"timescale changed from {before} to {after}");
    }



    public bool isActive
    {
        get
        {
            return gameObject.activeSelf;
        }
    }


    //���x�Ɖ����x
    public Vector2 Velocity
    {
        get
        {
            if (isEffected)
            {
                return BaseVelocity + EffectVelocity;
            }
            else
            {
                return BaseVelocity;
            }
        }
    }

    [Header("���x")]
    public Vector2 BaseVelocity;
    public Vector2 EffectVelocity;

    [Header("�����x")]
    public Vector2 BaseAcceleration;
    public Vector2 EffectAcceleration;

    public bool isEffected = true;//���x,�����x���O���ɉe������邩�ǂ���

    protected Vector2 MoveBuffer;

    //��ʕ\����+
    public Vector2Int AdjustedPosition;

    //�v�Z�Ŏg������ۂ̈ʒu�͂���
    public virtual Vector2 Position
    {
        get
        {
            return transform.position;
        }
        set
        {
            transform.position = value;

            AdjustedPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        }
    }

    public Vector2Int NowMapGrid
    {
        get
        {
            int mapx = AdjustedPosition.x / Game.width + 1;
            int mapy = AdjustedPosition.y / Game.height + 1;

            return new Vector2Int(mapx, mapy);
        }
    }

    void VarReset()
    {
        OwnTimeSiceStart_Var = 0;

        BaseVelocity = Vector2.zero;
        EffectVelocity = Vector2.zero;

        isEffected = true;

        MoveBuffer = Vector2.zero;
    }

    public virtual void Init()
    {
        VarReset();
        AdjustedPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
    }

    protected virtual void Move()
    {
        {
            while (MoveBuffer.x >= 1)
            {
                AdjustedPosition += Vector2Int.right;
                MoveBuffer.x--;
            }

            while (MoveBuffer.x <= -1)
            {
                AdjustedPosition += Vector2Int.left;
                MoveBuffer.x++;
            }
            while (MoveBuffer.y >= 1)
            {
                AdjustedPosition += Vector2Int.up;
                MoveBuffer.y--;
            }

            while (MoveBuffer.y <= -1)
            {
                AdjustedPosition += Vector2Int.down;
                MoveBuffer.y++;
            }

        }
        transform.position = new Vector3(AdjustedPosition.x, AdjustedPosition.y, 0);
    }

    public virtual void Updater(bool UpdateFlag = false)
    {
        OwnTimeSiceStart_Var += OwnDeltaTime;

        BaseVelocity += BaseAcceleration * OwnDeltaTime;
        EffectVelocity += EffectAcceleration * OwnDeltaTime;

        MoveBuffer += Velocity * OwnDeltaTime;

        Move();
    }
}
