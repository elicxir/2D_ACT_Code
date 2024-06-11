using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class CatalystData_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Manager/item/CatalystData/CatalystData.xlsx";
	private static readonly string exportPath = "Assets/Manager/item/CatalystData/CatalystData.asset";
	private static readonly string[] sheetNames = { "Sheet1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			CatalystDataSheet data = (CatalystDataSheet)AssetDatabase.LoadAssetAtPath (exportPath, typeof(CatalystDataSheet));
			if (data == null) {
				data = ScriptableObject.CreateInstance<CatalystDataSheet> ();
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

					CatalystDataSheet.Sheet s = new CatalystDataSheet.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						CatalystDataSheet.Param p = new CatalystDataSheet.Param ();
						
					cell = row.GetCell(0); p.name = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(1); p.flavor1 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p.flavor2 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(3); p.flavor3 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(4); p.passive = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(5); p.p_desc1 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(6); p.p_desc2 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(7); p.p_desc3 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(8); p.directional = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(9); p.d_desc1 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(10); p.d_desc2 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(11); p.d_desc3 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(12); p.d_mp = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(13); p.another = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(14); p.a_desc1 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(15); p.a_desc2 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(16); p.a_desc3 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(17); p.a_mp = (float)(cell == null ? 0 : cell.NumericCellValue);
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
