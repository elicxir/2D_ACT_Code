using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataTypes.Functions;
//���������ɔ��ł���
public class Bat_Horizontal : Bat
{
    const int FlySpeed = 64;//��s�̐�������
    const int FlySpeedRand = 4;//��s�̐��������̗���

    const int FlyHeight = 32;//�㉺�ɐU�����铮���̐U��
    const float FlyTime =1.6f;//�㉺�ɐU�����铮���̈���ɂ����鎞��

    float delta = 0;
    int speedrand = 0;

    public override void Init()
    {
        base.Init();
        delta = 2 * Mathf.PI * 0.125f * Rand.Range(0, 7);
    }

    protected override IEnumerator StateMachine()
    {
        while (isAlive)
        {

            DoAction((int)State.Fly);
            speedrand = Rand.Range(-FlySpeedRand, FlySpeedRand);
            yield return NowAction;
        }
    }

    protected override void EntityUpdater()
    {
        BaseVelocity = FrontVector * (FlySpeed+ speedrand) + FlyHeight * Vector2.up * Mathf.Sin(2 * Mathf.PI * OwnTimeSiceStart / FlyTime+ delta);
    }


    public  enum State
    {
        Fly,
    }
    public  State ActionState
    {
        get
        {
            return (State)ActionIndex;
        }
    }
}
