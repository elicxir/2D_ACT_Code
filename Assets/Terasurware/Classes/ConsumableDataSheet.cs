using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConsumableDataSheet : ScriptableObject
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
		public int count;
		public string desc1;
		public string flavor1;
		public string flavor2;
		public string flavor3;
		public string flavor4;
		public string flavor5;
	}
}

