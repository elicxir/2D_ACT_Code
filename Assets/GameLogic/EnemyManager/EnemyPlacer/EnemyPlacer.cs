using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using GameConsts;

//エネミー設置情報:エリア進入時に一度だけ生成される
public class EnemyPlacer : MonoBehaviour
{
    public List<EnemyPlaceData> enemyPlaceDatas;

    EnemyManager EM;

    void NullCheck()
    {
        if (EM == null)
        {
            EM = FindObjectOfType<EnemyManager>();
        }
    }

    private void OnValidate()
    {
        NullCheck();

        foreach (EnemyPlaceData epd in enemyPlaceDatas)
        {
            if (epd.enemyMarker != null)
            {
                epd.enemyMarker.SetSprite(epd, EM);
            }
            epd.Validater(EM);
        }
    }

    protected void OnDrawGizmos()
    {
        DrawBehaviorRect();
    }

    protected void DrawBehaviorRect()
    {
        Gizmos.color = new Color(1, 1, 1, 0.5f);
        foreach (EnemyPlaceData epd in enemyPlaceDatas)
        {
            if (epd.enemyMarker != null)
            {
                Gizmos.DrawWireCube(epd.RealBehaviorRect.position, epd.RealBehaviorRect.size);
            }
        }
    }



    public void Init()
    {
        foreach (EnemyPlaceData epd in enemyPlaceDatas)
        {
            if (epd.enemyMarker != null)
            {
                epd.enemyMarker.spriteRenderer.enabled = false;
            }
        }

        InitFunction();
    }

    protected virtual void InitFunction()
    {
        foreach (EnemyPlaceData epd in enemyPlaceDatas)
        {
            if (epd.enemyMarker != null)
            {
                Place(epd, epd.enemyMarker.Position);
            }
            else
            {
                Place(epd);
            }
        }
    }

    [ContextMenu("SetMarker")]
    void SetMarker()
    {
        for (int i = 0; i <enemyPlaceDatas.Count; i++)
        {
            enemyPlaceDatas[i].enemyMarker = null;
        }

        EnemyPlaceMarker[] markers = GetComponentsInChildren<EnemyPlaceMarker>();

        for(int i = 0; i < Mathf.Min(markers.Length, enemyPlaceDatas.Count); i++)
        {
            enemyPlaceDatas[i].enemyMarker = markers[i];
        }


        foreach (EnemyPlaceData epd in enemyPlaceDatas)
        {
            if (epd.enemyMarker != null)
            {
                epd.enemyMarker.SetSprite(epd, EM);
            }
            epd.Validater(EM);
        }
    }






    //Place関数により敵を配置する
    protected Enemy Place(EnemyPlaceData data)
    {
        return Place(data,Position);
    }
    protected Enemy Place(EnemyPlaceData data, Vector2 pos)
    {
        if (data.placeStatusData.facePlayer)
        {
            if ((GM.Player.Player.Position - pos).x >= 0)
            {
                return GM.Enemy.Appear(data.placeStatusData.enemyName, data.placeStatusData.VariantIndex, pos, Entity.FaceDirection.Right, data.RealBehaviorRect);
            }
            else
            {
                return GM.Enemy.Appear(data.placeStatusData.enemyName, data.placeStatusData.VariantIndex, pos, Entity.FaceDirection.Left, data.RealBehaviorRect);
            }
        }
        else
        {
            return GM.Enemy.Appear(data.placeStatusData.enemyName, data.placeStatusData.VariantIndex, pos,data.placeStatusData.face, data.RealBehaviorRect);
        }
    }



    public virtual void Updater()
    {

    }

    protected Vector2 Position
    {
        get
        {
            return transform.position;
        }
    }

    public Vector2Int MapGrid
    {
        get
        {
            int mapx = (int)(Position.x) / Game.width + 1;
            int mapy = (int)(Position.y) / Game.height + 1;

            return new Vector2Int(mapx, mapy);

        }
    }

}

[System.Serializable]
public class EnemyPlaceData
{
    public string EnemyName;
    public EnemyPlaceMarker enemyMarker;
    public EnemyPlaceStatusData placeStatusData;
    public void Validater(EnemyManager em)
    {
        EnemyName = em.GetName(placeStatusData.enemyName, placeStatusData.VariantIndex);

        if (placeStatusData.face == Entity.FaceDirection.Left && placeStatusData.facePlayer)
        {
            placeStatusData.face = Entity.FaceDirection.Right;
        }
    }

    public Rect RealBehaviorRect
    {
        get
        {
            Rect rect = new Rect();
            rect.size = placeStatusData.BehaviorRect.size;
            if (enemyMarker != null)
            {
                rect.position = placeStatusData.BehaviorRect.position + enemyMarker.Position;
            }
            else
            {
                rect.position = placeStatusData.BehaviorRect.position;
            }
            return rect;
        }
    }
}