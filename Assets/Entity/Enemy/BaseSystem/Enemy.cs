using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using System;
using DataTypes.Functions;
using DataTypes.Element;

public class Enemy : Entity
{
    protected Vector2 PlayerPos
    {
        get
        {
            return GM.Player.Player.Position;
        }
    }

    [Header("行動範囲用マーカー")]
    public Rect BehaviorRect;

    [Header("色違い用の処理")]
    [SerializeField] bool isVariant = false;
    [SerializeField] ReplaceInfo[] replaceInfo;
    void sprChange()
    {
        string sprName = sprite_renderer.sprite.name;

        int pos = sprName.LastIndexOf('_');
        if (pos >= 0)
        {
            string pre = sprName.Substring(0, pos);
            int index;
            try
            {
                index = int.Parse(sprName.Substring(pos + 1));
            }
            catch (FormatException)
            {
                return;
            }

            foreach (ReplaceInfo nfo in replaceInfo)
            {
                if (nfo.fromName == pre)
                {
                    sprite_renderer.sprite = nfo.sprite[index];

                    break;
                }
            }
        }
    }
    private void LateUpdate()
    {
        if (isVariant)
        {
            sprChange();
        }
    }

    protected override void OnDamageDealt(Element damage, Vector2 pos)
    {
        if (damage.Sum > 0)
        {
            SOM.GenerateByData(SOM.basicSODs[(int)SpriteObjectManager.DataIndex.HitEffect], pos);

            if (statsData.hasHitStop)
            {
                BuffContent slow = new BuffContent { amount = -0.8f, buffType = BuffType.TimeScale };
                Buff buff = new Buff { timer = 0.14f, contents = new BuffContent[1] { slow }, ID = "damageSlow" };

                buffManager.ADD(buff);
            }
        }
    }

    /// <summary>
    /// enemyとplayerが向き合っているかどうか
    /// </summary>
    protected bool FacePlayer
    {
        get
        {
            return ((Player.Position - Position).x * FrontVector.x >= 0);
        }
    }

    protected override IEnumerator DeathCoRoutine()
    {
        DisAppear();
        yield break;
    }


    protected ProjectileManager projectileManager
    {
        get
        {
            return GM.Projectile;
        }
    }

    protected Player Player
    {
        get
        {
            return GM.Player.Player;
        }
    }
    [Header("Enemy")]
    public EnemyManager.EnemyName type;
    public int variant;


    protected override void EntityUpdater()
    {
        //HitBoxActivation();




        //Debug.DrawRay(transform.position, Vector2.up * 50);

    }
    /*

    //特定範囲外ではダメージを受けなくする
    protected virtual void HitBoxActivation()
    {
        int range = 320;

        bool f = (Player.Position - Position).sqrMagnitude < range * range;

        foreach (AttackHitBox item in attackHitBox)
        {
            item.HitBoxEnable = f;
        }
    }
    */

    public void DisAppear()
    {
        //StopAllCoroutines();
        gameObject.SetActive(false);
    }



    public void Appear(Vector2 pos, FaceDirection direction, Rect behaviorRect)
    {
        Position = pos;

        BehaviorRect = behaviorRect;

        if (direction != faceDirection)
        {
            Flip();
        }

        Init();

    }


    public override void Init()
    {
        base.Init();
    }

    /// <summary>
    ///  敵が行動範囲エリアの外側を向いているか
    /// </summary>
    protected bool FaceOuter
    {
        get
        {
            switch (faceDirection)
            {
                case FaceDirection.Right:
                    if (Position.x > C_Rect.Center(BehaviorRect).x)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case FaceDirection.Left:
                    if (Position.x < C_Rect.Center(BehaviorRect).x)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }
    }

    /// <summary>
    ///  x座標において敵が行動範囲エリア内にいるか
    /// </summary>
    protected bool inExtent_X
    {
        get
        {
            bool f1 = Position.x <= C_Rect.Right(BehaviorRect);
            bool f2 = Position.x >= C_Rect.Left(BehaviorRect);

            return f1 && f2;
        }
    }

    /// <summary>
    /// プレイヤーに対する相対位置
    /// </summary>
    protected Vector2 RelatedPos
    {
        get
        {
            return Position - Player.Position;
        }
    }

    protected bool inRange(int min, int max)
    {
        bool c1 = min * min < (RelatedPos).sqrMagnitude;
        bool c2 = max * max > (RelatedPos).sqrMagnitude;

        return c1 && c2;
    }


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        if (isGround)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        // Gizmos.DrawWireSphere(Position, detectrange); //中心点とサイズ
        //Gizmos.DrawCube(FixedPoint, Vector3.one * 3); //中心点とサイズ
    }




}