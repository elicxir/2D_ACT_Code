using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataTypes.Element;

[CreateAssetMenu(menuName = "MyScriptable/Create EntityStatsData")]

public class EntityStatsData : ScriptableObject
{
	public BaseStats Stats;

	public bool hasHitStop = true;

}
