using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using GameConsts;
using Managers;

public class MapManager : MonoBehaviour
{
    /// <summary>
    /// Area    sectionの集合　地域の違い
    /// Section gridの集合　カメラ範囲の区切りの単位
    /// Grid    1マス
    /// </summary>

    protected Player player
    {
        get
        {
            return GM.Player.Player;

        }
    }

    [SerializeField] bool ShowSection;
    [SerializeField] bool ShowAreaColor;


    [SerializeField] MapData MapData;

    public List<MapSection> AllSections
    {
        get
        {
            if (AllSectionsVer == null)
            {
                AllSectionsVer = GetMapSectionList();
            }
            return AllSectionsVer;
        }
    }
    List<MapSection> AllSectionsVer;
    List<MapSection> GetMapSectionList()
    {
        List<MapSection> sections = new List<MapSection>();

        for (int i = 0; i < MapData.AreaData.Count; i++)
        {
            for (int j = 0; j < MapData.AreaData[i].SectionDatas.Count; j++)
            {
                sections.Add(MapSection(i, j));
            }
        }

        return sections;
    }






    public int SectionIndex(Vector2Int pos)
    {
        Vector2Int vector = pos;

        for (int n = 0; n < AllSections.Count; n++)
        {
            if (AllSections[n].AreaJudge(vector))
            {
                return n;
            }
        }

        Debug.Log("Not found MapData");
        return -1;

    }




