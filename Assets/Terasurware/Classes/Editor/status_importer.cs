using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class status_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Entity/Player/status.xlsx";
	private static readonly string exportPath = "Assets/Entity/Player/status.asset";
	private static readonly string[] sheetNames = { "Sheet1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			PlayerStatusSheet data = (PlayerStatusSheet)AssetDatabase.LoadAssetAtPath (exportPath, typeof(PlayerStatusSheet));
			if (data == null) {
				data = ScriptableObject.CreateInstance<PlayerStatusSheet> ();
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

					PlayerStatusSheet.Sheet s = new PlayerStatusSheet.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						PlayerStatusSheet.Param p = new PlayerStatusSheet.Param ();
						
					cell = row.GetCell(0); p.level = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.hp = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(2); p.mp = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(3); p.attack = (int)(cell == null ? 0 : cell.NumericCellValue);
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
