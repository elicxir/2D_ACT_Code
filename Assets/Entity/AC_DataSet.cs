using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataTypes.Element;

/// <summary>
/// AttackColider�ɓo�^����_���[�W��Projectile�ɓo�^���Ă����_���[�W
/// </summary>
[CreateAssetMenu(menuName = "MyScriptable/Create AC_DataSet")]
public class AC_DataSet : ScriptableObject
{
	public Element AttackRatio;



	public Buff buff;
	public AC_Type ac_type= AC_Type.Once;
	/// <summary>
	/// Projectile�ɓo�^����Ƃ��ɂ͎g���Ȃ�(���ˎ��Ɋe�X�ݒ肷��)
	/// </summary>
	public HostileType hostileType;
	public Durability durability;
	public bool isKnockBack;//�m�b�N�o�b�N���肪���邩�ǂ���
}

/// <summary>
/// �u���x�v�̊T�O
/// 
/// None		�U���F����AC�Ƒ��݊����Ȃ�(���Ȃ�)�@�h��F���x��0�Ɠ���
/// Level XX	����AC�Ɠ��������Ƃ��ɂ̓��x���������ق����ʂ�A���x���̒Ⴂ�ق��͑��E�����B
///					(�ߐڍU���͖h�䑤�̂ق������x���������ƃ_���[�W��^�����Ȃ�)
/// Forced		
/// </summary>
public enum Durability {
	None,
	Level_1,
	Level_2,
	Level_3,
	Level_4,
	Level_5,
	Level_6,
	Level_7,
}

public enum HostileType
{
	Normal,//�����Ƌt��
	Ally,//�����Ɠ����w�c��
	Both,//���҂�
}