using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Functions;
using GameConsts;
using Managers;
using DataTypes.Element;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// EntityとProjectile
/// Entity:耐久力,マナ,状態異常が実装されている。敵やプレイヤーに
/// Projectile:耐久力のみが実装されている。弾に
/// </summary>
public class Entity : OwnTimeMonoBehaviour
{

    [Header("各種能力値")]

    public EntityStats E_stats;
    public Element ATK;
    public Element DEF;

    public float BaseSpeed = 80;

    [SerializeField] protected EntityStatsData statsData;

    [Header("アニメーションと行動")]
    public SimpleAnimation EntityAnimator;
    public List<EntityAction> Actions;

    protected int ActionIndex
    {
        get
        {
            return ActionIndex_var;
        }
        set
        {
            BeforeActionIndex_var = ActionIndex_var;
            ActionIndex_var = value;
        }
    }
    [SerializeField] int ActionIndex_var;

    protected int BeforeActionIndex
    {
        get
        {
            return BeforeActionIndex_var;
        }
    }
    [SerializeField] int BeforeActionIndex_var;

    public bool isInvincible
    {
        get
        {
            return buffManager.GetValue(BuffType.Invincible) > 0;
        }
    }

    public override Vector2 Position
    {
        get
        {
            return transform.position;
        }
        set
        {
            transform.position = value;

            AdjustedPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);

            TerrainDataUpdate(AdjustedPosition);
        }
    }

    public int TerrainUpdateDetectRange
    {
        get
        {
            return Mathf.CeilToInt((new Vector2(TerrainBoxHeight, TerrainBoxWidth)).magnitude);
        }
    }

    protected override float TimeMult_Var => Mathf.Clamp(buffManager.GetValue(BuffType.TimeScale, BuffManager.mode.Multipl, 1), 0.1f, 10);



    //terrainHitBoxを変化させる際に利用する
    protected Vector2 SpriteOffset;

    public Vector2Int SpritePos
    {
        set
        {
            sprite_renderer.transform.localPosition = new Vector3(value.x + SpriteOffset.x, value.y + SpriteOffset.y, 0);
        }
        get
        {
            return new Vector2Int((int)sprite_renderer.transform.localPosition.x, (int)sprite_renderer.transform.localPosition.y);

        }
    }

    [Header("デバッグ用")]
    [SerializeField] bool ShowTerrainHitBox = true;
    [SerializeField] bool ShowAttackHitBox = true;
    [SerializeField] bool ShowAttackColider = true;


    [Header("各種登録")]

    public bool hasBlood = true;

    /// <summary>
    /// sprite_rendererは軸となるスプライトを登録する
    /// </summary>
    public SpriteRenderer sprite_renderer;

    /// <summary>
    /// additional_rendererにはsprite_rendererで登録した以外のスプライトを登録する
    /// </summary>
    public SpriteRenderer[] additional_renderer;

    public void ColorEffect(Color color)
    {
        sprite_renderer.color = BaseSpriteColor * color;
        foreach (SpriteRenderer item in additional_renderer)
        {
            item.color = BaseSpriteColor * color;
        }
    }

    public bool EntityIsVisible
    {
        get
        {
            if (sprite_renderer.isVisible)
            {
                return true;
            }

            foreach (SpriteRenderer item in additional_renderer)
            {
                if (item.isVisible)
                {
                    return true;
                }
            }

            return false;
        }
    }

    public bool activeAHB
    {
        get
        {
            return EntityIsVisible;
        }
    }














    Color BaseSpriteColor
    {
        get
        {
            if (isPoisoned)
            {
                return new Color(0.56f, 0.2f, 1);
            }
            else
            {
                return Color.white;
            }
        }
    }

    public void testAC(AC_set[] ac_Sets, bool test = false)
    {
        int ID = 0;
        /*
        for (int i = 0; i < attackColider.Length; i++)
        {

            if (i < ac_Sets.Length && ac_Sets[i].isUse)
            {
                attackColider[i].transform.localPosition = ac_Sets[i].offset;
                attackColider[i].SetAttackColider(true, ID, Element_Attack, ac_Sets[i].size, ac_Sets[i].dataSet, Hostility.Both);
            }
            else
            {
                attackColider[i].transform.localPosition = Vector2.zero;
                attackColider[i].SetAttackColider(false, ID, Element_Attack, Vector2.one * 8);
            }
        }*/
    }





    protected virtual void SetUseAC(AC_set[] ac_Sets, bool test = false)
    {
        int ID = GM.Game.GotAttackID;

        if (ac_Sets.Length != AC_Groups.Length)
        {
            Debug.LogError("Invalid AttackColliderData");
        }

        for (int i = 0; i < AC_Groups.Length; i++)
        {
            foreach (AttackColider ac in AC_Groups[i].attackColiders)
            {
                if (ac_Sets[i].isUse)
                {
                    Hostility h = Hostility.None;
                    if (this is Enemy)
                    {
                        switch (ac_Sets[i].dataSet.hostileType)
                        {
                            case HostileType.Normal:
                                h = Hostility.Player;
                                break;
                            case HostileType.Ally:
                                h = Hostility.Enemy;
                                break;
                            case HostileType.Both:
                                h = Hostility.Both;
                                break;
                            default:
                                break;
                        }
                    }
                    else if (this is Player)
                    {
                        switch (ac_Sets[i].dataSet.hostileType)
                        {
                            case HostileType.Normal:
                                h = Hostility.Enemy;
                                break;
                            case HostileType.Ally:
                                h = Hostility.Player;
                                break;
                            case HostileType.Both:
                                h = Hostility.Both;
                                break;
                            default:
                                break;
                        }

                    }
                    ac.SetAttackColider(true, ID, Element_Attack, false, ac_Sets[i].dataSet, h);

                }
                else
                {
                    ac.transform.localPosition = Vector2.zero;
                    ac.SetAttackColider(false, ID, Element_Attack);

                }
            }
        }
    }
    public void ACReset()
    {/*
        foreach (AttackColider ac in attackColider)
        {
            ac.SetAttackColider(false, 0, Element_Attack, Vector2.one * 8);
        }
        foreach (AttackHitBox ahb in attackHitBox)
        {
            ahb.HitBoxEnable = false;
        }
        */
    }



    public void SetHitBox(HitBox_set[] hitBox_Set)
    {
        if (hitBox_Set.Length != AHB_Groups.Length)
        {
            Debug.LogError("Invalid HitBoxData");
        }

        for (int i = 0; i < AHB_Groups.Length; i++)
        {
            foreach (AttackHitBox ahb in AHB_Groups[i].hitBoxes)
            {
                ahb.entityHitBoxData = hitBox_Set[i].HitBoxData;
                ahb.HitBoxEnable = hitBox_Set[i].isUse;
            }
        }
    }


    void Direction(Vector2Int offset)
    {
        switch (faceDirection)
        {
            case FaceDirection.Right:
                SpritePos = offset;
                sprite_renderer.transform.localScale = new Vector3(1, 1, 1);
                break;

            case FaceDirection.Left:
                SpritePos = Vector2Int.left * offset.x + Vector2Int.up * offset.y;
                sprite_renderer.transform.localScale = new Vector3(-1, 1, 1);
                break;
        }
    }








    [SerializeField] protected bool isTranparent;




    protected virtual void OnDrawGizmos()
    {
        if (ShowTerrainHitBox)
        {
            Gizmos.color = new Color(0, 1, 0, 0.33f);
            Gizmos.DrawCube(transform.position, TerrainHitBox.size);
        }

        if (ShowAttackHitBox)
        {
            Gizmos.color = new Color(0, 0, 1, 0.33f);
            foreach (var item in AHB_Groups)
            {
                foreach (var ahb in item.hitBoxes)
                {
                    if (ahb.HitBoxEnable)
                    {
                        Gizmos.DrawCube((Vector2)ahb.transform.position, ahb.size);
                    }
                }
            }

        }

        if (ShowAttackColider)
        {
            Gizmos.color = new Color(1, 0, 0, 0.33f);

            foreach (var item in AC_Groups)
            {
                foreach (var ac in item.attackColiders)
                {
                    if (ac.isActive && ac.transform.eulerAngles.z == 0)
                    {
                        Gizmos.DrawCube(ac.transform.position, ac.size);
                    }
                }
            }
        }
    }


    /// <summary>
    /// このエンティティが攻撃を行った際に計算に用いられる攻撃力
    /// </summary>
    public Element Element_Attack
    {
        get
        {
            Element val = new Element();
            val.SetValues(new int[9] { Attack + buffManager.GetValueInt(BuffType.ATK_Slash), Attack + buffManager.GetValueInt(BuffType.ATK_Hack), Attack + buffManager.GetValueInt(BuffType.ATK_Magic), Attack + buffManager.GetValueInt(BuffType.ATK_Heat), Attack + buffManager.GetValueInt(BuffType.ATK_Ice), Attack + buffManager.GetValueInt(BuffType.ATK_Thunder), 100, 100, 100 });
            return val;
        }

    }
    public Element Element_Deffence
    {
        get
        {
            Element val = new Element();
            val.SetValues(new int[9] { buffManager.GetValueInt(BuffType.DEF_Slash), buffManager.GetValueInt(BuffType.DEF_Hack), buffManager.GetValueInt(BuffType.DEF_Magic), buffManager.GetValueInt(BuffType.DEF_Heat), buffManager.GetValueInt(BuffType.DEF_Ice), buffManager.GetValueInt(BuffType.DEF_Thunder), buffManager.GetValueInt(BuffType.DEF_Poison), buffManager.GetValueInt(BuffType.DEF_Hurt), buffManager.GetValueInt(BuffType.DEF_Curse) });
            return val;
        }

    }




    void OnValidate()
    {
        SetHitBox();

        EntityAnimator = GetComponent<SimpleAnimation>();

        Actions = GetComponentsInChildren<EntityAction>().ToList();



        sprite_renderer = GetComponentInChildren<SpriteRenderer>();
        additional_renderer = GetComponentsInChildren<SpriteRenderer>();

        TerrainHitBox = GetComponent<BoxCollider2D>();

        foreach (EntityAction action in Actions)
        {
            action.entity = this;
        }

    }




    [SerializeField] protected BoxCollider2D TerrainHitBox;

    protected enum EntityBelong
    {
        Player,//プレイヤー
        Enemy,//敵
    }

    public bool canHit(Hostility hostility)
    {
        switch (hostility)
        {
            case Hostility.Player:
                switch (entityBelong)
                {
                    case EntityBelong.Player:
                        return true;

                    case EntityBelong.Enemy:
                        return false;
                }

                break;

            case Hostility.Enemy:
                switch (entityBelong)
                {
                    case EntityBelong.Player:
                        return false;

                    case EntityBelong.Enemy:
                        return true;
                }
                break;

            case Hostility.Both:
                return true;
        }

        return false;
    }

    EntityBelong entityBelong
    {
        get
        {
            if (this is Player)
            {
                return EntityBelong.Player;
            }

            return EntityBelong.Enemy;
        }
    }

    enum EntityType
    {
        OnGround,//地形に沿って移動する
        Float,//浮いている(地形に従うが重力は受けない)
        Transparent,//地形と接触しない(常に浮遊扱い)
    }




    [SerializeField] EntityType entityType;



    public EntityStats entityStats
    {
        get
        {
            EntityStats stats = new EntityStats
            {
                HP = HP,
                MaxHP = MaxHP,
                MP = MP,
                MaxMP = MaxMP,
                MPreg = MPreg,
                HPreg = HPreg,

                Attack = Attack,


                Poison = Poison,
                Hurt = Hurt,
                Curse = Curse,

            };

            return stats;
        }
    }


    protected bool isAlive = true;

    public bool GetIsAlive
    {
        get
        {
            return isAlive;
        }
    }




    public BuffManager buffManager = new BuffManager();
    public HitTimeManager hitTimeManager = new HitTimeManager();


    public int EntityID = 0;






    //現在の行動を保存するコルーチン
    protected Coroutine NowAction;

    protected string NowState;

    //次のステート
    string NextState;



    protected virtual IEnumerator StateMachine()
    {
        yield return null;
    }


    Coroutine state_machine;
    IEnumerator state_enumerator;

    Coroutine wait;

    /// <summary>
    /// StateMachineを中断してアクションを割り込ませて終了後には中断位置から再開する。
    /// </summary>
    protected void InterruptAction(int index)
    {

        EntityAction entityAction = Actions[index];

        if (state_machine != null)
        {
            StopCoroutine(state_machine);
            state_machine = null;
        }
        if (NowAction != null)
        {
            StopCoroutine(NowAction);
            NowAction = null;
        }

        SetUseAC(entityAction.EAS.ac_Sets);
        SetHitBox(entityAction.EAS.hitbox_set);
        Direction(entityAction.offset);

        CustomPlayingSpeedMult = 1;
        ActionIndex = index;

        NowAction = StartCoroutine(entityAction.Act());

        if (wait != null)
        {
            StopCoroutine(wait);
        }
        wait = StartCoroutine(RestartState());

        NowState = entityAction.ActionName;
    }


    protected void StopAction()
    {
        if (state_machine != null)
        {
            StopCoroutine(state_machine);
            state_machine = null;
        }

        if (NowAction != null)
        {
            StopCoroutine(NowAction);
            NowAction = null;
        }

        state_machine = StartCoroutine(StateMachine());
    }


    protected void DoAction(int index)
    {
        EntityAction entityAction = Actions[index];

        SetUseAC(entityAction.EAS.ac_Sets);
        SetHitBox(entityAction.EAS.hitbox_set);
        Direction(entityAction.offset);

        CustomPlayingSpeedMult = 1;

        ActionIndex = index;
        NowAction = StartCoroutine(entityAction.Act());
        NowState = entityAction.ActionName;
    }

    /// <summary>
    /// StateMachineを中断してアクションを割り込ませて終了後にはStateMachineをリセットする。
    /// </summary>
    protected virtual void OverrideAction(int index)
    {
        EntityAction entityAction = Actions[index];

        if (state_machine != null)
        {
            StopCoroutine(state_machine);
            state_machine = null;
        }

        if (NowAction != null)
        {
            StopCoroutine(NowAction);
            NowAction = null;
        }

        SetUseAC(entityAction.EAS.ac_Sets);
        SetHitBox(entityAction.EAS.hitbox_set);
        Direction(entityAction.offset);

        CustomPlayingSpeedMult = 1;
        ActionIndex = index;
        NowAction = StartCoroutine(entityAction.Act());

        if (wait != null)
        {
            StopCoroutine(wait);
        }

        wait = StartCoroutine(ResetState());

        NowState = entityAction.ActionName;
    }


    IEnumerator ResetState()
    {
        yield return NowAction;
        state_machine = StartCoroutine(StateMachine());
    }

    IEnumerator RestartState()
    {
        yield return NowAction;
        state_machine = StartCoroutine(state_enumerator);
    }


    EntityAction GetEA(string name)
    {
        foreach (EntityAction ea in Actions)
        {
            if (ea.ActionName == name)
            {
                return ea;
            }
        }
        print("null");
        return null;
    }

    public void FullRegene()
    {
        HP = MaxHP;
        MP = MaxMP;
        Poison = 0;
        Hurt = 0;
        Curse = 0;
    }

    public override void Init()
    {
        base.Init();
        buffManager.REMOVE();
        sprite_renderer.enabled = true;
        Direction(Vector2Int.zero);
        sprite_renderer.color = BaseSpriteColor;
        ColorEffect(Color.white);
        isAlive = true;

        FullRegene();
        AnimationReset();

        foreach (EntityAction ea in Actions)
        {
            if (EntityAnimator.GetState(ea.ActionName) == null && ea.animationClip != null)
            {
                EntityAnimator.AddClip(ea.animationClip, ea.ActionName);
            }

            for (int i = 0; i < ea.AnimationClips.Length; i++)
            {
                if (EntityAnimator.GetState(ea.GetActionName(i)) == null && ea.AnimationClips[i] != null)
                {
                    EntityAnimator.AddClip(ea.AnimationClips[i], ea.GetActionName(i));
                }
            }




        }


        EntityID = Define.GM.GotEntityID;
        for (int i = 0; i < isGroundLog.Length; i++)
        {
            isGroundLog[i] = false;
        }

        TerrainDataUpdate(AdjustedPosition);

        //hitTimeManager.Init();
        foreach (var item in AHB_Groups)
        {
            foreach (var ahb in item.hitBoxes)
            {
                ahb.Init(this);
            }
        }

        state_enumerator = StateMachine();
        state_machine = StartCoroutine(state_enumerator);


    }

    /// <summary>
    /// バフとヒットボックスのフレーム毎アップデート
    /// </summary>
    protected virtual void TemporalFunction(float timestep)
    {
        ATK = Element_Attack;
        DEF = Element_Deffence;

        //バフ,ヒット間隔,自動回復
        {
            buffManager.Updater(timestep);
            hitTimeManager.Updater(timestep);
            Reg(timestep);
            StateEffect(timestep);
        }

        //当たり判定関係
        {


            foreach (var item in AHB_Groups)
            {
                foreach (var ahb in item.hitBoxes)
                {
                    ahb.Updater();
                }
            }

            foreach (var item in AC_Groups)
            {
                foreach (var ac in item.attackColiders)
                {
                    ac.Updater();
                }
            }
        }
    }




    public string PlayingAnimation
    {
        get; set;
    }
    public float CustomPlayingSpeedMult = 1;

    protected virtual void EntityStateWatcher()
    {/*
        
        if (NowAction == null && !string.IsNullOrEmpty(NextState))
        {

            NowAction = StartCoroutine(GetEA(NextState).Act());
            NowState = GetEA(NextState).ActionName;

            NextState = string.Empty;
        }
        */
        AnimationSpeedAdjust();

    }

    void AnimationSpeedAdjust()
    {
        if (EntityAnimator.GetState(PlayingAnimation) != null)
        {
            EntityAnimator.GetState(PlayingAnimation).speed = TimeMult * CustomPlayingSpeedMult;
        }
    }

    public override void EnterGame()
    {
        base.EnterGame();
        AnimationSpeedAdjust();
    }

    public override void ExitGame()
    {
        base.ExitGame();
        AnimationSpeedAdjust();
    }

    protected void AnimationReset()
    {
        CustomPlayingSpeedMult = 1;

        if (NowAction != null)
        {
            StopCoroutine(NowAction);
            NowAction = null;
        }


    }

    protected int midAirCount = 0;

    protected virtual void EntityUpdater()
    {

    }

    protected virtual void EntityMove()
    {

    }
    /// <summary>
    /// OwnTimeMonoBehaviourの継承
    /// </summary>
    public override void Updater(bool UpdateFlag)
    {
        E_stats = entityStats;

        sprite_renderer.color = BaseSpriteColor;

        if (isGround)
        {
            midAirCount = 0;
        }
        else
        {
            midAirCount++;
        }
        TemporalFunction(Time.deltaTime);

        if (isAlive)
        {
            EntityStateWatcher();
            EntityUpdater();
        }

        if (UpdateFlag)
        {
            TerrainDataUpdate(AdjustedPosition);
        }

        if (!canMoveUp && Velocity.y > 0)
        {
            BaseVelocity.y = 0;
        }

        if (entityType == EntityType.OnGround && isAlive)
        {
            if (canMoveDown)
            {
                BaseVelocity.y = Math.Max(Velocity.y - Game.Gravity * OwnDeltaTime, -MaxFallSpeed);
            }
            else
            {
                if (Velocity.y <= 0)
                {
                    BaseVelocity.y = 0;
                }
            }
        }


        if (!GM.Player.OnEvent)
        {
            EntityMove();
        }

        base.Updater();

    }

    public virtual void LateUpdater()
    {

    }

    //[SerializeField] Transform effectRoot;
    void StateEffect(float timestep)
    {
        if (isPoisoned)
        {
            /*
            if (effectRoot != null)
            {
                GM.OBJ.SpriteObjectManager.GenerateByData(GM.OBJ.SpriteObjectManager.basicSODs[(int)SpriteObjectManager.DataIndex.PoisonEffect], Position);

            }
            else
            {
                GM.OBJ.SpriteObjectManager.GenerateByData(GM.OBJ.SpriteObjectManager.basicSODs[(int)SpriteObjectManager.DataIndex.PoisonEffect], effectRoot.position);
            }
        }*/
        }
    }


    void Reg(float timestep)
    {
        regbuffer_hp += HPreg * timestep;
        regbuffer_mp += MPreg * timestep;

        regbuffer_poison -= Poison_reg * timestep;
        regbuffer_hurt -= Hurt_reg * timestep;
        regbuffer_curse -= Curse_reg * timestep;

        if (regbuffer_hp >= 1)
        {
            HP = HP + Mathf.FloorToInt(regbuffer_hp);
            regbuffer_hp -= Mathf.FloorToInt(regbuffer_hp);
        }
        else if (regbuffer_hp <= -1)
        {
            HP = HP + Mathf.CeilToInt(regbuffer_hp);
            regbuffer_hp -= Mathf.CeilToInt(regbuffer_hp);
        }

        if (regbuffer_mp >= 1)
        {
            MP = MP + Mathf.FloorToInt(regbuffer_mp);
            regbuffer_mp -= Mathf.FloorToInt(regbuffer_mp);
        }
        else if (regbuffer_mp <= -1)
        {
            MP = MP + Mathf.CeilToInt(regbuffer_mp);
            regbuffer_mp -= Mathf.CeilToInt(regbuffer_mp);
        }

        if (regbuffer_poison >= 1)
        {
            Poison = Poison + Mathf.FloorToInt(regbuffer_poison);
            regbuffer_poison -= Mathf.FloorToInt(regbuffer_poison);
        }
        else if (regbuffer_poison <= -1)
        {
            Poison = Poison + Mathf.CeilToInt(regbuffer_poison);
            regbuffer_poison -= Mathf.CeilToInt(regbuffer_poison);
        }

        if (regbuffer_hurt >= 1)
        {
            Hurt = Hurt + Mathf.FloorToInt(regbuffer_hurt);
            regbuffer_hurt -= Mathf.FloorToInt(regbuffer_hurt);
        }
        else if (regbuffer_hurt <= -1)
        {
            Hurt = Hurt + Mathf.CeilToInt(regbuffer_hurt);
            regbuffer_hurt -= Mathf.CeilToInt(regbuffer_hurt);
        }

        if (regbuffer_curse >= 1)
        {
            Curse = Curse + Mathf.FloorToInt(regbuffer_curse);
            regbuffer_curse -= Mathf.FloorToInt(regbuffer_curse);
        }
        else if (regbuffer_curse <= -1)
        {
            Curse = Curse + Mathf.CeilToInt(regbuffer_curse);
            regbuffer_curse -= Mathf.CeilToInt(regbuffer_curse);
        }
    }


    /// <summary>
    /// 死亡関係の処理
    /// </summary>
    void Entity_Death()
    {
        isAlive = false;

        foreach (var item in AC_Groups)
        {
            foreach (var ac in item.attackColiders)
            {
                ac.isActive = false;
            }
        }

        foreach (var item in AHB_Groups)
        {
            foreach (var ahb in item.hitBoxes)
            {
                ahb.HitBoxEnable = false;
            }
        }

        StartCoroutine(DeathAnimationWait());

    }

    IEnumerator DeathAnimationWait()
    {
        yield return new WaitUntil(() => canDeathAnimation);

        if (NowAction != null)
        {
            StopCoroutine(NowAction);
        }
        BaseVelocity = Vector2.zero;

        StartCoroutine(DeathCoRoutine());
    }

    /// <summary>
    /// 死亡時に実行されるコルーチン
    /// </summary>
    protected virtual IEnumerator DeathCoRoutine()
    {
        yield break;
    }










    int HPvar = 1;
    int MPvar = 1;
    protected int HP
    {
        get
        {
            return HPvar;
        }
        set
        {
            HPvar = Mathf.Clamp(value, 0, MaxHP);

            if (HPvar == 0 && isAlive)
            {
                Entity_Death();
            }
        }
    }
    protected int MP
    {
        get
        {
            return MPvar;
        }
        set
        {
            MPvar = Mathf.Max(0, Mathf.Min(MaxMP, value));
        }
    }
    protected virtual int MaxHP
    {
        get
        {
            return statsData.Stats.MaxHP;
        }
    }
    protected virtual int MaxMP
    {
        get
        {
            return statsData.Stats.MaxMP;
        }
    }
    float HPreg
    {
        get
        {
            return Base_HPreg + buffManager.GetValue(BuffType.HPreg);
        }
    }
    float MPreg
    {
        get
        {
            return Base_MPreg + buffManager.GetValue(BuffType.MPreg);
        }
    }
    int Attack
    {
        get
        {
            return Base_Attack + buffManager.GetValueInt(BuffType.Attack);
        }
    }

    protected virtual float Base_HPreg
    {
        get
        {
            return statsData.Stats.BaseHPreg;
        }
    }
    protected virtual float Base_MPreg
    {
        get
        {
            return statsData.Stats.BaseMPreg;
        }
    }
    protected virtual int Base_Attack
    {
        get
        {
            return statsData.Stats.BaseAttack;

        }
    }
    protected virtual int Base_PoisonResist
    {
        get
        {
            return statsData.Stats.BasePoisonResist;
        }
    }
    protected virtual int Base_HurtResist
    {
        get
        {
            return statsData.Stats.BaseHurtResist;
        }
    }
    protected virtual int Base_CurseResist
    {
        get
        {
            return statsData.Stats.BaseCurseResist;
        }
    }
    protected virtual int PoisonResist
    {
        get
        {
            return statsData.Stats.BasePoisonResist + buffManager.GetValueInt(BuffType.DEF_Poison);
        }
    }
    protected virtual int HurtResist
    {
        get
        {
            return statsData.Stats.BaseHurtResist + buffManager.GetValueInt(BuffType.DEF_Hurt);
        }
    }
    protected virtual int CurseResist
    {
        get
        {
            return statsData.Stats.BaseCurseResist + buffManager.GetValueInt(BuffType.DEF_Curse);
        }
    }

    float Poison_reg
    {
        get
        {
            return PoisonResist * 0.10f + 4;
        }
    }
    float Hurt_reg
    {
        get
        {
            return HurtResist * 0.15f + 6;
        }
    }
    float Curse_reg
    {
        get
        {
            return CurseResist * 0.05f + 2;
        }
    }

    /// <summary>
    /// 毒(継続ダメージ)
    /// /// </summary>
    int Poison
    {
        get
        {
            return Poison_var;
        }
        set
        {
            Poison_var = Mathf.Clamp(value, 0, 100);

            if (Poison_var == 100)
            {
                Poisoned();
            }
            if (isPoisoned)
            {
                Poison_var = 0;
            }
        }

    }
    /// <summary>
    /// 出血(回復力低下:自然回復力ダウン)
    /// </summary>
    int Hurt
    {
        get
        {
            return Hurt_var;
        }
        set
        {
            Hurt_var = Mathf.Clamp(value, 0, 100);

            if (Hurt_var == 100)
            {
                GotHurt();
            }
            if (isHurt)
            {
                Hurt_var = 0;
            }
        }

    }
    /// <summary>
    /// 呪い(スペル使用不能)
    /// </summary>
    int Curse
    {
        get
        {
            return Curse_var;
        }
        set
        {
            Curse_var = Mathf.Clamp(value, 0, 100);

            if (Curse_var == 100)
            {
                Cursed();
            }
            if (isCursed)
            {
                Curse_var = 0;
            }
        }

    }

    int Poison_var = 0;
    int Hurt_var = 0;
    int Curse_var = 0;

    float regbuffer_poison = 0;
    float regbuffer_hurt = 0;
    float regbuffer_curse = 0;
    float regbuffer_hp = 0;
    float regbuffer_mp = 0;










    //地形との当たり判定等
    public int TerrainBoxWidth
    {

        get
        {
            return (int)TerrainHitBox.size.x / 2;
        }
    }
    public int TerrainBoxHeight
    {

        get
        {
            return (int)TerrainHitBox.size.y / 2;
        }
    }

    const int DetectWidth = 1;
    const int DetectHeight = 1;

    //
    const int canMoveVerticalHeight = 1;

    enum TerrainType
    {
        Space,
        Wall,
        Transparent
    }

    [SerializeField]
    TerrainType[,] TerrainData;

    //地形当たり判定の左右に移動可能な空間があるかどうか
    bool canMoveRight
    {
        get
        {
            if (BodyType == bodyType.TransParent)
            {
                return true;
            }
            int xline = (TerrainBoxWidth + DetectWidth) + TerrainBoxWidth;
            int y_min = (TerrainBoxHeight + DetectHeight) - TerrainBoxHeight;
            int y_max = (TerrainBoxHeight + DetectHeight) + TerrainBoxHeight - 1;

            bool flag = true;




            for (int y = y_min; y <= y_max; y++)
            {
                if (TerrainData[xline, y] == TerrainType.Wall)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }
    }
    bool canMoveLeft
    {
        get
        {
            if (BodyType == bodyType.TransParent)
            {
                return true;
            }
            int xline = (TerrainBoxWidth + DetectWidth) - TerrainBoxWidth - 1;
            int y_min = (TerrainBoxHeight + DetectHeight) - TerrainBoxHeight;
            int y_max = (TerrainBoxHeight + DetectHeight) + TerrainBoxHeight - 1;

            bool flag = true;

            for (int y = y_min; y <= y_max; y++)
            {
                if (TerrainData[xline, y] == TerrainType.Wall)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }
    }

    //地形当たり判定の左右に移動可能(上向き斜面含む空間があるかどうか)
    bool canMoveRightUp
    {
        get
        {
            if (BodyType == bodyType.TransParent)
            {
                return true;
            }
            int xline = (TerrainBoxWidth + DetectWidth) + TerrainBoxWidth;
            int y_min = (TerrainBoxHeight + DetectHeight) - TerrainBoxHeight;
            int y_max = (TerrainBoxHeight + DetectHeight) + TerrainBoxHeight - 1;

            if (TerrainData[xline, y_min] == TerrainType.Wall || (TerrainData[xline, y_min] == TerrainType.Transparent && !isTranparent))
            {
            }
            else
            {
                return false;
            }

            for (int y = y_min + 1; y <= y_max; y++)
            {
                if (TerrainData[xline, y] == TerrainType.Wall)
                {
                    return false;
                }
            }
            return true;
        }
    }
    bool canMoveLeftUp
    {
        get
        {
            if (BodyType == bodyType.TransParent)
            {
                return true;
            }
            int xline = (TerrainBoxWidth + DetectWidth) - TerrainBoxWidth - 1;
            int y_min = (TerrainBoxHeight + DetectHeight) - TerrainBoxHeight;
            int y_max = (TerrainBoxHeight + DetectHeight) + TerrainBoxHeight - 1;

            if (TerrainData[xline, y_min] == TerrainType.Wall || (TerrainData[xline, y_min] == TerrainType.Transparent && !isTranparent))
            {
            }
            else
            {
                return false;
            }

            for (int y = y_min + 1; y <= y_max; y++)
            {
                if (TerrainData[xline, y] == TerrainType.Wall)
                {
                    return false;
                }
            }
            return true;
        }
    }


    bool canMoveDown
    {
        get
        {
            if (BodyType == bodyType.TransParent)
            {
                return true;
            }
            int yline = (TerrainBoxHeight + DetectHeight) - TerrainBoxHeight - 1;
            int x_min = (TerrainBoxWidth + DetectWidth) - TerrainBoxWidth;
            int x_max = (TerrainBoxWidth + DetectWidth) + TerrainBoxWidth - 1;

            if (TerrainData == null)
            {
                TerrainDataUpdate(AdjustedPosition);
            }

            for (int x = x_min; x <= x_max; x++)
            {
                if (TerrainData == null)
                {
                    TerrainDataUpdate(AdjustedPosition);
                }

                if (TerrainData[x, yline] == TerrainType.Wall || (TerrainData[x, yline] == TerrainType.Transparent && !isTranparent))
                {
                    return false;
                }
            }
            return true;
        }
    }

    //右下に斜面が続いているか
    bool SlopeRightDown
    {
        get
        {
            if (BodyType == bodyType.TransParent)
            {
                return true;
            }
            int yline = (TerrainBoxHeight + DetectHeight) - TerrainBoxHeight - 1;
            int x_min = (TerrainBoxWidth + DetectWidth) - TerrainBoxWidth;
            int x_max = (TerrainBoxWidth + DetectWidth) + TerrainBoxWidth - 1;

            if (TerrainData == null)
            {
                TerrainDataUpdate(AdjustedPosition);
            }

            if (TerrainData[x_min, yline] == TerrainType.Wall || (TerrainData[x_min, yline] == TerrainType.Transparent && !isTranparent))
            {
            }
            else
            {
                return false;
            }

            for (int x = x_min + 1; x <= x_max; x++)
            {
                if (TerrainData[x, yline] == TerrainType.Wall || (TerrainData[x, yline] == TerrainType.Transparent && !isTranparent))
                {
                    return false;
                }
            }

            return CheckTerrain(AdjustedPosition + Vector2.left * TerrainBoxWidth + Vector2.down * TerrainBoxHeight + Vector2.down * 1.5f + Vector2.right * 1.5f);

        }
    }

    bool SlopeLeftDown
    {
        get
        {
            if (BodyType == bodyType.TransParent)
            {
                return true;
            }
            int yline = (TerrainBoxHeight + DetectHeight) - TerrainBoxHeight - 1;
            int x_min = (TerrainBoxWidth + DetectWidth) - TerrainBoxWidth;
            int x_max = (TerrainBoxWidth + DetectWidth) + TerrainBoxWidth - 1;

            if (TerrainData == null)
            {
                TerrainDataUpdate(AdjustedPosition);
            }

            if (TerrainData[x_max, yline] == TerrainType.Wall || (TerrainData[x_max, yline] == TerrainType.Transparent && !isTranparent))
            {
            }
            else
            {
                return false;
            }

            for (int x = x_min; x <= x_max - 1; x++)
            {
                if (TerrainData[x, yline] == TerrainType.Wall || (TerrainData[x, yline] == TerrainType.Transparent && !isTranparent))
                {
                    return false;
                }
            }

            return CheckTerrain(AdjustedPosition + Vector2.right * TerrainBoxWidth + Vector2.down * TerrainBoxHeight + Vector2.down * 1.5f + Vector2.left * 1.5f);

        }
    }

    //左右に平地のまま動けるかどうか
    bool canMoveRightPlain
    {
        get
        {
            if (!canMoveRight)
            {
                return false;
            }

            if (BodyType == bodyType.TransParent)
            {
                return true;
            }
            int yline = (TerrainBoxHeight + DetectHeight) - TerrainBoxHeight - 1;
            int x_min = (TerrainBoxWidth + DetectWidth) - TerrainBoxWidth + 1;
            int x_max = (TerrainBoxWidth + DetectWidth) + TerrainBoxWidth;

            if (TerrainData == null)
            {
                TerrainDataUpdate(AdjustedPosition);
            }

            for (int x = x_min; x <= x_max; x++)
            {
                if (TerrainData[x, yline] == TerrainType.Wall || (TerrainData[x, yline] == TerrainType.Transparent && !isTranparent))
                {
                    return true;
                }
            }
            return false;
        }
    }

    bool canMoveLeftPlain
    {
        get
        {
            if (!canMoveLeft)
            {
                return false;
            }

            if (BodyType == bodyType.TransParent)
            {
                return true;
            }
            int yline = (TerrainBoxHeight + DetectHeight) - TerrainBoxHeight - 1;
            int x_min = (TerrainBoxWidth + DetectWidth) - TerrainBoxWidth - 1;
            int x_max = (TerrainBoxWidth + DetectWidth) + TerrainBoxWidth - 2;

            if (TerrainData == null)
            {
                TerrainDataUpdate(AdjustedPosition);
            }

            for (int x = x_min; x <= x_max; x++)
            {
                if (TerrainData[x, yline] == TerrainType.Wall || (TerrainData[x, yline] == TerrainType.Transparent && !isTranparent))
                {
                    return true;
                }
            }
            return false;
        }
    }


    //向いている方向に壁が存在している状態
    public bool Face_Wall()
    {
        if (faceDirection == FaceDirection.Right)
        {
            if (canMoveRightPlain || canMoveRightUp || SlopeRightDown)
            {
                return false;
            }
        }
        else if (faceDirection == FaceDirection.Left)
        {
            if (canMoveLeftPlain || canMoveLeftUp || SlopeLeftDown)
            {
                return false;
            }
        }
        return true;
    }

    //これ以上向いているほうに進むと穴に落ちて落下する状態
    protected bool Face_Hole()
    {
        if (isGround)
        {
            if (faceDirection == FaceDirection.Right)
            {
                if (canMoveRightPlain || canMoveRightUp || SlopeRightDown)
                {
                    return false;
                }
            }
            else if (faceDirection == FaceDirection.Left)
            {
                if (canMoveLeftPlain || canMoveLeftUp || SlopeLeftDown)
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }





    public bool isOnTransparent
    {
        get
        {
            int yline = (TerrainBoxHeight + DetectHeight) - TerrainBoxHeight - 1;
            int x_min = (TerrainBoxWidth + DetectWidth) - TerrainBoxWidth;
            int x_max = (TerrainBoxWidth + DetectWidth) + TerrainBoxWidth - 1;

            bool flag = true;

            for (int x = x_min; x <= x_max; x++)
            {
                if (TerrainData[x, yline] == TerrainType.Wall)
                {
                    flag = false;
                    break;
                }
            }
            return flag;

        }
    }

    bool canMoveUp
    {
        get
        {
            if (BodyType == bodyType.TransParent)
            {
                return true;
            }
            int yline = (TerrainBoxHeight + DetectHeight) + TerrainBoxHeight;
            int x_min = (TerrainBoxWidth + DetectWidth) - TerrainBoxWidth;
            int x_max = (TerrainBoxWidth + DetectWidth) + TerrainBoxWidth - 1;

            bool flag = true;

            if (TerrainData == null)
            {
                TerrainDataUpdate(AdjustedPosition);
            }

            for (int x = x_min; x <= x_max; x++)
            {
                if (TerrainData[x, yline] == TerrainType.Wall)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }
    }

    bool CheckTerrain(Vector2 pos)
    {
        Collider2D[] collider = Physics2D.OverlapPointAll(pos, Terrain);

        bool c = false;
        bool t = true;

        foreach (Collider2D s in collider)
        {
            if (s)
            {
                c = true;
            }
        }
        foreach (Collider2D s in collider)
        {
            if (s && !s.CompareTag("Transparented"))
            {
                t = false;
            }
        }


        if (c)
        {
            if (t)
            {
                if (!isTranparent)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }


    protected virtual void OnFaceWall(FaceDirection direction)
    {
        //print("wall");
    }

    /// <summary>
    /// 進行方向に足場がない
    /// </summary>
    protected virtual void OnFaceHole(FaceDirection direction)
    {
        //print("hole");
    }


    void CheckFaceState()
    {
        {
            if (!canMoveRight && !canMoveRightUp)
            {
                OnFaceWall(FaceDirection.Right);
            }
            else if (!canMoveLeft && !canMoveLeftUp)
            {
                OnFaceWall(FaceDirection.Left);
            }
        }

        if (isGround)
        {
            if (canMoveRight && !canMoveRightPlain && !SlopeRightDown && Velocity.x > 0)
            {
                OnFaceHole(FaceDirection.Right);
            }
            else if (canMoveLeft && !canMoveLeftPlain && !SlopeLeftDown && Velocity.x < 0)
            {
                OnFaceHole(FaceDirection.Left);
            }
        }
    }




    protected void TerrainDataUpdate(Vector2Int Pos)
    {
        TerrainData = new TerrainType[TerrainBoxWidth * 2 + DetectWidth * 2, TerrainBoxHeight * 2 + DetectHeight * 2];

        if (entityType == EntityType.Transparent)
        {
            for (int y = 0; y < TerrainData.GetLength(1); y++)
            {
                for (int x = 0; x < TerrainData.GetLength(0); x++)
                {
                    TerrainData[x, y] = TerrainType.Space;
                }
            }

        }
        else
        {
            Vector2 LeftDownScanPoint = Pos + Vector2.left * (TerrainBoxWidth + DetectWidth) + Vector2.down * (TerrainBoxHeight + DetectHeight) + Vector2.one * 0.5f;

            for (int y = 0; y < TerrainData.GetLength(1); y++)
            {
                for (int x = 0; x < TerrainData.GetLength(0); x++)
                {
                    if (x == 0 || x == TerrainData.GetLength(0) - 1 || y == 0 || y == TerrainData.GetLength(1) - 1)
                    {
                        Collider2D[] collider = Physics2D.OverlapPointAll(LeftDownScanPoint + Vector2.right * x + Vector2.up * y, Terrain);

                        bool c = false;
                        bool t = true;

                        foreach (Collider2D s in collider)
                        {
                            if (s)
                            {
                                c = true;
                            }
                        }
                        foreach (Collider2D s in collider)
                        {
                            if (s && !s.CompareTag("Transparented"))
                            {
                                t = false;
                            }
                        }


                        if (c)
                        {
                            if (t)
                            {
                                TerrainData[x, y] = TerrainType.Transparent;
                            }
                            else
                            {
                                TerrainData[x, y] = TerrainType.Wall;
                            }
                        }
                        else
                        {
                            TerrainData[x, y] = TerrainType.Space;

                        }
                    }
                }
            }
        }

        CheckFaceState();

    }

    //壁にめり込みうる強制的な移動
    public void ForcedMove(Vector2Int val)
    {
        if (val.x > 0)
        {
            for (int i = 0; i < val.x; i++)
            {
                MoveRight();
            }
        }
        if (val.x < 0)
        {
            for (int i = 0; i < Mathf.Abs(val.x); i++)
            {
                MoveLeft();
            }
        }
        if (val.y < 0)
        {
            for (int i = 0; i < Mathf.Abs(val.y); i++)
            {
                MoveDown();
            }
        }
        if (val.y > 0)
        {
            for (int i = 0; i < val.y; i++)
            {
                MoveUp();

            }
        }
        {
            transform.position = new Vector3(AdjustedPosition.x, AdjustedPosition.y, 0);
            TerrainDataUpdate(AdjustedPosition);
        }

    }
    //壁で止まる移動
    public void EffectedMove(Vector2Int val)
    {
        if (val.x > 0)
        {
            for (int i = 0; i < val.x; i++)
            {
                if (canMoveRight)
                {
                    MoveRight();
                }
            }
        }
        if (val.x < 0)
        {
            for (int i = 0; i < Mathf.Abs(val.x); i++)
            {
                if (canMoveLeft)
                {
                    MoveLeft();
                }
            }
        }
        if (val.y < 0)
        {
            for (int i = 0; i < Mathf.Abs(val.y); i++)
            {
                if (canMoveDown)
                {
                    MoveDown();
                }

            }
        }
        if (val.y > 0)
        {
            for (int i = 0; i < val.y; i++)
            {
                if (canMoveUp)
                {
                    MoveUp();
                }
            }
        }
        {
            transform.position = new Vector3(AdjustedPosition.x, AdjustedPosition.y, 0);
            TerrainDataUpdate(AdjustedPosition);

        }
    }
    protected void MoveRight()
    {
        AdjustedPosition += Vector2Int.right;
        TerrainDataUpdate(AdjustedPosition);

    }
    protected void MoveLeft()
    {
        AdjustedPosition += Vector2Int.left;
        TerrainDataUpdate(AdjustedPosition);
    }
    protected void MoveDown()
    {
        AdjustedPosition += Vector2Int.down;
        TerrainDataUpdate(AdjustedPosition);
    }
    protected void MoveUp()
    {
        AdjustedPosition += Vector2Int.up;
        TerrainDataUpdate(AdjustedPosition);
    }

    protected void MoveFunction(Vector2Int dir)
    {
        AdjustedPosition += dir;
        TerrainDataUpdate(AdjustedPosition);
    }


    protected override void Move()
    {
        {
            while (MoveBuffer.x >= 1)
            {
                if (canMoveRightPlain)
                {
                    MoveRight();
                    MoveBuffer.x--;
                }
                else if (SlopeRightDown)
                {
                    MoveFunction(Vector2Int.down + Vector2Int.right);
                    MoveBuffer.x--;

                }
                else if (canMoveRightUp)
                {
                    MoveFunction(Vector2Int.up + Vector2Int.right);
                    MoveBuffer.x--;

                }
                else if (canMoveRight)
                {
                    MoveRight();
                    MoveBuffer.x--;

                }
                else
                {
                    MoveBuffer.x = 0;
                }
            }

            while (MoveBuffer.x <= -1)
            {
                if (canMoveLeftPlain)
                {
                    MoveLeft();
                    MoveBuffer.x++;

                }
                else if (SlopeLeftDown)
                {
                    MoveFunction(Vector2Int.down + Vector2Int.left);
                    MoveBuffer.x++;

                }
                else if (canMoveLeftUp)
                {
                    MoveFunction(Vector2Int.up + Vector2Int.left);
                    MoveBuffer.x++;

                }
                else if (canMoveLeft)
                {
                    MoveLeft();
                    MoveBuffer.x++;

                }
                else
                {
                    MoveBuffer.x = 0;
                }
            }
            while (MoveBuffer.y >= 1)
            {
                if (canMoveUp)
                {
                    MoveUp();
                    MoveBuffer.y--;

                }
                else
                {
                    MoveBuffer.y = 0;
                    break;
                }
            }

            while (MoveBuffer.y <= -1)
            {
                if (canMoveDown)
                {
                    MoveDown();
                    MoveBuffer.y++;

                }
                else
                {
                    MoveBuffer.y = 0;
                    break;
                }
            }
        }

        transform.position = new Vector3(AdjustedPosition.x, AdjustedPosition.y, 0);

    }

    protected Vector2[] PosLog = new Vector2[240 * 8];

    protected bool[] isGroundLog = new bool[240];

    public bool isGround
    {
        get
        {
            return !canMoveDown;
        }
    }


    //nフレーム前までの間に設置しているか
    protected bool Ground(int n)
    {

        for (int i = 0; i < n; i++)
        {
            if (isGroundLog[i])
            {
                return true;
            }
        }

        return false;
    }

    [Header("攻撃判定")]
    public AC_Group[] AC_Groups;

    [Header("食らい判定")]
    public AHB_Group[] AHB_Groups;


    [System.Serializable]
    public class AC_Group
    {
        public string name;
        public AttackColider[] attackColiders;

        public GameObject[] @object;

        public void SetCollider()
        {

            List<AttackColider> list = new List<AttackColider>();
            foreach (var item in @object)
            {
                foreach (var ac in item.GetComponentsInChildren<AttackColider>())
                {
                    list.Add(ac);
                }

            }

            attackColiders = list.ToArray();

        }
    }

    [System.Serializable]
    public class AHB_Group
    {
        public string name;
        public AttackHitBox[] hitBoxes;

        public GameObject[] @object;

        public void SetCollider()
        {
            List<AttackHitBox> list = new List<AttackHitBox>();
            foreach (var item in @object)
            {
                foreach (var ac in item.GetComponentsInChildren<AttackHitBox>())
                {
                    list.Add(ac);
                }

            }

            hitBoxes = list.ToArray();

        }
    }











    void SetHitBox()
    {

        foreach (var item in AHB_Groups)
        {
            item.SetCollider();
        }

        foreach (var item in AC_Groups)
        {
            item.SetCollider();
        }

    }



    IEnumerator damage_color;

    public void Damage(Element damage, Vector2 pos, bool toRight)
    {
        OnDamageDealt(damage, pos);

        if (damage.Sum != 0)
        {
            if (damage_color != null)
            {
                StopCoroutine(damage_color);
                damage_color = null;
            }

            damage_color = DamegeColorChange();
            StartCoroutine(damage_color);


            if (hasBlood)
            {
                GenerateBlood(damage.Sum, pos, toRight);
            }
        }



        HP = HP - damage.Sum;

        Poison = Poison + damage.poison;
        Hurt = Hurt + damage.hurt;
        Curse = Curse + damage.curse;


    }

    void GenerateBlood(int amount, Vector2 pos, bool toRight)
    {

        for (int i = 0; i < 6 + amount / 36; i++)
        {
            Vector2 Velocity;

            if (toRight)
            {
                Velocity = Vector2.right * UnityEngine.Random.Range(32, 96) + Vector2.up * UnityEngine.Random.Range(20, 40);
            }
            else
            {
                Velocity = Vector2.left * UnityEngine.Random.Range(32, 96) + Vector2.up * UnityEngine.Random.Range(20, 40);

            }
            SpriteObject SO = SOM.GenerateByData(SOM.basicSODs[(int)SpriteObjectManager.DataIndex.Blood], pos);

            SO.BaseVelocity = Velocity;
            SO.BaseAcceleration = Vector2.down * 240;
        }
    }


    IEnumerator DamegeColorChange()
    {
        float CRtimer = 0;
        float length = 0.24f;

        while (CRtimer < length)
        {
            float progress = CRtimer / length;

            if (progress < 0.42f)
            {
                ColorEffect(new Color(1, 0.3f, 0.3f, 1));
            }
            else if (progress < 0.7f)
            {
                ColorEffect(Color.white);
            }
            else
            {
                ColorEffect(new Color(1, 0.3f, 0.3f, 1));
            }

            CRtimer += OwnDeltaTime;
            yield return null;
        }
        ColorEffect(Color.white);

    }


    /// <summary>
    /// ダメージを食らったときに呼ばれる関数
    /// </summary>
    protected virtual void OnDamageDealt(Element damage, Vector2 pos)
    {

    }




    public bool MPConsume(int val)
    {
        if (val <= MP)
        {
            MP -= val;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ChangeVital(float[] val)
    {
        if (val.Length == 5)
        {
            regbuffer_hp += val[0];
            regbuffer_mp += val[1];

            regbuffer_poison += val[2];
            regbuffer_hurt += val[3];
            regbuffer_curse += val[4];

            Debug.LogWarning($"HP:{HP} MP:{MP} Poison:{Poison} Hurt:{Hurt} Curse:{Curse}");
        }
        else
        {
            Debug.LogError("error!!");
        }

    }








    protected virtual void Poisoned()
    {
        print($"{gameObject.name} is Poisoned {MaxHP * 30 / (100 + PoisonResist)}");
        //毒は20秒かけて最大体力の30*100/(100+PoisonResist) のダメージを与える   
        Buff buff = new Buff { ID = "Poison", timer = 15, contents = new BuffContent[1] { new BuffContent { buffType = BuffType.HPreg, amount = -MaxHP * 2 / (100 + PoisonResist) } } };

        buffManager.ADD(buff);
    }

    bool isPoisoned
    {
        get
        {
            return buffManager.CHECK("Poison");
        }
    }

    protected virtual void GotHurt()
    {
        BuffContent content1 = new BuffContent { buffType = BuffType.HPreg, amount = -Base_HPreg * 80 / (100 + HurtResist) };
        BuffContent content2 = new BuffContent { buffType = BuffType.MPreg, amount = -Base_MPreg * 80 / (100 + HurtResist) };

        Buff buff = new Buff { ID = "Hurt", timer = 20, contents = new BuffContent[2] { content1, content2 } };

        buffManager.ADD(buff);

    }
    bool isHurt
    {
        get
        {
            return buffManager.CHECK("Hurt");
        }
    }

    protected virtual void Cursed()
    {
        BuffContent content1 = new BuffContent { buffType = BuffType.NoEffect, amount = 0 };

        Buff buff = new Buff { ID = "Curse", timer = 20, contents = new BuffContent[1] { content1 } };

        buffManager.ADD(buff);

    }
    bool isCursed
    {
        get
        {
            return buffManager.CHECK("Curse");
        }
    }




    bool Tfloor;







    public enum FaceDirection
    {
        Right,
        Left,
    }

    public FaceDirection faceDirection;

    public Vector2 FrontVector
    {
        get
        {
            switch (faceDirection)
            {
                case FaceDirection.Right:
                    return Vector2.right;

                case FaceDirection.Left:
                    return Vector2.left;

                default:
                    return Vector2.right;
            }
        }
    }

    public void Flip()
    {
        switch (faceDirection)
        {
            case FaceDirection.Right:
                faceDirection = FaceDirection.Left;
                break;

            case FaceDirection.Left:
                faceDirection = FaceDirection.Right;
                break;
        }
        CheckFaceState();
    }

    //死亡時に即座に死亡モーションになるかどうか
    public bool canDeathAnimation = true;

    /// <summary>
    /// 右向きを基準とした子transformの相対座標
    /// </summary>
    public Vector2 Entity_Rpos(Transform point, Transform root)
    {
        Vector2 v = point.position - root.position;

        switch (faceDirection)
        {
            case FaceDirection.Right:
                return v;

            case FaceDirection.Left:
                return new Vector2(-v.x, v.y);
        }
        return Vector2.zero;
    }



    public float Gravity = 1200;

    public float Speed;

    public float JumpPower;

    public float MaxFallSpeed = 960;

}















[System.Serializable]
public class EntityStats
{
    public int HP;
    public int MaxHP;

    public int MP;
    public int MaxMP;

    public float HPreg;
    public float MPreg;

    public int Attack;

    public int Poison;
    public int Hurt;
    public int Curse;
}


[System.Serializable]
public class HitTimeManager
{
    [System.Serializable]
    class HitData
    {
        public string ID;
        public float Timer;
    }

    [SerializeField] List<HitData> hitDatas = new List<HitData>();

    public void Updater(float time)
    {
        if (hitDatas.Count > 0)
        {
            for (int i = 0; i < hitDatas.Count; i++)
            {
                if (hitDatas[i].Timer > 0)
                {
                    hitDatas[i].Timer -= time;
                }
                else
                {
                    hitDatas.Remove(hitDatas[i]);
                }
            }

        }

    }

    public void ADD(string id, float timer)
    {
        if (!CHECK(id))
        {
            HitData data = new HitData
            {
                ID = id,
                Timer = timer,
            };

            hitDatas.Add(data);
        }


    }


    public bool CHECK(string id)
    {
        if (hitDatas.Count > 0)
        {
            foreach (HitData data in hitDatas)
            {
                if (data.ID == id)
                {
                    return true;
                }
            }
        }
        return false;
    }
}

[System.Serializable]
public class BuffManager
{
    [SerializeField] List<Buff> buff = new List<Buff>();

    /// <summary>
    /// 
    /// </summary>
    public enum mode
    {
        Add, Multipl
    }

    public float GetValue(BuffType type, mode mode = mode.Add, float init = 0)
    {
        float val = init;

        foreach (Buff data in buff)
        {
            foreach (BuffContent content in data.contents)
            {
                if (content.buffType == type)
                {
                    switch (mode)
                    {
                        case mode.Add:
                            val += content.amount;
                            break;

                        case mode.Multipl:
                            val *= 1 + content.amount;
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        return val;
    }

    public int GetValueInt(BuffType type, mode mode = mode.Add, float init = 0)
    {
        float val = init;

        foreach (Buff data in buff)
        {
            foreach (BuffContent content in data.contents)
            {
                if (content.buffType == type)
                {
                    switch (mode)
                    {
                        case mode.Add:
                            val += content.amount;
                            break;

                        case mode.Multipl:
                            val *= 1 + content.amount;
                            break;

                        default:
                            break;
                    }
                }
            }
        }
        return Mathf.RoundToInt(val);
    }

    public void ADD(Buff data)
    {
        for (int q = 0; q < buff.Count; q++)
        {
            if (buff[q].ID == data.ID)
            {
                buff.RemoveAt(q);
            }
        }


        buff.Add(data.ShallowCopy());
    }
    public void REMOVE(string id = "all")
    {
        if (id == "all")
        {
            buff.Clear();
        }
        else
        {
            for (int i = buff.Count - 1; i >= 0; i--)
            {
                if (buff[i].ID == id)
                {
                    buff.RemoveAt(i);
                }
            }
        }
    }
    public void Updater(float timestep)
    {
        foreach (Buff data in buff)
        {
            if (data.timer > 0)
            {
                data.timer -= timestep;
            }
        }

        for (int i = buff.Count - 1; i >= 0; i--)
        {
            if (buff[i].timer <= 0)
            {
                buff.RemoveAt(i);
            }
        }
    }
    public bool CHECK(string id = "all")
    {
        if (id == "all")
        {
            return buff.Count > 0;
        }
        else
        {
            for (int i = buff.Count - 1; i >= 0; i--)
            {
                if (buff[i].ID == id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}


/// <summary>
/// 一つのバフ
/// </summary>
[System.Serializable]
public class Buff
{
    public string ID;
    public BuffContent[] contents;
    public float timer;

    public Buff ShallowCopy()
    {
        return (Buff)MemberwiseClone();
    }
}


/// <summary>
/// バフの一つの成分
/// </summary>
[System.Serializable]
public class BuffContent
{
    public BuffType buffType;
    public float amount;
}

public enum BuffType
{
    NoEffect,

    OverrideSpecial,//特殊コマンドを上書きする

    HPreg,
    MPreg,

    Attack,

    ATK_Slash,
    ATK_Hack,
    ATK_Magic,
    ATK_Heat,
    ATK_Ice,
    ATK_Thunder,

    DEF_Slash,
    DEF_Hack,
    DEF_Magic,
    DEF_Heat,
    DEF_Ice,
    DEF_Thunder,

    DEF_Poison,
    DEF_Hurt,
    DEF_Curse,

    Speed,
    JumpPower,
    Ground,

    Invincible,
    TimeScale,


}