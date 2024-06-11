using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;


public class PlayerManager : Managers_MainGame
{

    public Player Player;
    public PlayerAttack PlayerAttack;

    DataTypes.GameData.PlayData PlayData
    {
        get
        {
            return GM.Game.PlayData;
        }
    }

    public override void GameExit()
    {
        Player.ExitGame();
    }

    public override void GameEnter()
    {
        Player.EnterGame();
    }

    public PlayerStatusSheet statusSheet;

    [SerializeField] CameraController cameracon;

    /// <summary>
    /// 各種フラグ
    /// </summary>
    bool alive = true;//生きているかどうか

    public bool OnForcedControll = false;//強制的な移動中かどうか
    public bool OnEvent = false;//イベントの発生時かどうか
    public bool Knockback = false;//ノックバック中かどうか
    public bool Invincible = false;//無敵時間中かどうか


    public void AddEXP(int val)
    {
        //Define.SDM.GameData.exp += val;
    }
    public bool CheckEXP(int val)
    {
        return val <= PlayData.exp;
    }

    public bool Controll
    {
        get
        {
            if (OnEvent)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }





    public bool canpray;//祈れるかどうか

    /// <summary>
    /// 各種ステータス
    /// </summary>
    /// 

    int HP_Level
    {
        get
        {
            return GM.Game.PlayData.HP_UP;
        }
    }
    int MP_Level
    {

        get
        {
            return GM.Game.PlayData.MP_UP;

        }
    }

    int ATK_Level
    {

        get
        {
            return GM.Game.PlayData.ATK_UP;

        }
    }

    int MaxLV
    {
        get
        {
            return statusSheet.sheets[0].list.Count - 1;
        }
    }



    public int maxHP
    {
        get
        {
            return statusSheet.sheets[0].list[Mathf.Clamp(HP_Level, 0, MaxLV)].hp;
        }
    }
    public int maxMP
    {
        get
        {
            return statusSheet.sheets[0].list[Mathf.Clamp(MP_Level, 0, MaxLV)].mp;
        }
    }
    public float MPreg
    {
        get
        {
            return maxMP * 0.01f;
        }
    }
    public int Attack
    {
        get
        {
            return statusSheet.sheets[0].list[Mathf.Clamp(ATK_Level, 0, MaxLV)].attack;
        }
    }



    public bool isMPreg = true;//MP自動回復を行うか

    public Vector2 R_Pos
    {
        get
        {
            return r_pos;
        }
    }


    Vector2 r_pos = Vector3.zero;//画面上でのプレイヤーの座標


    Vector2Int mapgrid;



    public override void ManagerUpdater(MainGame caller)
    {
        //RefreshStatus();
        AliveFunction();


        Player.Updater(caller.gimmickManager.EntityUpdateFlag(Player.Position,Player.TerrainUpdateDetectRange));
        r_pos = Player.Position - cameracon.Position;

        if (mapgrid != Player.NowMapGrid)
        {
            caller.OnGridChanged(Player.NowMapGrid);

            if (Map.SectionIndex(mapgrid) != Map.SectionIndex(Player.NowMapGrid))
            {
                caller.OnSectionChanged(Player.NowMapGrid ,mapgrid != Vector2Int.zero);
            }

            mapgrid = Player.NowMapGrid;

        }


        GM.UI.Updater(Player.entityStats);
        //GM.UI.Updater(HP / MaxHP, MP / MaxMP, buffManager.BuffDataOut(), 0);


    }

    public override void ManagerLoadInit(MainGame caller)
    {
        r_pos = Player.Position - cameracon.Position;
        Player.Init();
        GM.UI.Updater(Player.entityStats);

    }

    public void SetRestart()
    {
        //Define.SDM.GameData.ReStartPoint = Player.Position;
        //Debug.Log("SetRestart:" + Player_Data.ReStartPoint);
    }






    void ItemAction()
    {/*
        if (Define.IM.ButtonDown(Control.Item))
        {
            if (Define.IM.Button(Control.Up))
            {
                print("select item");
                PlayData.ItemSelect();
            }
            else
            {
                print("use item");
                //ITEM();
            }

        }*/

    }

    void SpellAction()
    {
    }

    void Action()
    {
        if (Player)

            //if (Define.F.isGaming)


            if ((Define.IM.ButtonDown(Control.Map)))
            {

                Define.MiniMAP.SimpleMiniMap = !Define.MiniMAP.SimpleMiniMap;

            }















    }





    void AliveFunction()
    {
        if (alive)
        {
            ItemAction();


            //Define.SDM.GameData.totalplaytime += Time.deltaTime;

            //Debug.Log(Player_Data.totalplaytime);

            //Define.UM.PLAYER_HP((float)HP / MaxHP);
            //Define.UM.PLAYER_MP((float)MP / MaxMP);

            //if (HP <= 0)
            //{
            //    DEATH();
            //}

            //Action();


        }
        else
        {
            //Define.UM.PLAYER_HP(0);

        }
    }


    public void REFRESH()
    {

        if (alive)
        {
            //Define.SDM.GameData.totalplaytime += Time.deltaTime;

            //Debug.Log(Player_Data.totalplaytime);

            //Define.UM.PLAYER_HP((float)HP / MaxHP);
            //Define.UM.PLAYER_MP((float)MP / MaxMP);

            //if (HP <= 0)
            //{
            //    DEATH();
            //}

            //Action();


        }
        else
        {
            //Define.UM.PLAYER_HP(0);

        }
    }





    //データ読み込み時のリセット関数
    public void SET_STATUS()
    {
        //data.SumToData();

        //RefreshStatus();
        alive = true;
        //Define.UM.SetItemGraph(data1.Sprite, Player_Data.equipcount[ItemSelect]);
        // Define.UM.SetItemGraph(data1.Sprite, Define.SDM.GameData.GetEquipment.ItemCount[ItemSelect]);

        Define.MAP.MoveToPoint(GM.Game.PlayData.StartPoint);
        mapgrid = Player.NowMapGrid;


    }

    //データのセーブ
    public void DATA_SAVE()
    {
        //Define.SDM.GameData.StartPoint = Player.Position;
        //Define.SDM.GameData.ReStartPoint = Player.Position;

        //Define.UI.ItemGraph(data1.Sprite, Player_Data.equipcount[ItemSelect]);

        //Define.SDM.GameDataSave();
    }

    //一次セーブ
    public void PROGRESS_SAVE()
    {

    }

    //初回のロード　最終セーブポイントor初期地点に飛ばされる
    public void GameStart()
    {


    }

    public int RESTART()
    {

        alive = true;
        OnEvent = false;//イベントの発生時かどうか
        Knockback = false;//ノックバック中かどうか
        Invincible = false;//無敵時間中かどうか

        //Define.MAP.MoveToPoint(Player_Data.ReStartPoint);
        Debug.Log("Restarted");

        return 0;
    }


    public void DEATH()
    {
        alive = false;
        Define.MAP.MoveToPoint(new Vector2(240, 180));

        GM.Game.StateQueue((int)gamestate.GameOver);

    }


    public void KnockBack(int level)//ノックバックさせる。
    {

    }
    /*
    public void Damage(int amount, bool knockback = true)//ダメージを与えノックバックさせる
    {
        HP_CHANGE(-amount);
        //ノックバック処理
    }
    */
    /*
    public void HP_CHANGE(int amount, int proportion = 0)
    {
        HP += (int)(MaxHP * (float)proportion / 100) + amount; ;

    }

    public bool MP_CHANGE(int amount, int proportion = 0, int min_MP_prop = 0)
    {
        float exMP = MP + (int)(MP * (float)proportion / 100) + amount;

        if (min_MP_prop > 0)
        {
            if (MP * 100 < MaxMP * min_MP_prop)
            {
                return false;
            }
        }

        if (exMP > MaxMP)
        {
            //MP = MaxMP;
            return true;
        }
        else if (exMP < 0)
        {
            return false;
        }
        else
        {
            //MP =exMP;
            return true;
        }
    }


    public void MP_Gain(float amount)
    {
        print("addMP:" + amount);
        MP = amount;
    }
    */

    public void SPIKE_HIT()
    {
        if (alive)
        {
            //HP = -100000;
        }
    }

    public bool GET_ALIVE()
    {
        return alive;
    }


    public void ITEM()
    {
        //Define.SDM.GameData.UseItem();
    }


    public void TELLEPORT_PLAYER(Vector2 t)
    {
        Player.Position = t;

    }













    //接地しているかどうか返す
    public bool OnGround()
    {
        return true;
    }

}


public class PlayerStats
{
    public float MaxHP;
    public float HP;

    public float MaxMP;
    public float MP;


    public float Timer_Skill1;//スキル1の溜め時間
    public float Timer_Skill2;//スキル1の溜め時間






    public bool alive;//生きているかどうか

    public bool isOnEvent;//イベントの発生時かどうか
    public bool isKnockback;//ノックバック中かどうか



    public bool Invincible;//無敵時間中かどうか

    public bool canControll
    {
        get
        {
            return true;
        }
    }


}