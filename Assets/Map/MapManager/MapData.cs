using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConsts;

[CreateAssetMenu(menuName = "MyScriptable/MapData")]
public class MapData : ScriptableObject
{
    public List<AreaData> AreaData;

    [Header("以下生成したデータ")]
    public List<GridData> GridData;
    public List<WallData> WallData;
    public List<DotData> DotData;

    public GridData GetFromPos(Vector2Int pos)
    {
        foreach (var item in GridData)
        {
            if (item.Pos == pos)
            {
                return item;
            }
        }
        Debug.LogWarning("null");
        return null;
    }

    public RectInt GetRectFromGrid(GridData grid)
    {
        Vector2Int rightup = grid.Pos * (Vector2Int.up * GameConsts.Game.height + Vector2Int.right * GameConsts.Game.width);
        Vector2Int leftdown = rightup - (Vector2Int.up * GameConsts.Game.height + Vector2Int.right * GameConsts.Game.width);


        RectInt rect = new RectInt();
        rect.SetMinMax(leftdown, rightup);

        return rect;
    }

    public WallData GetFromPos(Vector2Int pos, WallData.WallDir dir)
    {
        foreach (var item in WallData)
        {
            if (item.Pos == pos && item.wallDir == dir)
            {
                return item;
            }
        }
        return null;
    }

    public Color GetColor(int index)
    {
        if (index == -1)
        {
            return new Color(0.3f,0.3f,0.3f);
        }
        else
        {
            return AreaData[index].AreaColor;
        }
    }

    void WallSetting(WallData wall, int grid1_index, int grid2_index)
    {
        GridData grid1;
        if (grid1_index != -1)
        {
            grid1 = GridData[grid1_index];
        }
        else
        {
            grid1 = null;
        }
        GridData grid2;
        if (grid2_index != -1)
        {
            grid2 = GridData[grid2_index];
        }
        else
        {
            grid2 = null;
        }


        if (grid1 != null && grid2 != null)
        {
            if (grid1.SectionIndex == grid2.SectionIndex)
            {
                wall.wallType = global::WallData.WallType.None;
                wall.colorindex = grid1.AreaIndex;
            }
            else
            {

                if (Wall_or_Path(grid1, grid2))
                {
                    wall.wallType = global::WallData.WallType.Path;
                    if (grid1.AreaIndex == grid2.AreaIndex)
                    {
                        wall.colorindex = grid1.AreaIndex;
                    }
                    else
                    {
                        wall.colorindex = -1;

                    }
                }
                else
                {
                    wall.wallType = global::WallData.WallType.Wall;

                }
            }
        }
        else
        {
            wall.wallType = global::WallData.WallType.Wall;
        }
    }

    bool Wall_or_Path(GridData grid1, GridData grid2)
    {
        RectInt grid1Rect = GetRectFromGrid(grid1);

        Vector2Int Start;
        Vector2Int End;

        if (grid2.Pos - grid1.Pos == Vector2Int.right)
        {
            Start = new Vector2Int(grid1Rect.xMax, grid1Rect.yMax);
            End = new Vector2Int(grid1Rect.xMax, grid1Rect.yMin);

        }
        else if (grid2.Pos - grid1.Pos == Vector2Int.left)
        {
            Start = new Vector2Int(grid1Rect.xMin, grid1Rect.yMax);
            End = new Vector2Int(grid1Rect.xMin, grid1Rect.yMin);

        }
        else if (grid2.Pos - grid1.Pos == Vector2Int.up)
        {
            Start = new Vector2Int(grid1Rect.xMax, grid1Rect.yMax);
            End = new Vector2Int(grid1Rect.xMin, grid1Rect.yMax);
        }
        else if (grid2.Pos - grid1.Pos == Vector2Int.down)
        {
            Start = new Vector2Int(grid1Rect.xMax, grid1Rect.yMin);
            End = new Vector2Int(grid1Rect.xMin, grid1Rect.yMin);
        }
        else
        {
            Debug.LogError("error");
            return false;
        }
        return TerrainCheck(Start, End);
    }


