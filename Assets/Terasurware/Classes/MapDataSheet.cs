using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapDataSheet : ScriptableObject
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
		
		public string mapname;
		public string areaname;
		public bool nameflag;
		public int up;
		public int down;
		public int right;
		public int left;
		public bool hidden;
		public int R;
		public int G;
		public int B;
	}
}

