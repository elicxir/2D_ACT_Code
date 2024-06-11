using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataTypes.Element;

/// <summary>
/// AttackColiderに登録するダメージやProjectileに登録しておくダメージ
/// </summary>
[CreateAssetMenu(menuName = "MyScriptable/Create AC_DataSet")]
public class AC_DataSet : ScriptableObject
{
	public Element AttackRatio;



	public Buff buff;
	public AC_Type ac_type= AC_Type.Once;
	/// <summary>
	/// Projectileに登録するときには使われない(発射時に各々設定する)
	/// </summary>
	public HostileType hostileType;
	public Durability durability;
	public bool isKnockBack;//ノックバック判定があるかどうか
}

/// <summary>
/// 「強度」の概念
/// 
/// None		攻撃：他のACと相互干渉しない(霧など)　防御：レベル0と同等
/// Level XX	他のACと当たったときにはレベルが高いほうが通り、レベルの低いほうは相殺される。
///					(近接攻撃は防御側のほうがレベルが高いとダメージを与えられない)
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
	Normal,//自分と逆に
	Ally,//自分と同じ陣営に
	Both,//両者に
}