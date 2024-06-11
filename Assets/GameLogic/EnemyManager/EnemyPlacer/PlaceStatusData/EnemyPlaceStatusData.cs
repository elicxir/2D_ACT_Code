using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "MyScriptable/Create EnemyPlaceStatusData")]

public class EnemyPlaceStatusData : ScriptableObject
{
    [Header("�Ăяo���G�̐ݒ�")]

    public EnemyManager.EnemyName enemyName;//�G�̎푰��
    [Range(0, 6)] public int VariantIndex = 0;//�����푰�̒��ł̌`���Ⴂ

    [Header("�G�̏�Ԑݒ�")]

    public bool facePlayer;
    public Entity.FaceDirection face;

    public Rect BehaviorRect = new Rect(0, 0, 100, 48);
}
