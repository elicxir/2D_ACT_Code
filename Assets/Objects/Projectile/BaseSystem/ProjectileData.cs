using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create ProjectileData")]
public class ProjectileData : ScriptableObject
{
    [Header("�X�v���C�g�A�j���[�V����")]
    public  Sprite[] sprites;
    public float AnimationSpeed;

    [Header("�_���[�W�f�[�^")]
    public AC_set[] dataset;
    /// <summary>
    /// �U���q�b�g��ɂ��U�����肪�c�邩
    /// </summary>
    public bool Remain = false;

    public ActiveMode activeMode;

    [Header("�n�`������ђʂ��邩�ǂ���")]
    public Projectile.TerrainHitType hitType;

    [Header("�e�̉�]�̐ݒ�")]
    public Projectile.RotateType rotateType;

    public Vector2 acc;



    [Header("�G���A�؂�ւ����ɏ����邩�ǂ���")]
    public bool DontDestroyOnSectionChange = false;

}
