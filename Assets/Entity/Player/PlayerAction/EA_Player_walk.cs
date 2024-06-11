using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

//歩行のアクションを担うクラス
public class EA_Player_walk : PlayerAction
{
    //スプライト位置の調整用データ
    [SerializeField] SpriteObjectData sod;

    //煙のエフェクトのための位置情報
    [SerializeField] Transform FootMarker;

    //煙エフェクトを出す位置
    Vector2 pos
    {
        get
        {
            return (Vector2)FootMarker.position + player.FrontVector * -7 + Vector2.up * 3;
        }
    }

    //アクション開始時に呼ばれる
    public override IEnumerator Act()
    {
        bool f1 = true;
        bool f2 = true;

        //アニメーションの再生(アニメーション1ループ単位での処理をする)
        Play(ActionName);

        //アニメーションが終了するまでループ
        while (GetState.normalizedTime < 1)
        {
            //歩行スピードの調整
            float speed = 1 + player.buffManager.GetValue(BuffType.Speed);
            player.CustomPlayingSpeedMult = speed;

            //歩行時の速度の調整(加速時の慣性なども管理している)
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

            //煙を出す
            if (GetState.normalizedTime > 0.1f && f1)
            {
                f1 = false;
                GM.OBJ.SpriteObjectManager.GenerateByData(sod, pos, player.faceDirection == Entity.FaceDirection.Left);
            }
            //煙を出す
            if (GetState.normalizedTime > 0.6f && f2)
            {
                f2 = false;
                GM.OBJ.SpriteObjectManager.GenerateByData(sod, pos,player.faceDirection== Entity.FaceDirection.Left);
            }

            yield return null;
        }
        //アニメーション終了時に一度速度をリセット
        entity.BaseAcceleration.x = 0;
        //アニメーションの停止
        Stop();
        //当たり判定のリセット
        SettingReset();

    }

}
