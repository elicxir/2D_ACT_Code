using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellDataSheet : ScriptableObject
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
		public string flavor1;
		public string flavor2;
		public string flavor3;
		public string flavor4;
		public int empower_time;
		public int MP;
		public int empowered_MP;
		public int type;
	}
}

