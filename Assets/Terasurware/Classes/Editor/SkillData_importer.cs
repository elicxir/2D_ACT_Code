using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class SkillData_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Manager/item/SkillData/SkillData.xlsx";
	private static readonly string exportPath = "Assets/Manager/item/SkillData/SkillData.asset";
	private static readonly string[] sheetNames = { "Sheet1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			SkillDataSheet data = (SkillDataSheet)AssetDatabase.LoadAssetAtPath (exportPath, typeof(SkillDataSheet));
			if (data == null) {
				data = ScriptableObject.CreateInstance<SkillDataSheet> ();
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

					SkillDataSheet.Sheet s = new SkillDataSheet.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						SkillDataSheet.Param p = new SkillDataSheet.Param ();
						
					cell = row.GetCell(0); p.name = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(1); p.desc = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p.active = (cell == null ? false : cell.BooleanCellValue);
					cell = row.GetCell(3); p.weapon = (cell == null ? false : cell.BooleanCellValue);
					cell = row.GetCell(4); p.inc_desc = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(5); p.flavor1 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(6); p.flavor2 = (cell == null ? "" : cell.StringCellValue);
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
