using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create ProjectileData")]
public class ProjectileData : ScriptableObject
{
    [Header("スプライトアニメーション")]
    public  Sprite[] sprites;
    public float AnimationSpeed;

    [Header("ダメージデータ")]
    public AC_set[] dataset;
    /// <summary>
    /// 攻撃ヒット後にも攻撃判定が残るか
    /// </summary>
    public bool Remain = false;

    public ActiveMode activeMode;

    [Header("地形判定を貫通するかどうか")]
    public Projectile.TerrainHitType hitType;

    [Header("弾の回転の設定")]
    public Projectile.RotateType rotateType;

    public Vector2 acc;



    [Header("エリア切り替え時に消えるかどうか")]
    public bool DontDestroyOnSectionChange = false;

}
