using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using GameConsts;

//�w��͈͓��ɓ������^�C�~���O�ŃJ�����ɑ΂��鑊�΍��W�̎w��n�_�ɓG���o��������
public class EP_Generate_Related : EP_Generate
{


    private new void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(Game.width, Game.height));



        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DetectRange);

        DrawBehaviorRect();
    }

    protected override void OnPlayerEnter(Entity.FaceDirection direction)
    {
        bool mirror = (direction == Entity.FaceDirection.Left) && useDirection;

        Generate(GM.Game.Camera.Position, mirror);
    }
}
