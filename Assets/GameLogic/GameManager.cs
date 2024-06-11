using DataTypes.GameData;
using GameConsts;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Game_Manager;

    [SerializeField] gamestate startstate;

    //フェード関連
    public CanvasGroup transitionpanel;

    public IEnumerator FadeOut(float time, Action action = null)
    {
        print("fade");
        float mult = 1 / time;
        transitionpanel.alpha = 0;

        while (transitionpanel.alpha < 1)
        {
            transitionpanel.alpha += Time.deltaTime * mult;
            transitionpanel.alpha = Mathf.Min(transitionpanel.alpha, 1);
            if (action != null)
            {
                action();
            }
            yield return null;
        }
        transitionpanel.alpha = 1;

    }
    public IEnumerator FadeIn(float time, Action action = null)
    {
        float mult = 1 / time;

        transitionpanel.alpha = 1;

        while (transitionpanel.alpha > 0)
        {
            transitionpanel.alpha -= Time.deltaTime * mult;
            transitionpanel.alpha = Mathf.Max(transitionpanel.alpha, 0);

            if (action != null)
            {
                action();
            }
            yield return null;
        }
        transitionpanel.alpha = 0;
    }
    public IEnumerator In(float time, Action action = null)
    {
        float timer = 0;

        do
        {
            timer = Mathf.Min(timer + Time.deltaTime, time);
            transitionpanel.alpha = 1 - timer / time;
            if (action != null)
            {
                action();
            }
            yield return null;

        } while (timer < time);
        transitionpanel.alpha = 0;
    }






    // セーブデータ関連

    enum LoadState
    {
        None,
        Slot1,
        Slot2,
        Slot3,
        Slot4
    }

    LoadState loadState = LoadState.None;

    ConfigData configData;//コンフィグ等のデータ
    [SerializeField] PlayData playData;//現在プレイ中のデータ

    public PlayData PlayData
    {
        get
        {
            return playData;
        }
    }
    public ConfigData ConfigData
    {
        get
        {
            return configData;
        }
        set
        {
            print("configデータの変更が行われました");
            configData = value;
            Screen.SetResolution(Game.width * ScreenMult, Game.height * ScreenMult, isFullScreen);


            SaveData.SetClass<ConfigData>("ConfigData", ConfigData, 0);
            SaveData.Save(0);
        }
    }

    public Transform start;



    void AwakeDataLoad()
    {
        SaveData.Init();
        ConfigData = SaveData.GetClass<ConfigData>("ConfigData", new ConfigData(), 0);

        foreach (GameStateExecuter executer in Executers)
        {
            if (executer is MainGame)
            {
                MainGame ex = (MainGame)executer;
                ex.OnGameStart();
            }
        }


    }
    public void PlayDataSave(string name)
    {
        switch (loadState)
        {
            case LoadState.None:
                print($"ScriptError");
                break;

            default:
                playData.Filename = name;
                print($"DataSave:Slot{(int)loadState}");
                SaveData.SetClass<PlayData>("PlayData", playData, (int)loadState);
                SaveData.SetClass<FileData>("FileData", playData.fileData(), (int)loadState);
                SaveData.Save((int)loadState);
                break;
        }


    }
    public void PlayDataLoad(int slot)
    {
        playData = SaveData.GetClass<PlayData>("PlayData", new PlayData(), slot);
        loadState = (LoadState)slot;
        print($"DataLoad:Slot{(int)loadState}");
    }
    public void PlayDataClear(int slot)
    {
        SaveData.Clear(slot);
        print($"DataClear:Slot{slot}");
    }
    public void ResetPlayData()
    {
        playData = null;
    }




    /// <summary>
    /// ステートマシン
    /// 1フレームにつき一度までしかステートが切り替わらない。
    /// </summary>

    public gamestate GameState
    {
        get
        {
            return Now_GameState;
        }
    }

    gamestate Now_GameState = gamestate.Undefined;
    gamestate Pre_GameState = gamestate.Undefined;
    gamestate Next_GameState = gamestate.Undefined;

    public void StateQueue(int to = -1)
    {
        statequeueflag = true;
        if (to == -1)
        {
            Next_GameState = Pre_GameState;
        }
        else
        {
            Next_GameState = (gamestate)to;
        }
    }
    bool statequeueflag = false;

    IEnumerator StateChange()
    {
        statequeueflag = false;

        Pre_GameState = Now_GameState;
        Now_GameState = gamestate.Undefined;

        if (DebugMode.gamestate) print("Transition…");

        yield return StartCoroutine(Executers[(int)Pre_GameState].Finalizer(Next_GameState));
        yield return StartCoroutine(Executers[(int)Next_GameState].Init(Pre_GameState));

        Now_GameState = Next_GameState;

        if (DebugMode.gamestate)
        {
            print($"GameState was Changed from {Pre_GameState} to {Now_GameState}");
        }
        yield break;
    }


    void StateMachineUpdater()
    {
        if (Now_GameState != gamestate.Undefined)
        {
            Executers[(int)Now_GameState].Updater();
        }
    }

    void StateMachineLateUpdater()
    {
        if (Now_GameState != gamestate.Undefined)
        {
            Executers[(int)Now_GameState].LateUpdater();
        }
    }


    [SerializeField] GameStateExecuter[] Executers;
    public MainGame mainGame
    {
        get
        {
            return (MainGame)Executers[(int)gamestate.MainGame];
        }
    }










    public bool isGaming
    {
        get
        {
            return GameState == gamestate.MainGame;
        }
    }










    public Vector2 R_Pos(Vector2 pos)
    {
        return pos - Camera.Position;
    }



    [SerializeField] Image Image_GameOver;
    [SerializeField] CanvasGroup canvasGroup;

    public void GameOver_Image(bool flag, Vector2 pos, Sprite sprite, bool flip)
    {
        if (flag)
        {
            canvasGroup.alpha = 1;
        }
        else
        {
            canvasGroup.alpha = 0;
        }

        Image_GameOver.rectTransform.anchoredPosition = pos;
        Image_GameOver.sprite = sprite;
        if (flip)
        {
            Image_GameOver.rectTransform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            Image_GameOver.rectTransform.localScale = new Vector3(1, 1, 1);
        }
    }
















    bool menu = true;//メニューが開けるか

    bool time = true;//時間が進行するか
    float timemult = 1.0f;

    string Stage = "";//現在のステージの名前


    //各種マネージャー
    public InputSystemManager Input_Manager;

    public MapManager Map_Manager;

    public EquipmentDataManager EquipmentDataManager;
    public UI_Manager UI_Manager;

    public TransitionPanel Trans_Panel;
    public ObjectManager ObjectManger;
    public Equipment Equipment;
    public EventManager Event_Manager;

    public MiniMap MiniMap;
    public CameraController Camera;




    //public Sound Sound_Manager;

    [SerializeField] StageName StageNameScript;

    public int GotAttackID
    {
        get
        {
            AttackIDvar++;
            return (int)Mathf.Repeat(AttackIDvar, int.MaxValue);
        }
    }
    int AttackIDvar = 0;

    public int GotEntityID
    {
        get
        {
            EntityIDvar++;
            return EntityIDvar;
        }
    }
    int EntityIDvar = 0;


    public ConfigData.Language Language
    {
        get
        {
            return  ConfigData.Language.Japanese;
        }
    }



    void Awake()
    {
        if (Game_Manager == null)
        {
            Game_Manager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        transitionpanel.alpha = 1;

        if (!Application.isEditor && !Debug.isDebugBuild)
        {
            Application.targetFrameRate = 120;
        }

        AwakeDataLoad();
        GAME_AWAKE();
    }


    // Start is called before the first frame update
    void Start()
    {
        // Define.SDM.G_DATA_LOAD();
        //Define.MiniMAP.SimpleMiniMap = false;

    }

    // Update is called once per frame
    private void Update()
    {
        Input_Manager.Updater();

        if (statequeueflag)
        {
            StartCoroutine(StateChange());
        }

        StateMachineUpdater();
    }

    private void LateUpdate()
    {
        StateMachineLateUpdater();
    }


    public float TimeScale(float scale = -1)
    {
        if (scale >= 0 && scale <= 1)
        {
            if (GameState == gamestate.MainGame)
            {
                Time.timeScale = scale;
            }
        }
        return timemult;
    }

    public Transform Get_Camera_Pos()
    {
        return Camera.transform;
    }




    //ステージ名表示関連

    public void CHANGE_STAGENAME(string name)
    {
        if (Stage != name)
        {
            Stage = name;
        }
    }



    public void SHOW_STAGENAME(string stagename)
    {
        if (stagename == null)
        {
            stagename = string.Empty;
        }
        StageNameScript.Show(stagename);
    }



    public void RESTART_SAVE()
    {
        //Debug.Log("最終セーブポイントからやり直す");
    }

    public void QUIT()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
#endif
    }









    public int ScreenMult
    {
        get
        {
            switch (configData.screenMode)
            {
                case ConfigData.ScreenMode.Window_1:
                    return 1;
                case ConfigData.ScreenMode.Window_2:
                    return 2;
                case ConfigData.ScreenMode.Window_3:
                    return 3;
                case ConfigData.ScreenMode.Window_4:
                    return 4;

                default:
                    print("invalid config");
                    return 9;
            }

        }
    }
    bool isFullScreen
    {
        get
        {
            return configData.screenMode == ConfigData.ScreenMode.FullScreen;

        }
    }









    bool Game_Init()
    {
        //GameState = gamestate.Title;
        Map_Manager.CLEAR_ALL_MAP();

        //Title_Screen.EnterScreen();

        // Define.DS.ShowSelectMenu();
        return true;
    }



    //ゲーム開始時に呼ばれる
    void GAME_AWAKE()
    {
        Input_Manager.Init();

        StateQueue((int)gamestate.Title);


        Physics2D.autoSyncTransforms = true;
    }



    //スタート画面でボタンが押されると呼ばれる
    public void GAME_START()
    {

    }

    public void Continue()
    {
        ResetPlayData();
        PlayDataLoad((int)loadState);
    }

    //データセレクトでロードすると呼ばれる
    public void MAIN_GAME_START(int num)
    {
        PlayDataLoad(num);
        //GameState = gamestate.inGame;

        //Define.DS.ExitSelect();
        //Define.GM.SCREENCLEAR();

        //Define.PM.SET_STATUS();
        // Define.MiniMAP.Init();
        /*
        if (Define.DM.P_DATA_LOAD(num) != null)
        {
            //データを読み出し
            Define.PM.SET_STATUS(Define.DM.P_DATA_LOAD(num));      
            
            




            /*
            //セーブ地点のマップを読み込む
#if UNITY_EDITOR
            Debug.Log(SceneManager.sceneCount);

            if (SceneManager.sceneCount == 1)
            {
                Define.MAP.LoadScene(Define.PM.GET_MAP_NAME());
                Debug.Log(Define.PM.GET_MAP_NAME());

            }
            else
            {
                Define.GM.SCREENCLEAR();
                Define.GM.State(4);
            }

#else
                           Define.MAP.LoadScene(Define.PM.GET_MAP_NAME());

#endif
            
            Define.GM.State(4);

            //セレクト画面終了ntei 



            //

        }
    */
    }

    //タイトルに戻る。ゲームの進行状況は破棄される。
    public void GOTO_TITLE()
    {
        //Define.MAP.CLEAR_ALL_MAP();
        //GAME_AWAKE();
        //GameState = gamestate.Title;
    }

    //ゲームのセーブ
    public void GAME_SAVE()
    {

    }

    //ゲームの中断セーブ
    public void PROGRESS_SAVE(int scenenum)
    {
        Map_Manager.Progress(scenenum);
        //Player_Manager.PROGRESS_SAVE();
    }

    //ゲーム進行管理
    public void RESTART_AREA()
    {
        // Player_Manager.RESTART();
        Map_Manager.RESTART();
        //Define.GM.GameState = gamestate.inGame;

    }



    void OnStateChanged(gamestate pre, gamestate after)
    {

    }

}

