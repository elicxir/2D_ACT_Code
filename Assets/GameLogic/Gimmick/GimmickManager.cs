using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GimmickManager : Managers_MainGame
{
    [SerializeField] GameObject Event_and_Gimmick;
    [SerializeField] GameObject Grids;

    [SerializeField] Gimmick[] Gimmicks;
    [SerializeField] Gimmick[] GimmicksGrid;


    public override void OnSectionChanged(Vector2Int newGrid)
    {
        ActivateGimmicks(newGrid);
    }

    void ActivateGimmicks(Vector2Int newGrid)
    {
        foreach (Gimmick gimmick in Gimmicks)
        {
            Vector2Int P = new Vector2Int(Mathf.RoundToInt(gimmick.Pos.x), Mathf.RoundToInt(gimmick.Pos.y));

            if (Map.InSameSectionForActivate(newGrid, P))
            {
                gimmick.gameObject.SetActive(true);
                gimmick.Init();
            }
            else
            {
                if (gimmick.gameObject.activeSelf)
                {
                    gimmick.gameObject.SetActive(false);
                }
            }
        }

        foreach (Gimmick gimmick in GimmicksGrid)
        {
            if (Map.SectionIndex(newGrid) == Map.SectionIndex(gimmick.MapGrid))
            {
                gimmick.gameObject.SetActive(true);
                gimmick.Init();
            }
            else
            {
                if (gimmick.gameObject.activeSelf)
                {
                    gimmick.gameObject.SetActive(false);
                }
            }
        }

    }

    public bool EntityUpdateFlag(Vector2 pos,int detectrange)
    {
        foreach(UpdateCircle uc in updateCircles)
        {
            if (uc.inRange(pos,detectrange))
            {
                return true;
            }
        }
        return false;
    }

    struct UpdateCircle
    {
        public Vector2 center;
        public int Range;
        public UpdateCircle(Vector2 c, int r)
        {
            center = c;
            Range = r;
        }
        public bool inRange(Vector2 pos, int detectrange)
        {
            return !((pos - center).sqrMagnitude > (Range+ detectrange) * (Range + detectrange));
        }
    }

    List<UpdateCircle> updateCircles=new List<UpdateCircle>();

    public override void ManagerUpdater(MainGame caller)
    {
        updateCircles.Clear();

        foreach (Gimmick gimmick in Gimmicks)
        {
            if (gimmick.gameObject.activeSelf)
            {
                if (gimmick.Updater())
                {
                    if (gimmick.UpdateRange > 0)
                    {
                        updateCircles.Add(new UpdateCircle(gimmick.Position, gimmick.UpdateRange));
                    }
                }
            }
        }
        foreach (Gimmick gimmick in GimmicksGrid)
        {
            if (gimmick.gameObject.activeSelf)
            {
                if (gimmick.Updater())
                {
                    if (gimmick.UpdateRange > 0)
                    {
                        updateCircles.Add(new UpdateCircle(gimmick.Position, gimmick.UpdateRange));
                    }
                }

            }
        }

    }


    public override void ManagerLoadInit(MainGame caller)
    {
        GetGimmicks2();

    }

    private void OnValidate()
    {
        GetGimmicks();
    }

    void GetGimmicks()
    {
        Gimmick[] G1 = Event_and_Gimmick.GetComponentsInChildren<Gimmick>();

        Gimmicks = G1;
    }

    void GetGimmicks2()
    {
        GimmicksGrid = Grids.GetComponentsInChildren<Gimmick>(true);
    }

}
