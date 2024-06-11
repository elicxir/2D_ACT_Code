using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create MenuData")]
public class MenuData : ScriptableObject
{
	public string menuname;
	public Sprite sprite;
	[TextArea]
	public string inst;

}