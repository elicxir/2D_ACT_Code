using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class SpellData_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Manager/item/Spell/SpellData.xlsx";
	private static readonly string exportPath = "Assets/Manager/item/Spell/SpellData.asset";
	private static readonly string[] sheetNames = { "Sheet1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			SpellDataSheet data = (SpellDataSheet)AssetDatabase.LoadAssetAtPath (exportPath, typeof(SpellDataSheet));
			if (data == null) {
				data = ScriptableObject.CreateInstance<SpellDataSheet> ();
				AssetDatabase.CreateAsset ((ScriptableObject)data, exportPath);
				data.hideFlags = HideFlags.NotEditable;
			}
			
			data.sheets.Clear ();
			using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
				IWorkbook book = null;
				if (Path.GetExtension (filePath) == ".xls") {
					book = new HSSFWorkbook(stream);
				} else {
					book = new XSSFWorkbook(stream);
				}
				
				foreach(string sheetName in sheetNames) {
					ISheet sheet = book.GetSheet(sheetName);
					if( sheet == null ) {
						Debug.LogError("[QuestData] sheet not found:" + sheetName);
						continue;
					}

					SpellDataSheet.Sheet s = new SpellDataSheet.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						SpellDataSheet.Param p = new SpellDataSheet.Param ();
						
					cell = row.GetCell(0); p.name = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(1); p.desc = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p.flavor1 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(3); p.flavor2 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(4); p.flavor3 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(5); p.flavor4 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(6); p.empower_time = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(7); p.MP = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(8); p.empowered_MP = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(9); p.type = (int)(cell == null ? 0 : cell.NumericCellValue);
						s.list.Add (p);
					}
					data.sheets.Add(s);
				}
			}

			ScriptableObject obj = AssetDatabase.LoadAssetAtPath (exportPath, typeof(ScriptableObject)) as ScriptableObject;
			EditorUtility.SetDirty (obj);
		}
	}
}
