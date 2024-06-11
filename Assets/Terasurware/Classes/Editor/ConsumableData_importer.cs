using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class ConsumableData_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Equipment/Comsumable/BaseSystem/ConsumableData.xlsx";
	private static readonly string exportPath = "Assets/Equipment/Comsumable/BaseSystem/ConsumableData.asset";
	private static readonly string[] sheetNames = { "Sheet1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			ConsumableDataSheet data = (ConsumableDataSheet)AssetDatabase.LoadAssetAtPath (exportPath, typeof(ConsumableDataSheet));
			if (data == null) {
				data = ScriptableObject.CreateInstance<ConsumableDataSheet> ();
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

					ConsumableDataSheet.Sheet s = new ConsumableDataSheet.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						ConsumableDataSheet.Param p = new ConsumableDataSheet.Param ();
						
					cell = row.GetCell(0); p.name = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(1); p.count = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(2); p.desc1 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(3); p.flavor1 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(4); p.flavor2 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(5); p.flavor3 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(6); p.flavor4 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(7); p.flavor5 = (cell == null ? "" : cell.StringCellValue);
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