    bool TerrainCheck(Vector2 start, Vector2 end)
    {
        LayerMask Terrain = 1 << 8;

        int max = Mathf.CeilToInt((end - start).magnitude);

        for (int i = 0; i < max; i++)
        {
            Vector2 center = Vector2.Lerp(start, end, (float)i / max);

            Collider2D collider = Physics2D.OverlapBox(center, Vector2.one * 16, 0, Terrain);

            if (collider == null)
            {
                return true;
            }

        }


        return false;
    }


    //Dotから隣接する辺を得る
    WallData[] GetWall(DotData dot)
    {
        WallData up = GetFromPos(dot.Pos + Vector2Int.up, global::WallData.WallDir.Vertical);
        WallData down = GetFromPos(dot.Pos, global::WallData.WallDir.Vertical);
        WallData right = GetFromPos(dot.Pos + Vector2Int.right, global::WallData.WallDir.Horizontal);
        WallData left = GetFromPos(dot.Pos, global::WallData.WallDir.Horizontal);

        int GetIndex(WallData d)
        {
            if (d == null)
            {
                return -1;
            }
            else
            {
                return WallData.IndexOf(d);
            }
        }


        dot.wallindexes = new int[4] { GetIndex(up), GetIndex(down), GetIndex(right), GetIndex(left) };

        return new WallData[4] { up, down, right, left };
    }

    void setCornerData(DotData dot)
    {
        int c = 0;

        foreach (var item in GetWall(dot))
        {
            if (item != null)
            {
                if (item.wallType != global::WallData.WallType.None)
                {
                    c++;
                }
            }
        }

        if (c > 1)
        {
            dot.isWall = true;
        }
        else
        {
            dot.isWall = false;
            foreach (var item in GetWall(dot))
            {
                if (item.wallType == global::WallData.WallType.None)
                {
                    dot.colorindex = item.colorindex;
                }
            }
        }
    }


    private void OnValidate()
    {
        DataFunction();
    }


