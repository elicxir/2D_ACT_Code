using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillDataSheet : ScriptableObject
{	
	public List<Sheet> sheets = new List<Sheet> ();

	[System.SerializableAttribute]
	public class Sheet
	{
		public string name = string.Empty;
		public List<Param> list = new List<Param>();
	}

	[System.SerializableAttribute]
	public class Param
	{
		
		public string name;
		public string desc;
		public bool active;
		public bool weapon;
		public string inc_desc;
		public string flavor1;
		public string flavor2;
	}
}

