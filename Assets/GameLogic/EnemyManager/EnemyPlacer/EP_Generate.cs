using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

//指定範囲内に入ったタイミングで指定地点に敵を出現させる
public class EP_Generate : EnemyPlacer
{
    /// <summary>
    /// 右向きを起点としてプレイヤーの侵入方向により生成位置を反転させる(使う場合は向きに注意すること)
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
    /// <param name="direction">プレイヤーの進行方向(左から来たならRight)</param>
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