    [ContextMenu("GenerateFunction")]
    void DataFunction()
    {
        GridData.Clear();
        WallData.Clear();
        DotData.Clear();

        void AddWallData(WallData data)
        {
            foreach (var item in WallData)
            {
                if (item.Pos == data.Pos && item.wallDir == data.wallDir)
                {
                    return;
                }
            }
            WallData.Add(data);
        }

        void AddDotData(DotData data)
        {
            foreach (var item in DotData)
            {
                if (item.Pos == data.Pos)
                {
                    return;
                }
            }
            DotData.Add(data);
        }

        //グリッドのデータを生成する
        for (int i = 0; i < AreaData.Count; i++)
        {
            int sI = 0;
            foreach (var data in AreaData[i].SectionDatas)
            {

                for (int x = data.Extent.x; x < data.Extent.x + data.Extent.width; x++)
                {
                    for (int y = data.Extent.y; y < data.Extent.y + data.Extent.height; y++)
                    {
                        GridData gd = new GridData
                        {
                            Pos = new Vector2Int(x, y),
                            AreaIndex = i,
                            SectionIndex = sI

                        };
                        GridData.Add(gd);
                    }

                }
                sI++;
            }
        }

        //各グリッドに対して四方の壁を重複しないように生成する
        for (int i = 0; i < AreaData.Count; i++)
        {
            foreach (var data in AreaData[i].SectionDatas)
            {
                for (int x = data.Extent.x; x < data.Extent.x + data.Extent.width; x++)
                {
                    for (int y = data.Extent.y; y < data.Extent.y + data.Extent.height; y++)
                    {
                        WallData Up = new WallData
                        {
                            wallDir = global::WallData.WallDir.Horizontal,
                            Pos = new Vector2Int(x, y)
                        };
                        WallData Down = new WallData
                        {
                            wallDir = global::WallData.WallDir.Horizontal,
                            Pos = new Vector2Int(x, y - 1)
                        };
                        WallData Right = new WallData
                        {
                            wallDir = global::WallData.WallDir.Vertical,
                            Pos = new Vector2Int(x, y)
                        };
                        WallData Left = new WallData
                        {
                            wallDir = global::WallData.WallDir.Vertical,
                            Pos = new Vector2Int(x - 1, y)
                        };

                        AddWallData(Up);
                        AddWallData(Down);
                        AddWallData(Right);
                        AddWallData(Left);
                    }
                }
            }
        }


        //各壁の状態を設定する
        for (int i = 0; i < WallData.Count; i++)
        {
            WallData data = WallData[i];

            switch (data.wallDir)
            {
                case global::WallData.WallDir.Vertical:

                    if (GetFromPos(data.Pos) != null)
                    {
                        data.gridindex1 = GridData.IndexOf(GetFromPos(data.Pos));
                    }
                    else
                    {
                        data.gridindex1 = -1;
                    }

                    if (GetFromPos(data.Pos + Vector2Int.right) != null)
                    {
                        data.gridindex2 = GridData.IndexOf(GetFromPos(data.Pos + Vector2Int.right));
                    }
                    else
                    {
                        data.gridindex2 = -1;
                    }
                    break;

                case global::WallData.WallDir.Horizontal:
                    if (GetFromPos(data.Pos) != null)
                    {
                        data.gridindex1 = GridData.IndexOf(GetFromPos(data.Pos));
                    }
                    else
                    {
                        data.gridindex1 = -1;
                    }

                    if (GetFromPos(data.Pos + Vector2Int.up) != null)
                    {
                        data.gridindex2 = GridData.IndexOf(GetFromPos(data.Pos + Vector2Int.up));
                    }
                    else
                    {
                        data.gridindex2 = -1;
                    }
                    break;
            }
            WallSetting(data, data.gridindex1, data.gridindex2);

        }

        //各グリッドに対して四方の頂点を重複しないように生成する
        for (int i = 0; i < AreaData.Count; i++)
        {
            foreach (var data in AreaData[i].SectionDatas)
            {
                for (int x = data.Extent.x; x < data.Extent.x + data.Extent.width; x++)
                {
                    for (int y = data.Extent.y; y < data.Extent.y + data.Extent.height; y++)
                    {
                        DotData Up = new DotData
                        {
                            Pos = new Vector2Int(x, y)
                        };
                        DotData Down = new DotData
                        {
                            Pos = new Vector2Int(x, y - 1)
                        };
                        DotData Right = new DotData
                        {
                            Pos = new Vector2Int(x - 1, y - 1)
                        };
                        DotData Left = new DotData
                        {
                            Pos = new Vector2Int(x - 1, y)
                        };

                        AddDotData(Up);
                        AddDotData(Down);
                        AddDotData(Right);
                        AddDotData(Left);

                    }

                }
            }
        }

        //各頂点に対して状態を設定する
        for (int i = 0; i < DotData.Count; i++)
        {
            setCornerData(DotData[i]);
        }

    }



}


[System.Serializable]
public class WallData
{
    public int colorindex;

    public int gridindex1;
    public int gridindex2;


    public Vector2Int Pos;//このgridの右と上のいずれかがこの壁である
    public WallDir wallDir;

    public WallType wallType;
    public enum WallType
    {
        Wall, Path, None, Breakale
    }

    public enum WallDir
    {
        Vertical, Horizontal
    }


}


[System.Serializable]
public class DotData
{
    public Vector2Int Pos;//このgridの右上の頂点がこの点である
    public bool isWall;
    public int colorindex;

    public int[] wallindexes;

}



[System.Serializable]
public class GridData
{
    public int AreaIndex;
    public int SectionIndex;
    public Vector2Int Pos;
}