using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

//�w��͈͓��ɓ������^�C�~���O�Ŏw��n�_�ɓG���o��������
public class EP_Generate : EnemyPlacer
{
    /// <summary>
    /// �E�������N�_�Ƃ��ăv���C���[�̐N�������ɂ�萶���ʒu�𔽓]������(�g���ꍇ�͌����ɒ��ӂ��邱��)
    /// </summary>
    public bool useDirection = true;

    Player player
    {
        get
        {
            return GM.Player.Player;
        }
    }

    public int DetectRange = 80;

    bool inRangeFlag=false;

    public override void Updater()
    {
        if (!inRangeFlag && ChackInRange())
        {
            if((player.Position - Position).x > 0)
            {
                OnPlayerEnter( Entity.FaceDirection.Left);
            }
            else
            {
                OnPlayerEnter(Entity.FaceDirection.Right);
            }
            inRangeFlag = true;

        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction">�v���C���[�̐i�s����(�����痈���Ȃ�Right)</param>
    protected virtual void OnPlayerEnter(Entity.FaceDirection direction)
    {
        Generate(Position);
    }



    bool ChackInRange()
    {
        return !((player.Position - Position).sqrMagnitude > (DetectRange + player.TerrainUpdateDetectRange)* (DetectRange + player.TerrainUpdateDetectRange));
    }


    private new void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DetectRange);
        DrawBehaviorRect();
    }

    protected override void InitFunction()
    {
        inRangeFlag = false;
    }

    protected void Generate(Vector2 CenterPos, bool mirror = false)
    {
        foreach (EnemyPlaceData epd in enemyPlaceDatas)
        {
            if (epd.enemyMarker != null)
            {
                Vector2 relatedPos = (epd.enemyMarker.Position - Position);

                if (mirror)
                {
                    relatedPos = new Vector2(-relatedPos.x, relatedPos.y);
                }

                Place(epd, CenterPos+ relatedPos);
            }
            else
            {
                Place(epd, CenterPos);
            }
        }
    }


}
