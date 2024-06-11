using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataTypes.Element;
[CreateAssetMenu(menuName = "MyScriptable/Create EntityHitBoxData")]
public class EntityHitBoxData : ScriptableObject
{

	public Element BaseDeffence;

	public Durability durability;
}
