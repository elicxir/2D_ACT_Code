using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "MyScriptable/Create EnemyPlaceStatusData")]

public class EnemyPlaceStatusData : ScriptableObject
{
    [Header("呼び出す敵の設定")]

    public EnemyManager.EnemyName enemyName;//敵の種族名
    [Range(0, 6)] public int VariantIndex = 0;//同じ種族の中での形式違い

    [Header("敵の状態設定")]

    public bool facePlayer;
    public Entity.FaceDirection face;

    public Rect BehaviorRect = new Rect(0, 0, 100, 48);
}
