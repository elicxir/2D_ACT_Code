using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class MainGame : GameStateExecuter
{
    public TileMapManager tileMapManager;
    public PlayerManager playerManager;
    public EnemyManager enemyManager;
    public EventManager eventManager;
    public GimmickManager gimmickManager;

    public orbManager orbManager;
    public ProjectileManager projectileManager;
    public SpriteObjectManager spriteObjectManager;

    [SerializeField] CameraController Camera;

    public void OnGameStart()
    {
        playerManager.OnGameStart();
        enemyManager.OnGameStart();
        projectileManager.OnGameStart();
        spriteObjectManager.OnGameStart();
    }



    public override void Updater()
    {
        GAME.PlayData.PlayTime += Time.deltaTime;

        ManagerUpdate();

        if (!GM.Player.OnEvent)
        {
            if (INPUT.ButtonDown(Control.Menu))
            {
                GAME.StateQueue((int)gamestate.Menu);
            }
            else if (INPUT.ButtonDown(Control.Map))
            {
                GAME.StateQueue((int)gamestate.MiniMap);
            }
        }

    }

    public override void LateUpdater()
    {
        ManagerLateUpdater();
    }

    private void LateUpdate()
    {
        GM.Game.Camera.SetExtent(GM.Game.Map_Manager.GetAreaExtent(playerManager.Player.NowMapGrid));
        GM.Game.Camera.SetCamera(playerManager.Player.CameraCenterPos);
    }

    string NowArea = string.Empty;
    void ManagerLoadInit()
    {
        gimmickManager.ManagerLoadInit(this);
        playerManager.ManagerLoadInit(this);
        enemyManager.ManagerLoadInit(this);
        eventManager.ManagerLoadInit(this);
        projectileManager.ManagerLoadInit(this);
        spriteObjectManager.ManagerLoadInit(this);
        NowArea = string.Empty;
    }

    void ManagerUpdate()
    {
        tileMapManager.ManagerUpdater(this);
        gimmickManager.ManagerUpdater(this);
        playerManager.ManagerUpdater(this);
        enemyManager.ManagerUpdater(this);
        eventManager.ManagerUpdater(this);
        projectileManager.ManagerUpdater(this);
        spriteObjectManager.ManagerUpdater(this);



    }

    void ManagerLateUpdater()
    {
        gimmickManager.ManagerLateUpdater(this);
        playerManager.ManagerLateUpdater(this);
        enemyManager.ManagerLateUpdater(this);
        eventManager.ManagerLateUpdater(this);
        projectileManager.ManagerLateUpdater(this);

    }


    public void OnGridChanged(Vector2Int newGrid)
    {
        if (!GM.Game.PlayData.VisitedGrid.Contains(newGrid))
        {
            GM.Game.PlayData.VisitedGrid.Add(newGrid);
        }
        print($"grid change {newGrid }");

    }


    IEnumerator fade;

    public void OnSectionChanged(Vector2Int newGrid, bool isFadeOut = true)
    {
        print("section change");

        string newArea = GM.MAP.GetAreaName(GM.MAP.SectionIndex(newGrid));



        if (isFadeOut)
        {
            if (fade != null)
            {
                StopCoroutine(fade);
                fade = null;
            }

            fade = GM.Game.In(0.4f);
            StartCoroutine(fade);

            if (newArea != NowArea)
            {
                OnAreaChanged(newArea, 0.5f);
                NowArea = newArea;
            }
        }
        else
        {

            if (newArea != NowArea)
            {
                OnAreaChanged(newArea, 1.5f);
                NowArea = newArea;

            }
        }


        tileMapManager.OnSectionChanged(newGrid);
        gimmickManager.OnSectionChanged(newGrid);
        playerManager.OnSectionChanged(newGrid);
        enemyManager.OnSectionChanged(newGrid);
        eventManager.OnSectionChanged(newGrid);
        projectileManager.OnSectionChanged(newGrid);



    }

    Coroutine stagename;
    void OnAreaChanged(string Areaname, float delay)
    {
        print("area change");

        if (stagename != null)
        {
            GM.UI.StageName.interrupt();
            StopCoroutine(stagename);
            stagename = null;
        }
        stagename = StartCoroutine(GM.UI.StageName.ShowStageName(Areaname, delay));
    }







    public override IEnumerator Finalizer(gamestate after)
    {
        GAME.Camera.isFollowing = false;
        enemyManager.GameExit();
        playerManager.GameExit();
        tileMapManager.GameExit();

        if (after == gamestate.DataSelect || after == gamestate.Title)
        {
        }

        yield break;
    }

    IEnumerator AreaChangeFade;

    public override IEnumerator Init(gamestate before)
    {
        GAME.Camera.isFollowing = true;

        enemyManager.GameEnter();
        playerManager.GameEnter();
        tileMapManager.GameEnter();

        if (before == gamestate.DataSelect || before == gamestate.GameOver)
        {
            if (GAME.PlayData.PlayTime == 0)
            {
                playerManager.Player.Position = GAME.start.position;
            }
            else
            {
                playerManager.Player.Position = GAME.PlayData.StartPoint;
            }

            GM.Game.Camera.SetCamera(playerManager.Player.CameraCenterPos);

            GM.Game.PlayData.FillConsumable();

            ManagerLoadInit();

            OnGridChanged(GM.Player.Player.NowMapGrid);
            OnSectionChanged(GM.Player.Player.NowMapGrid, false);

            yield return new WaitForSeconds(0.5f);

            StartCoroutine(GAME.FadeIn(1.2f));

            yield return new WaitForSeconds(0.3f);

        }
        else
        {

        }
        yield break;
    }













}
