using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using GameConsts;

//指定範囲内に入ったタイミングでカメラに対する相対座標の指定地点に敵を出現させる
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
