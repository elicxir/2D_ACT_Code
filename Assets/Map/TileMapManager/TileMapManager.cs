using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : Managers_MainGame
{
    [SerializeField] GameObject Lights;

    float TimeRatio {
        get
        {
            return TimeRatioVar;
        }
        set
        {
            tilemap.animationFrameRate = value;
            TimeRatioVar = value;
        }
    }

    float TimeRatioVar = 1;


    public override void GameExit()
    {
        //éûä‘í‚é~èàóù
        TimeRatio = 0;
    }
    public override void GameEnter()
    {
        //éûä‘êiçsë¨ìxÇ1Ç…Ç∑ÇÈ
        TimeRatio = 1;
    }

    public List<TileMapLightingData> tileMapLightingDatas;

    public List<TileMapLighting> tileMapLightings;


    [ContextMenu("SetLighting")]
    void SetLighting()
    {
        foreach(var item in tileMapLightingDatas)
        {
            foreach(var tm in tilemaps)
            {
                TileMapPlaceLight(tm, item);
            }
        }
    }

    [ContextMenu("removeLighting")]
    void Remove()
    {
        foreach (var item in tileMapLightings)
        {
            DestroyImmediate(item.gameObject);
        }

        tileMapLightings.Clear();
    }


    void TileMapPlaceLight(Tilemap tilemap, TileMapLightingData data)
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int cellPosition = new Vector3Int(pos.x, pos.y, pos.z);

            if (tilemap.HasTile(cellPosition))
            {
                TileBase tile = tilemap.GetTile(cellPosition);

                if (tile == data.tile)
                {
                    Vector2 tilepos = new Vector2(cellPosition.x, cellPosition.y) * 16+Vector2.one*8;

                    print("sameTile");
                    PlaceLight(tilepos, data);
                }
            }
        }
    }

    void PlaceLight(Vector2 pos, TileMapLightingData data)
    {
        TileMapLighting light = Instantiate(data.light, pos+data.offset, Quaternion.Euler(0, 0, 0), Lights.transform);
        tileMapLightings.Add(light);
    }


    public override void OnSectionChanged(Vector2Int newSction)
    {
        foreach (TileMapLighting tml in tileMapLightings)
        {
            if (Map.InSameSectionForActivate(newSction, tml.pos))
            {

                tml.gameObject.SetActive(true);
            }
            else
            {
                if (tml.gameObject.activeSelf)
                {
                    tml.gameObject.SetActive(false);
                }
            }
        }


    }



    float time = 0;
    public override void ManagerUpdater(MainGame caller)
    {
        time += Time.deltaTime * TimeRatio;

        foreach(TileMapLighting tml in tileMapLightings)
        {
            tml.Updater(time);
        }
    }



    public Tilemap tilemap;

    public Tilemap[] tilemaps;

    private void OnValidate()
    {
        tilemaps = GetComponentsInChildren<Tilemap>();
    }

}

