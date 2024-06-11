using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CatalystDataSheet : ScriptableObject
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
		public string flavor1;
		public string flavor2;
		public string flavor3;
		public string passive;
		public string p_desc1;
		public string p_desc2;
		public string p_desc3;
		public string directional;
		public string d_desc1;
		public string d_desc2;
		public string d_desc3;
		public float d_mp;
		public string another;
		public string a_desc1;
		public string a_desc2;
		public string a_desc3;
		public float a_mp;
	}
}

