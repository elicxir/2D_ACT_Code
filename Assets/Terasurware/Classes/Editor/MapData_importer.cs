using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class MapData_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Map/MapManager/MapData.xlsx";
	private static readonly string exportPath = "Assets/Map/MapManager/MapData.asset";
	private static readonly string[] sheetNames = { "Sheet1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			MapDataSheet data = (MapDataSheet)AssetDatabase.LoadAssetAtPath (exportPath, typeof(MapDataSheet));
			if (data == null) {
				data = ScriptableObject.CreateInstance<MapDataSheet> ();
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

					MapDataSheet.Sheet s = new MapDataSheet.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						MapDataSheet.Param p = new MapDataSheet.Param ();
						
					cell = row.GetCell(0); p.mapname = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(1); p.areaname = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p.nameflag = (cell == null ? false : cell.BooleanCellValue);
					cell = row.GetCell(3); p.up = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.down = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(5); p.right = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(6); p.left = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(7); p.hidden = (cell == null ? false : cell.BooleanCellValue);
					cell = row.GetCell(8); p.R = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(9); p.G = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(10); p.B = (int)(cell == null ? 0 : cell.NumericCellValue);
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
