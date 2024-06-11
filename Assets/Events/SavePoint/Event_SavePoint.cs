using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using GameConsts;

public class Event_SavePoint : Event
{
    public string SavePointName;
    public  SpriteRenderer sr;

    [SerializeField] Sprite[] sprites;

    private void OnValidate()
    {
        sr= GetComponent<SpriteRenderer>();

        EC_Save save = GetComponent<EC_Save>();
        save.SavePointName = SavePointName;

        EC_Branch_Flag f = GetComponentInChildren<EC_Branch_Flag>();
        f.Flag = SavePointName;

        EC_GameFlag g= GetComponentInChildren<EC_GameFlag>();
        g.data.FlagID = SavePointName;
    }

    [ContextMenu("setdata")]
    void SetData()
    {
        MapManager map = FindObjectOfType<MapManager>();

        if (map != null)
        {
            SavePointName=map.GetAreaName(map.SectionIndex(NowMapGrid));
        }
    }

    public Vector2Int NowMapGrid
    {
        get
        {
            int mapx = (int)transform.position.x / Game.width + 1;
            int mapy = (int)transform.position.y / Game.height + 1;

            return new Vector2Int(mapx, mapy);
        }
    }

    public override void Init()
    {
        GameFlag gameFlag = GM.Game.PlayData.GetGameFlag(SavePointName);

        if (gameFlag.isTrue)
        {
            sr.sprite = sprites[0];
        }
        else
        {
            sr.sprite = sprites[1];

        }

    }

}
