using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapGrid : MonoBehaviour
{
    protected Player player
    {
        get
        {
            return Define.PM.Player;
        }
    }

    int tile = 1;
    int areawidth = 24;
    int areaheight = 18;

    int width = 80;
    int height = 40;

    [SerializeField] Color gridline;

    [SerializeField] MiniMapBlock[] Blocks;

    [SerializeField] RectTransform PlayerIcon;

    [SerializeField] RectTransform MiniMap;


    List<BlockData> BlockDatas = new List<BlockData>();





    void SetBlockDatas()
    {/*
        List<MapSection> sectionsdata = Define.MAP.GetMapSectionList();

        foreach (MapSection data in sectionsdata)
        {
            for (int y = data.down; y <= data.up; y++)
            {
                for (int x = data.left; x <= data.right; x++)
                {
                    BlockData Block = new BlockData
                    {
                        Pos = new Vector2Int(x, y),
                        wall_up = 0,
                        wall_down = 0,
                        wall_left = 0,
                        wall_right = 0,
                        color = data.color
                    };


                    if (x < data.right)
                    {
                        Block.wall_right = 2;
                    }
                    if (x > data.left)
                    {
                        Block.wall_left = 2;
                    }
                    if (y < data.up)
                    {
                        Block.wall_up = 2;
                    }
                    if (y > data.down)
                    {
                        Block.wall_down = 2;
                    }

                    BlockDatas.Add(Block);

                }
            }
        
        }*/

    }


    /// <summary>
    /// セーブデータ読み込み時の初期化
    /// </summary>
    public void Init()
    {
        /*
        WalkData data = Define.SDM.GameData.PlayerWalkData;


        foreach(SaveDataManager.PersonalData.PathData path in data.PathDatas)
        {
            MakePath(path.sec1, path.sec2);
        }

        RefleshWalkData();*/
    }


    class BlockData
    {
        public Vector2Int Pos;

        public int wall_up;
        public int wall_down;
        public int wall_right;
        public int wall_left;

        public Color color;
    }

    int Mult = 1;

    private void Awake()
    {
        Blocks = FindObjectsOfType<MiniMapBlock>();
        SetBlockDatas();

    }

    private void Start()
    {
        SetCordinate();

        for (int e = 0; e < BlockDatas.Count; e++)
        { 
            Blocks[e].SetWall();
        }
    }

    private void Update()
    {
        PlayerIcon.sizeDelta = new Vector2(3 * Mult, 3 * Mult);
        PlayerIcon.anchoredPosition = new Vector2((player.Position.x + 240) * Mult * 0.05f, (player.Position.y + 180) * Mult * 0.05f);

        MiniMap.anchoredPosition = new Vector2(-(player.Position.x + 240) * Mult * 0.05f, -(player.Position.y + 180) * Mult * 0.05f);
    }



    public void MakePath(Vector2Int sec1, Vector2Int sec2)
    {
        Vector2Int direction = sec2 - sec1;

        if (direction.magnitude==1)
        {
            foreach (BlockData data in BlockDatas)
            {

                if (data.Pos == sec1)
                {
                    if (direction.y > 0)
                    {
                        if (data.wall_up == 0)
                        {
                            data.wall_up = 1;
                        }
                    }
                    if (direction.y < 0)
                    {
                        if (data.wall_down == 0)
                        {
                            data.wall_down = 1;
                        }
                    }
                    if (direction.x > 0)
                    {
                        if (data.wall_right == 0)
                        {
                            data.wall_right = 1;
                        }
                    }
                    if (direction.x < 0)
                    {
                        if (data.wall_left == 0)
                        {
                            data.wall_left = 1;
                        }
                    }
                }

                if (data.Pos == sec2)
                {
                    if (direction.y < 0)
                    {
                        if (data.wall_up == 0)
                        {
                            data.wall_up = 1;
                        }
                    }
                    if (direction.y > 0)
                    {
                        if (data.wall_down == 0)
                        {
                            data.wall_down = 1;
                        }
                    }
                    if (direction.x < 0)
                    {
                        if (data.wall_right == 0)
                        {
                            data.wall_right = 1;
                        }
                    }
                    if (direction.x > 0)
                    {
                        if (data.wall_left == 0)
                        {
                            data.wall_left = 1;
                        }
                    }
                }

            }

            
        }

        
    }


    Vector2Int BaseRect = new Vector2Int(24, 18);

    Vector2Int BlockRect
    {
        get
        {
            return BaseRect * Mult;
        }
    }

    void SetCordinate()
    {
        for (int e = 0; e < BlockDatas.Count; e++)
        {
            Blocks[e].Mult = Mult;
            Blocks[e].transform.position = new Vector3((BlockDatas[e].Pos.x) * BlockRect.x, (BlockDatas[e].Pos.y) * BlockRect.y);

            Blocks[e].up = BlockDatas[e].wall_up;
            Blocks[e].down = BlockDatas[e].wall_down;
            Blocks[e].left = BlockDatas[e].wall_left;
            Blocks[e].right = BlockDatas[e].wall_right;
            Blocks[e].color_Visited = BlockDatas[e].color;
            Blocks[e].isVisible = false;
            Blocks[e].isVisited = false;
        }
    }

    public void RefleshWalkData()
    {
        /*
        WalkData data = Define.SDM.GameData.PlayerWalkData;

        for (int e = 0; e < BlockDatas.Count; e++)
        {
            if (data.VisibleCell.Contains(BlockDatas[e].Pos))
            {
                Blocks[e].isVisible = true;
            }
            if (data.VisitedCell.Contains(BlockDatas[e].Pos))
            {
                Blocks[e].isVisited = true;
            }

            Blocks[e].up = BlockDatas[e].wall_up;
            Blocks[e].down = BlockDatas[e].wall_down;
            Blocks[e].left = BlockDatas[e].wall_left;
            Blocks[e].right = BlockDatas[e].wall_right;

            Blocks[e].SetWall();

        }*/
    }
}