public enum gamestate
{
    Undefined,

    Title,
    DataSelect,
    GameOver,

    MainGame,

    Menu,

    Status,
    Equipments,
    KeyItems,
    Memo,
    Settings,

    MiniMap
}







public static class Define
{
    public static readonly GameManager GM = GameManager.Game_Manager;

    public static readonly PlayerManager PM = GM.mainGame.playerManager;
    public static readonly UI_Manager UI = GM.UI_Manager;
    //public static readonly inGameUI UM = GM.UI_Manager;

    public static readonly EquipmentDataManager EDM = GM.EquipmentDataManager;

    public static readonly Equipment EQ = GM.Equipment;

    public static readonly MapManager MAP = GM.Map_Manager;

    public static readonly Equipment EQU = GM.Equipment;
    public static readonly EnemyManager EnemyM = GM.mainGame.enemyManager;

    public static readonly MiniMap MiniMAP = GM.MiniMap;

    public static readonly InputSystemManager IM = GM.Input_Manager;
}
/*
public class GameRank
{
    /// <summary>
    /// ランクの値を返す 代入すればランクを設定する
    /// </summary>
    public int Rank
    {
        get
        {
            for (int n = 0; n < border.Length - 1; n++)
            {
                if (border[n] <= exp && border[n + 1] > exp)
                {
                    return n;
                }
            }

            return 0;
        }

        set
        {
            int val = Mathf.Max(0, Mathf.Min(16, value));

            exp = (border[val] + border[val + 1]) / 2;

        }
    }

    /// <summary>
    /// 累計の経験値の値　0-20000の間の値をとる
    /// </summary>
    int exp
    {
        get
        {
            return 0;
        }
        set
        {
            int val = Mathf.Max(0, Mathf.Min(20000, value));

            point = val;
        }
    }

    /// <summary>
    /// 経験値の変数
    /// </summary>
    int point;

    int[] border = new int[17] { 0, 628, 1329, 2104, 2954, 3880, 4883, 5964, 7124, 8364, 9685, 11088, 12573, 14142, 15745, 17532, 20000 };

}
*/