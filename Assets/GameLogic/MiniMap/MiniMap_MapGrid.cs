using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConsts;
using Managers;

public class MiniMap_MapGrid : MonoBehaviour
{
    public Canvas canvas;
    public RectTransform rect;

    [SerializeField] List<MiniMap_MapBlock> MapBlocks;
    [SerializeField] List<MiniMap_Wall> MapWalls;
    [SerializeField] List<MiniMap_Dot> MapDots;

    [SerializeField] MapManager MM;


    [SerializeField] MiniMap_PlayerMarker PlayerMarker;

    int width = Map.Cell_Holizontal;
    int height = Map.Cell_Vertical;

    
    enum Scale
    {
        //x1ÇÕ16x12
        x1, x2, x3, x4, x5
    }

    Scale nowScale = Scale.x1;

    public void Init(int scale)
    {
        rect.localScale = new Vector3(scale, scale, 1);
        SetPlayerMarker(GM.Player.Player.NowMapGrid);
        //rect.anchoredPosition = -PlayerMarker.Pos* scale;
    }

    const int wall = 1;



    public void SetPlayerMarker(Vector2Int playerPos)
    {
        Vector2Int pos = new Vector2Int(Mathf.RoundToInt(playerPos.x / 80), Mathf.RoundToInt(playerPos.y / 80));

        PlayerMarker.Pos = Vector2.right * (Prefab.rect.sizeDelta.x + wall) * (playerPos.x) + Vector2.up * (Prefab.rect.sizeDelta.y + wall) * (playerPos.y);

    }

    public void SetPos(Vector2 pos)
    {
        RectTransform rect = this.transform as RectTransform;
        rect.anchoredPosition = pos;
    }

    public Vector2 Origin;
    [SerializeField] Vector2Int CellSize;





    //ì•îjèÛãµÇÃîΩâf
    public void SetMapState()
    {

    }














    [SerializeField] MiniMap_MapBlock Prefab;
    [SerializeField] MiniMap_Wall Wall;
    [SerializeField] MiniMap_Dot Dot;


    [SerializeField] GameObject Blocks;
    [SerializeField] GameObject Walls;
    [SerializeField] GameObject Dots;

    [SerializeField] MapData mapData;

    public Vector2Int size;

    [ContextMenu("Set")]
    void Set()
    {

        {
            MapBlocks = new List<MiniMap_MapBlock>(GetComponentsInChildren<MiniMap_MapBlock>());
            int AllNum = mapData.GridData.Count;

            if (MapBlocks.Count < AllNum)
            {
                do
                {
                    MiniMap_MapBlock block = Instantiate(Prefab, Blocks.transform);
                    MapBlocks.Add(block);
                } while (MapBlocks.Count < AllNum);
            }

            for (int i = 0; i < AllNum; i++)
            {
                MapBlocks[i].colorChange = mapData.AreaData[mapData.GridData[i].AreaIndex].AreaColor;
                MapBlocks[i].rect.anchoredPosition = Vector2.right * (MapBlocks[i].rect.sizeDelta.x + wall) * (mapData.GridData[i].Pos.x) + Vector2.up * (MapBlocks[i].rect.sizeDelta.y + wall) * (mapData.GridData[i].Pos.y);
            }
        }

        {
            MapWalls = new List<MiniMap_Wall>(GetComponentsInChildren<MiniMap_Wall>());
            int AllNum = mapData.WallData.Count;

            if (MapWalls.Count < AllNum)
            {
                do
                {
                    MiniMap_Wall block = Instantiate(Wall, Walls.transform);
                    MapWalls.Add(block);
                } while (MapWalls.Count < AllNum);
            }

            for (int i = 0; i < AllNum; i++)
            {
                switch (mapData.WallData[i].wallDir)
                {
                    case WallData.WallDir.Vertical:
                        MapWalls[i].dir = mapData.WallData[i].wallDir;
                        MapWalls[i].rect.anchoredPosition = Vector2.right * (Prefab.rect.sizeDelta.x + wall) * (mapData.WallData[i].Pos.x) + Vector2.up * (Prefab.rect.sizeDelta.y + wall) * (mapData.WallData[i].Pos.y) + Vector2.right * (Prefab.rect.sizeDelta.x * 0.5f+wall*0.5f);
                        MapWalls[i].SetState(mapData.WallData[i].wallType, mapData.GetColor(mapData.WallData[i].colorindex));
                        break;


                    case WallData.WallDir.Horizontal:
                        MapWalls[i].dir = mapData.WallData[i].wallDir;
                        MapWalls[i].rect.anchoredPosition = Vector2.right * (Prefab.rect.sizeDelta.x + wall) * (mapData.WallData[i].Pos.x) + Vector2.up * (Prefab.rect.sizeDelta.y + wall) * (mapData.WallData[i].Pos.y) + Vector2.up * (Prefab.rect.sizeDelta.y * 0.5f+wall * 0.5f);
                        MapWalls[i].SetState(mapData.WallData[i].wallType, mapData.GetColor(mapData.WallData[i].colorindex));
                        break;
                }
            }
        }

        {
            MapDots = new List<MiniMap_Dot>(GetComponentsInChildren<MiniMap_Dot>());
            int AllNum = mapData.DotData.Count;

            if (MapDots.Count < AllNum)
            {
                do
                {
                    MiniMap_Dot block = Instantiate(Dot, Dots.transform);
                    MapDots.Add(block);
                } while (MapDots.Count < AllNum);
            }

            for (int i = 0; i < AllNum; i++)
            {
                MapDots[i].rect.anchoredPosition = Vector2.right * (Prefab.rect.sizeDelta.x + wall) * (mapData.DotData[i].Pos.x) + Vector2.up * (Prefab.rect.sizeDelta.y + wall) * (mapData.DotData[i].Pos.y) + Vector2.one * (Prefab.rect.sizeDelta.x * 0.5f + wall * 0.5f);
                MapDots[i].SetState(mapData.DotData[i].isWall, mapData.GetColor(mapData.DotData[i].colorindex));

            }
        }
    }