    public bool InSameSection(Vector2Int pos1, Vector2Int pos2)
    {
        foreach (MapSection section in AllSections)
        {
            if (section.AreaJudgePoint(pos1) && section.AreaJudgePoint(pos2))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// pos1にはグリッド　pos2には座標
    /// </summary>
    public bool InSameSectionForActivate(Vector2Int pos1, Vector2 pos2)
    {
        foreach (MapSection section in AllSections)
        {
            if (section.AreaJudge(pos1) && section.AreaJudgePoint(new Vector2Int(Mathf.RoundToInt(pos2.x), Mathf.RoundToInt(pos2.y))))
            {
                return true;
            }
        }
        return false;
    }

    MapSection MapSection(int areaindex, int index)
    {
        index = Mathf.Max(0, index);

        MapSection data = new MapSection
        {
            mapname = MapData.AreaData[areaindex].AreaName,
            extent = MapData.AreaData[areaindex].SectionDatas[index].Extent,
            color = MapData.AreaData[areaindex].AreaColor
        };

        return data;

    }

    public string GetAreaName(int index)
    {
        index = Mathf.Clamp(index, 0, AllSections.Count - 1);
        return AllSections[index].mapname;
    }


    int NowNum
    {
        get
        {
            Vector2Int vector = player.NowMapGrid;

            for (int n = 0; n < AllSections.Count; n++)
            {
                if (AllSections[n].AreaJudge(vector))
                {
                    return n;
                }
            }

            //ここに来るとエラー
            Debug.Log("Not found MapData");
            return -1;
        }
    }

    public MapSection NowSection
    {
        get
        {
            return AllSections[NowNum];
        }
    }

    string nowmapname
    {
        get { return NowSection.mapname; }
    }


    [SerializeField] Savepoint[] savepointDatas = new Savepoint[5];

    public bool isActivated(Vector2Int vector)
    {
        return NowSection.AreaJudge(vector);

    }

    public AreaExtent GetAreaExtent(Vector2Int Grid)
    {
        foreach (var item in AllSections)
        {
            if (item.AreaJudge(Grid))
            {
                Vector2Int ld = item.C_LeftDown + Vector2Int.up * Game.height / 2 + Vector2Int.right * Game.width / 2;
                Vector2Int ru = item.C_RightUp - Vector2Int.up * Game.height / 2 - Vector2Int.right * Game.width / 2;

                return new AreaExtent { LeftDown = ld, RightUp = ru };
            }
        }

        //Debug.LogWarning("InvalidExtent");
        return new AreaExtent { LeftDown = new Vector2Int(0, 0), RightUp = new Vector2Int(50 * Game.width, 80 * Game.height) };
    }



    public bool GimmickActivate(Vector2Int vector)
    {
        return false;
        return NowSection.AreaJudge(vector);
    }

    public string NowMapName
    {
        get
        {
            return NowSection.mapname;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        for (int j = 0; j < MapData.AreaData.Count; j++)
        {
            for (int i = 0; i < MapData.AreaData[j].SectionDatas.Count; i++)
            {

                MapSection item = MapSection(j, i);

                if (ShowAreaColor)
                {
                    Gizmos.color = new Color(item.color.r, item.color.g, item.color.b, 0.3f);

                    Gizmos.DrawCube(new Vector2(item.C_LeftDown.x, item.C_LeftDown.y) * 0.5f + new Vector2(item.C_RightUp.x, item.C_RightUp.y) * 0.5f, new Vector2(item.C_RightUp.x, item.C_RightUp.y) - new Vector2(item.C_LeftDown.x, item.C_LeftDown.y));
                }

                if (ShowSection)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawLine(new Vector3(item.C_LeftUp.x + 1, item.C_LeftUp.y - 1, 0), new Vector3(item.C_LeftDown.x + 1, item.C_LeftDown.y + 1, 0));
                    Gizmos.DrawLine(new Vector3(item.C_LeftDown.x + 1, item.C_LeftDown.y + 1, 0), new Vector3(item.C_RightDown.x - 1, item.C_RightDown.y + 1, 0));
                    Gizmos.DrawLine(new Vector3(item.C_RightDown.x - 1, item.C_RightDown.y + 1, 0), new Vector3(item.C_RightUp.x - 1, item.C_RightUp.y - 1, 0));
                    Gizmos.DrawLine(new Vector3(item.C_RightUp.x - 1, item.C_RightUp.y - 1, 0), new Vector3(item.C_LeftUp.x + 1, item.C_LeftUp.y - 1, 0));
                }
            }
        }
    }

    /*
    [SerializeField]
    SavePointData[] SavepointDatas = new SavePointData[10];
    */

    int MapNum;//再開するマップ
    int StartPoint;//再開するマップ入りポイント

    string wasmapname;
    IEnumerator screen;
    IEnumerator screen2;

    Vector2Int BeforeMapGrid;

    int PreviousNum = 0;



    //IEnumerator screen = Define.TP.AERA_TRANS_ON();
    /*
    public void LoadMainMap()
    {
        SceneManager.LoadScene("mainmap", LoadSceneMode.Additive);
        Define.GM.State(4);
    }*/

    //特定のポイントに移動する(ワープ地点の整合性もチェックする)
    public void MoveToPoint(Vector2 point)
    {
        //ワープ地点の整合性チェック
        Define.PM.TELLEPORT_PLAYER(point + Vector2Int.down);
    }

    /*
    IEnumerator Load(string name2)
    {
        AsyncOperation syncload;
        syncload = SceneManager.LoadSceneAsync(name2, LoadSceneMode.Additive);
        syncload.allowSceneActivation = false;     
        while (syncload.progress != 0.9f)
        {
            yield return new WaitForFixedUpdate();
        }
        syncload.allowSceneActivation = true;
        TP.Trans();
        Define.GM.SCREENCLEAR();
        Define.GM.State(4);
        yield break;
    }
    */

    string prevSceneName = "Basement";
    string nowSceneName = "Basement";

    public void RESTART()
    {


        /*
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).buildIndex != 0)
            {

               // SceneManager.UnloadSceneAsync(mapDatas[SceneManager.GetSceneAt(i).buildIndex].scenename);
                //Debug.Log("シーン削除" + mapDatas[SceneManager.GetSceneAt(i).buildIndex].scenename);

            }

        }

        StartCoroutine(Load(nowSceneName));
        */
    }

    /*
    private IEnumerator ene()
    {
        var async = SceneManager.LoadSceneAsync("NextScene");

        async.allowSceneActivation = false;
        //yield return new WaitForSeconds(1);
        //async.allowSceneActivation = true;
        yield return new WaitForSeconds(1);

    }
    */
    /*
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(nowSceneName!= scene.name)
        {
            prevSceneName = nowSceneName;
            nowSceneName = scene.name;


            if (prevSceneName != "Basement")
            {
                SceneManager.UnloadSceneAsync(prevSceneName);
            }

        }
        else
        {

        }
        


        int nextnum = GET_SCENE_NUM(nowSceneName);
        int prevnum = GET_SCENE_NUM(prevSceneName);

        if (prevnum == 0 && nextnum != 0)
        {
            Define.GM.SHOW_STAGENAME(mapDatas[nextnum].areaname);

            Define.PM.SET_PLAYER(Search_Player());

            if (mapDatas[nextnum].save_progress)
            {
                Define.GM.PROGRESS_SAVE(nextnum);
            }
        }
        else if (nextnum == 0)
        {

        }

        else
        {
            Define.PM.SET_PLAYER(Search_Player());

            //Define.PM.TELLEPORT_PLAYER(SET_START_POS(mapDatas[prevnum].scenename));

            Debug.Log(prevnum + " and " + nextnum+" nowname"+ nowSceneName);

            if (mapDatas[prevnum].areaname != mapDatas[nextnum].areaname && mapDatas[nextnum].show_area_name)
            {
                Define.GM.SHOW_STAGENAME(mapDatas[nextnum].areaname);

            }

            if (mapDatas[nextnum].save_progress)
            {
                Define.GM.PROGRESS_SAVE(nextnum);
            }

        }
    }
    */


    /*
    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        





        int nextnum = GET_SCENE_NUM(nowSceneName);
        int prevnum = GET_SCENE_NUM(prevSceneName);

        //Debug.Log("next" + nextnum);
        //Debug.Log("prev" + prevnum);


        if (prevnum == 0&& nextnum != 0)
        {
            Define.GM.SHOW_STAGENAME(mapDatas[nextnum].areaname);

            Define.PM.SET_PLAYER(Search_Player());

            if (mapDatas[nextnum].save_progress)
            {
                Define.GM.PROGRESS_SAVE(nextnum);
            }
        }
        else if (nextnum == 0)
        {

        }
        else
        {
            Define.PM.SET_PLAYER(Search_Player());

            Define.PM.TELLEPORT_PLAYER(SET_START_POS(mapDatas[prevnum].scenename));

            //初期地点計算処理




            if (mapDatas[prevnum].areaname != mapDatas[nextnum].areaname&& mapDatas[nextnum].show_area_name)
            {
                Define.GM.SHOW_STAGENAME(mapDatas[nextnum].areaname);

            }

            if (mapDatas[nextnum].save_progress)
            {
                Define.GM.PROGRESS_SAVE(nextnum);
            }

        }
       

    }
    */
    /*
    public Transform SET_START_POS(string prevname)
    {
        //Debug.Log(prevname);
        GameObject[] objects = GameObject.FindGameObjectsWithTag(dest_tag);

        foreach (GameObject point in objects)
        {
            //Debug.Log(point.name);

            MoveMap script = point.GetComponent<MoveMap>();

            //Debug.Log("マップ名"+script.GetName());


            if (script.GetName() == prevname)
            {
                //Debug.Log("見つけたマップ移動ポイント" + script.GetPos());

                Define.PM.DIRECTION(script.Right);

                return script.GetPos();
            }
        }       
        return null;
    }

    public playerdemo Search_Player()
    {
        GameObject[] playerobjects = GameObject.FindGameObjectsWithTag("Player");

        foreach(GameObject p_ob in playerobjects)
        {
            if (p_ob.scene.name == nowSceneName)
            {
                return p_ob.GetComponent<playerdemo>();
            }
        }
        return null;

        
    }

    */
    public void CLEAR_ALL_MAP()
    {
        /*
         for (int i = 0; i < SceneManager.sceneCount; i++)
         {
             if (SceneManager.GetSceneAt(i).buildIndex!=0)
             {               
                 SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i).buildIndex);
             }

         }

         nowSceneName = "Basement";

         */
    }




    public void Progress(int scenenum)
    {
        MapNum = scenenum;
    }

}

public class MapSection
{
    //マップ>エリア>セクションの順に集合
    public string mapname;//

    public bool nameflag;//エリア切り替え時の名前表示の可否

    public bool hidden;//隠しエリアかどうか(隠しエリアならば隣接するエリアとの通路は表示されない(壁のまま))

    public RectInt extent;



    int up
    {
        get
        {
            return extent.y + extent.height-1;
        }
    }
    int down
    {
        get
        {
            return extent.y;
        }
    }
    int left
    {
        get
        {
            return extent.x;
        }
    }
    int right
    {
        get
        {
            return extent.x + extent.width-1;
        }
    }

    public Color color;

    public bool AreaJudge(Vector2Int point)
    {
        return (Mathf.Min(left, right) <= point.x && Mathf.Max(left, right) >= point.x && Mathf.Min(down, up) <= point.y && Mathf.Max(down, up) >= point.y);

    }

    /// <summary>
    /// 座標がエリアに含まれるか判断する
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public bool AreaJudgePoint(Vector2Int point)
    {
        return (Mathf.Min((left - 1) * Game.width, right * Game.width) <= point.x && Mathf.Max((left - 1) * Game.width, right * Game.width) >= point.x && Mathf.Min((down - 1) * Game.height, up * Game.height) <= point.y && Mathf.Max((down - 1) * Game.height, up * Game.height) >= point.y);

    }

    public Vector2Int C_LeftUp
    {
        get
        {
            return new Vector2Int((left - 1) * Game.width, (up) * Game.height);
        }
    }
    public Vector2Int C_LeftDown
    {
        get
        {
            return new Vector2Int((left - 1) * Game.width, (down - 1) * Game.height);
        }
    }
    public Vector2Int C_RightUp
    {
        get
        {
            return new Vector2Int((right) * Game.width, up * Game.height);
        }
    }
    public Vector2Int C_RightDown
    {
        get
        {
            return new Vector2Int(right * Game.width, (down - 1) * Game.height);
        }
    }
}



public class AreaExtent
{
    public Vector2Int RightUp;
    public Vector2Int LeftDown;
}