    [ContextMenu("Delete")]
    void Delete()
    {
        for (int i = MapBlocks.Count - 1; i >= 0; i--)
        {
            MiniMap_MapBlock block = MapBlocks[i];
            MapBlocks.RemoveAt(i);
            DestroyImmediate(block.gameObject);
        }
        for (int i = MapWalls.Count - 1; i >= 0; i--)
        {
            MiniMap_Wall block = MapWalls[i];
            MapWalls.RemoveAt(i);
            DestroyImmediate(block.gameObject);
        }

        for (int i = MapDots.Count - 1; i >= 0; i--)
        {
            MiniMap_Dot block = MapDots[i];
            MapDots.RemoveAt(i);
            DestroyImmediate(block.gameObject);
        }
    }

    public float Achievement;

    public void UpdateGrid(List<Vector2Int> visited)
    {
        int adad = 0;
        //ñKÇÍÇΩgridÇï\é¶
        for (int i = 0; i < MapBlocks.Count; i++)
        {
            if (visited.Contains(mapData.GridData[i].Pos))
            {
                MapBlocks[i].isVisited = true;
                adad++;
            }
            else
            {
                MapBlocks[i].isVisited = false;
            }
        }
        int ac = adad * 1000 / MapBlocks.Count;
        Achievement = ac*0.1f;

        //ñKÇÍÇΩgridÇ…ó◊ê⁄Ç∑ÇÈwallÇï\é¶
        for (int i = 0; i < MapWalls.Count; i++)
        {
            int a = mapData.WallData[i].gridindex1;
            int b = mapData.WallData[i].gridindex2;

            if (a != -1 && b != -1)
            {
                if (MapBlocks[a].isVisited || MapBlocks[b].isVisited)
                {
                    MapWalls[i].isVisited = true;
                }
                else
                {
                    MapWalls[i].isVisited = false;
                }
            }
            else if(a != -1)
            {
                if (MapBlocks[a].isVisited)
                {
                    MapWalls[i].isVisited = true;
                }
                else
                {
                    MapWalls[i].isVisited = false;
                }
            }
            else if (b != -1)
            {
                if (MapBlocks[b].isVisited)
                {
                    MapWalls[i].isVisited = true;
                }
                else
                {
                    MapWalls[i].isVisited = false;
                }
            }
        }

        //ï\é¶Ç≥ÇÍÇƒÇ¢ÇÈ2Ç¬à»è„ÇÃwallÇ…ó◊ê⁄Ç∑ÇÈdotÇï\é¶
        for (int i = 0; i < MapDots.Count; i++)
        {
            int s = 0;

            foreach (var item in mapData.DotData[i].wallindexes)
            {
                if (item != -1)
                {
                    if (MapWalls[item].isVisited)
                    {
                        s++;
                    }
                }
            }

            MapDots[i].isVisited = s > 1;

        }










    }
}
