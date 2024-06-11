using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Functions;
using DataTypes.Element;
using DataTypes.GameData;

public class Player : Entity
{
    public PlayerActionValues PAV;

    /// <summary>
    /// 位置用のマーカー
    /// 0:立ち投擲
    /// 1:しゃがみ投擲
    /// 2:立ちレイピア
    /// 3:しゃがみレイピア
    /// 4:空上レイピア
    /// 5:空下レイピア
    /// 6:
    /// 7:
    /// 8:
    /// 9:
    /// </summary>
    public Transform[] markers;

    public Vector2 CameraCenterPos
    {
        get
        {
            return Position + cameraAdjust;
        }
    }
    Vector2 cameraAdjust = Vector2.zero;

    InputSystemManager INPUT
    {
        get
        {
            return GM.Game.Input_Manager;
        }
    }
    PlayerManager PM
    {
        get
        {
            return GM.Player;
        }
    }

    public enum State
    {
        Stand,
        Walk,
        Jump,
        Evade,
        Falling,//落下中
        Crouch,
        Sliding,
        DiveKick,
        Cast1,//投擲
        Cast1_Air,//投擲:空中
        Cast1_Crouch,
        KnockBack,
        KnockBack_Air,
        KnockBack_Crouch,
        Death,
        Item,
        Item_Crouch,
        Rapier,
        Rapier_Crouch,
        Rapier_AirDown,
        Rapier_AirUp,
    }

    //釘付け状態(ジャンプとスライディングが不可)
    bool buff_grounded
    {
        get
        {
            return buffManager.GetValue(BuffType.Ground) > 0;
        }
    }



    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(Position, TerrainUpdateDetectRange);
    }



    public override void Init()
    {
        GM.Player.Player.SetEquipmentData();

        base.Init();

        OverrideAction((int)State.Stand);
    }

    public State PlayerState
    {
        get
        {
            return (State)ActionIndex;
        }
    }

    public State BeforeState
    {
        get
        {
            return (State)BeforeActionIndex;

        }
    }


    protected override int Base_Attack => PM.Attack;
    protected override float Base_MPreg => PM.MPreg;
    protected override int MaxHP => PM.maxHP;
    protected override int MaxMP => PM.maxMP;





    protected override void Poisoned()
    {
        base.Poisoned();
        GM.UI.UI_Warning.SHOW_WARNING("-Poisoned-", new Color(1, 0, 1, 1));
    }
    protected override void Cursed()
    {
        base.Cursed();
        GM.UI.UI_Warning.SHOW_WARNING("-Cursed-", new Color(0, 1, 1, 1));
    }
    protected override void GotHurt()
    {
        base.GotHurt();
        GM.UI.UI_Warning.SHOW_WARNING("-Bleeding-", new Color(1, 0, 0, 1));

    }




    protected override IEnumerator StateMachine()
    {
        while (true)
        {
            switch (PlayerState)
            {
                case State.Stand:
                case State.Falling:
                case State.Crouch:
                case State.Walk:
                case State.Jump:
                    DoAction((int)PlayerState);
                    break;

                case State.Sliding:
                    TerrainBoxTypeChange(TerrainBoxType.Sliding, TerrainBoxType.Crouch);
                    DoAction((int)State.Crouch);
                    break;

                case State.Cast1:
                case State.KnockBack:
                case State.Item:
                case State.Rapier:
                case State.Evade:
                    DoAction((int)State.Stand);
                    break;

                case State.Cast1_Air:
                case State.KnockBack_Air:
                case State.Rapier_AirDown:
                case State.Rapier_AirUp:
                    DoAction((int)State.Jump);
                    break;

                case State.Cast1_Crouch:
                case State.KnockBack_Crouch:
                case State.Item_Crouch:
                case State.Rapier_Crouch:
                    DoAction((int)State.Crouch);
                    break;
            }
            yield return NowAction;
        }
    }

    [SerializeField] SpecialFunction[] Special = new SpecialFunction[6];
    int SP_SelectIndex = 0;

    void SpecialUpdater()
    {
        if (GM.Inputs.ButtonDown(Control.SpecialChange))
        {
            print("special change");

            for (int i = 0; i < Special.Length; i++)
            {
                SP_SelectIndex = (SP_SelectIndex + 1) % Special.Length;

                if (Special[SP_SelectIndex] != null)
                {
                    break;
                }
            }
            Special_UIUpdate();
        }

        Special[SP_SelectIndex].TemporalFunction(GM.Inputs.Button(Control.Special));

    }











    [SerializeField] ConsumableFunction[] Consumable = new ConsumableFunction[3];
    [SerializeField] SpellFunction[] Spell = new SpellFunction[4];
    [SerializeField] AccessoryFunction[] Accessory = new AccessoryFunction[4];


    public void SetEquipmentData()
    {
        EquipData data = GM.Game.PlayData.equipData;

        for (int i = 0; i < 3; i++)
        {
            if (data.consumableID[i] != -1)
            {
                Consumable[i] = GM.EDM.GET_CONSUMABLE_FUNCTION(data.consumableID[i]);
            }
            else
            {
                Consumable[i] = null;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (data.spellID[i] != -1)
            {
                Spell[i] = GM.EDM.GET_SPELL_FUNCTION(data.spellID[i]);
            }
            else
            {
                Spell[i] = null;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (data.accessoryID[i] != -1)
            {
                Accessory[i] = GM.EDM.GET_ACCESSORY_FUNCTION(data.accessoryID[i]);
            }
            else
            {
                Accessory[i] = null;
            }
        }

        ItemValidate();
        Special_UIUpdate();
    }

    void AccessoryUpdater()
    {
        foreach (AccessoryFunction f in Accessory)
        {
            if (f != null)
            {
                f.Updater();
            }
        }
    }


    public bool SelectedPallet = false;//falseがAでtrueがB

    void SpellUpdater()
    {
        if (GM.Inputs.ButtonDown(Control.SpecialChange))
        {
            print("pallet change");

            bool canChange = false;

            if (SelectedPallet)
            {
                if (Spell[0] == null && Spell[1] == null)
                {

                }
                else
                {
                    canChange = true;
                }
            }
            else
            {
                if (Spell[2] == null && Spell[3] == null)
                {

                }
                else
                {
                    canChange = true;
                }
            }

            if (canChange)
            {
                GM.UI.ArtsPallet.SetNowSelected(SelectedPallet);

                if (GM.UI.ArtsPallet.Pallet())
                {
                    SelectedPallet = !SelectedPallet;
                }
                else
                {

                }
            }

        }
        else
        {
            if (GM.Inputs.ButtonDown(Control.Special))
            {
                if (SelectedPallet)
                {
                    ArtsExecuter(Spell[2]);
                }
                else
                {
                    ArtsExecuter(Spell[0]);
                }
            }
        }
    }

    void ItemUpdater()
    {/*
        if (GM.Inputs.ButtonDown(Control.Item))
        {
            if (GM.Inputs.Button(Control.Up))
            {
                ItemSelect();
            }
            else
            {
                ItemExecuter(Consumable[itemSelectIndex]);
            }
        }*/
    }

    void CASTSPELL(SpellFunction spell, Vector2 pos)
    {
        MP -= spell.MP;
        switch (PlayerState)
        {
            case State.Cast1:
            case State.Cast1_Air:
            case State.Cast1_Crouch:
            case State.Rapier:
            case State.Rapier_Crouch:
                spell.Fire(pos, FrontVector);
                break;
            case State.Rapier_AirDown:
                spell.Fire(pos, FrontVector + Vector2.down);
                break;
            case State.Rapier_AirUp:
                spell.Fire(pos, FrontVector * 2 + Vector2.up);
                break;
        }
    }
    void CASTSPELL_WITHOUT_MP(SpellFunction spell, Vector2 pos)
    {
        switch (PlayerState)
        {
            case State.Cast1:
            case State.Cast1_Air:
            case State.Cast1_Crouch:
            case State.Rapier:
            case State.Rapier_Crouch:
                spell.Fire(pos, FrontVector);
                break;
            case State.Rapier_AirDown:
                spell.Fire(pos, FrontVector + Vector2.down);
                break;
            case State.Rapier_AirUp:
                spell.Fire(pos, FrontVector * 2 + Vector2.up);
                break;
        }
    }


    void USEITEM(ConsumableFunction item)
    {
        item.Active();
        item.useCount--;
        Special_UIUpdate();
    }

    void ArtsExecuter(SpellFunction spell)
    {


        if (spell != null)
        {
            switch (spell.spellType)
            {
                case SpellType.Trigger:

                    if (MP >= spell.MP)
                    {
                        switch (spell.castType)
                        {
                            case CastType.Rapier:
                                Rapier(spell, (Vector2 pos) => CASTSPELL_WITHOUT_MP(spell, pos));
                                break;

                            case CastType.Cast1:
                                Cast1((Vector2 pos) => CASTSPELL(spell, pos), spell.castspeed);
                                break;
                            default:
                                break;
                        }




                    }





                    break;
                case SpellType.Switch:
                    break;
                case SpellType.Increase:
                    break;
                case SpellType.Undefined:
                    break;
            }




        }
    }




    void ItemExecuter(ConsumableFunction item)
    {
        if (item != null)
        {
            if (item.Require() && item.useCount > 0)
            {
                Item(() => USEITEM(item));
            }
        }
    }

    bool canItem = true;

    int itemSelectIndex = 0;





    void Item(Action action)
    {
        if (Controll && isAlive)
        {
            if (isGround)
            {
                if (PlayerState == State.Crouch)
                {
                    EA_Player_Item item = Actions[(int)State.Item_Crouch] as EA_Player_Item;
                    item.action = action;

                    OverrideAction((int)State.Item_Crouch);
                }
                else if (PlayerState == State.Stand || PlayerState == State.Walk)
                {
                    EA_Player_Item item = Actions[(int)State.Item] as EA_Player_Item;
                    item.action = action;

                    OverrideAction((int)State.Item);
                }
            }
        }
    }

    void ItemValidate()
    {
        if (Consumable[itemSelectIndex] != null)
        {
            return;
        }

        for (int i = 0; i < Consumable.Length; i++)
        {
            itemSelectIndex = (itemSelectIndex + 1) % Consumable.Length;

            if (Consumable[itemSelectIndex] != null)
            {
                return;
            }
        }
    }
    /*
    void ItemSelect()
    {
        print("itemselect");

        for (int i = 0; i < Consumable.Length; i++)
        {
            itemSelectIndex = (itemSelectIndex + 1) % Consumable.Length;

            if (Consumable[itemSelectIndex] != null)
            {
                break;
            }
        }

        Special_UIUpdate();
    }

    void Item_UIUpdate()
    {
        GM.UI.Item.SetItemGraph(Consumable[itemSelectIndex]);

    }
    */
    void Special_UIUpdate()
    {
        print("update");

        if (Special[SP_SelectIndex] is ConsumableFunction)
        {
            GM.UI.Item.SetItemGraph((ConsumableFunction)Special[SP_SelectIndex]);
        }
        else if (Special[SP_SelectIndex] is SpellFunction)
        {
            GM.UI.Item.SetSpellGraph((SpellFunction)Special[SP_SelectIndex]);
        }
        else
        {
            GM.UI.Item.SetSpellGraph(null);

        }


    }


    public void Onsave()
    {
        FullRegene();

        Special_UIUpdate();
    }

    bool Controll
    {
        get
        {
            return GM.Player.Controll;
        }
    }
    bool C_Input(Control control)
    {
        return Controll && INPUT.Button(control);
    }
    bool C_InputDown(Control control)
    {
        return Controll && INPUT.ButtonDown(control);

    }
    bool C_InputUp(Control control)
    {
        return Controll && INPUT.ButtonUp(control);

    }

    Vector2 C_InputVector()
    {
        if (Controll)
        {
            return INPUT.InputVector();
        }
        else
        {
            return Vector2.zero;
        }
    }

    protected override void EntityUpdater()
    {
        SpecialUpdater();
        AccessoryUpdater();

        switch (PlayerState)
        {
            case State.Stand:
                DAIE.SetActive(false);

                if (isGround)
                {
                    if (canEvade)
                    {
                        if (C_InputDown(Control.Evade) && !buff_grounded)
                        {
                            Evade_Function();
                            break;
                        }
                    }
                    if (C_Input(Control.Down))
                    {
                        PlayerFlip();
                        TerrainBoxTypeChange(TerrainBoxType.Stand, TerrainBoxType.Crouch);
                        OverrideAction((int)State.Crouch);
                        break;
                    }

                    if (canJump)
                    {
                        if (C_InputDown(Control.Jump) && !buff_grounded)
                        {
                            PlayerFlip();
                            OverrideAction((int)State.Jump);
                            break;
                        }

                    }
                    if (canWalk)
                    {
                        if (C_Input(Control.Right))
                        {
                            PlayerFlip();
                            OverrideAction((int)State.Walk);
                            break;
                        }
                        else if (C_Input(Control.Left))
                        {
                            PlayerFlip();
                            OverrideAction((int)State.Walk);
                            break;
                        }
                    }

                    if (PM.OnForcedControll && BaseVelocity.x != 0)
                    {
                        PlayerFlip();
                        OverrideAction((int)State.Walk);
                        break;
                    }


                }
                else
                {
                    if (Velocity.y < 0)
                    {
                        OverrideAction((int)State.Falling);
                    }
                }

                break;
            case State.Walk:
                DAIE.SetActive(false);

                if (isGround)
                {
                    if (canEvade)
                    {
                        if (C_InputDown(Control.Evade) && !buff_grounded)
                        {
                            Evade_Function();
                            break;
                        }
                    }
                    if (C_Input(Control.Down))
                    {
                        PlayerFlip();
                        TerrainBoxTypeChange(TerrainBoxType.Stand, TerrainBoxType.Crouch);
                        OverrideAction((int)State.Crouch);
                        break;
                    }

                    if (canJump)
                    {
                        if (C_InputDown(Control.Jump) && !buff_grounded)
                        {
                            PlayerFlip();
                            OverrideAction((int)State.Jump);
                            break;
                        }

                    }



                    if ((C_Input(Control.Right) && FrontVector.x > 0) || (C_Input(Control.Left) && FrontVector.x < 0) || (PM.OnForcedControll && BaseVelocity.x != 0))
                    {
                    }
                    else
                    {
                        OverrideAction((int)State.Stand);
                        break;
                    }

                }
                else
                {
                    OverrideAction((int)State.Falling);
                    break;
                }

                break;
            case State.Jump:
                DAIE.SetActive(false);

                if (Velocity.y <= 8)
                {
                    OverrideAction((int)State.Falling);
                }
                break;
            case State.Evade:
                DAIE.SetActive(true);
                if (isGround)
                {
                    if (canEvade)
                    {
                        if (C_InputDown(Control.Evade) && !buff_grounded)
                        {

                            Evade_Function();
                            break;
                        }
                    }
                }

                break;

            case State.Falling:

                if (Velocity.y == -MaxFallSpeed)
                {
                    DAIE.SetActive(true);

                }
                else
                {
                    DAIE.SetActive(false);

                }

                if (isGround && Velocity.y <= 8)
                {
                    if (Velocity.y == -MaxFallSpeed)
                    {
                        TerrainBoxTypeChange(TerrainBoxType.Stand, TerrainBoxType.Crouch);
                        OverrideAction((int)State.Crouch);
                    }
                    else
                    {
                        OverrideAction((int)State.Stand);
                    }

                }

                break;

            case State.Crouch:
                DAIE.SetActive(false);
                if (PlayerFlip())
                {
                    OverrideAction((int)State.Crouch);
                }
                if (isGround)
                {
                    if (!C_Input(Control.Down))
                    {
                        if (canStandUp() && BeforeState != State.Falling)
                        {
                            TerrainBoxTypeChange(TerrainBoxType.Crouch, TerrainBoxType.Stand);
                            OverrideAction((int)State.Stand);
                        }
                    }
                    else
                    {
                        if (C_InputDown(Control.Evade))
                        {
                            if (isOnTransparent)
                            {
                                if (T_Floor_C != null)
                                {
                                    StopCoroutine(T_Floor_C);
                                    T_Floor_C = null;
                                }

                                T_Floor_C = StartCoroutine(T_Floor());
                            }
                            else
                            {
                                if (canSliding && !buff_grounded)
                                {
                                    PlayerFlip();
                                    TerrainBoxTypeChange(TerrainBoxType.Crouch, TerrainBoxType.Sliding);
                                    OverrideAction((int)State.Sliding);
                                }
                            }
                        }
                    }
                }
                else
                {
                    TerrainBoxTypeChange(TerrainBoxType.Crouch, TerrainBoxType.Stand);
                    OverrideAction((int)State.Falling);
                }
                break;

            case State.Sliding:
                DAIE.SetActive(true);
                if (isGround)
                {
                    if (C_InputDown(Control.Jump) && C_Input(Control.Down) && canSliding && !buff_grounded)
                    {
                        PlayerFlip();

                        OverrideAction((int)State.Sliding);
                    }
                }
                else
                {
                    TerrainBoxTypeChange(TerrainBoxType.Sliding, TerrainBoxType.Stand);
                    OverrideAction((int)State.Falling);
                }

                break;

            case State.Rapier_Crouch:
            case State.Cast1_Crouch:
                DAIE.SetActive(false);
                if (isGround)
                {
                    {
                        if (C_InputDown(Control.Jump))
                        {
                            if (isOnTransparent)
                            {
                                if (T_Floor_C != null)
                                {
                                    StopCoroutine(T_Floor_C);
                                    T_Floor_C = null;
                                }

                                T_Floor_C = StartCoroutine(T_Floor());
                            }
                            else
                            {
                                if (canSliding && !buff_grounded)
                                {
                                    PlayerFlip();

                                    TerrainBoxTypeChange(TerrainBoxType.Crouch, TerrainBoxType.Sliding);
                                    OverrideAction((int)State.Sliding);
                                }
                            }
                        }
                    }
                }
                else
                {
                    OverrideAction((int)State.Falling);
                }
                break;

            case State.Cast1_Air:
            case State.KnockBack_Air:
            case State.Rapier_AirDown:
            case State.Rapier_AirUp:

                DAIE.SetActive(false);
                if (isGround && Velocity.y <= 0)
                {
                    OverrideAction((int)State.Stand);
                }
                else
                {
                }
                break;

            case State.KnockBack:
            case State.Item:
            case State.Cast1:
            case State.Rapier:

                DAIE.SetActive(false);
                if (isGround)
                {
                    if (canEvade)
                    {
                        if (C_InputDown(Control.Evade) && !buff_grounded)
                        {
                            Evade_Function();
                            break;
                        }
                    }
                }
                else
                {
                    OverrideAction((int)State.Falling);
                }
                break;

            case State.KnockBack_Crouch:
            case State.Item_Crouch:

                DAIE.SetActive(false);
                if (isGround)
                {

                }
                else
                {
                    OverrideAction((int)State.Falling);
                }
                break;
        }
    }

    public bool Posture_Crouch
    {
        get
        {
            return PlayerState == State.Crouch || PlayerState == State.Rapier_Crouch || PlayerState == State.Cast1_Crouch || PlayerState == State.Sliding || PlayerState == State.Item_Crouch || PlayerState == State.KnockBack_Crouch;
        }
    }


    void Cast1(Action<Vector2> action, float castspeed)
    {
        if (canCast1 && Controll && isAlive)
        {
            if (!isGround)
            {
                EA_Player_Cast1 cast = Actions[(int)State.Cast1_Air] as EA_Player_Cast1;
                cast.action = action;
                cast.castspeed = castspeed;
                OverrideAction((int)State.Cast1_Air);
            }
            else
            {
                if (Posture_Crouch)
                {
                    EA_Player_Cast1 cast = Actions[(int)State.Cast1_Crouch] as EA_Player_Cast1;
                    cast.action = action;
                    cast.castspeed = castspeed;
                    OverrideAction((int)State.Cast1_Crouch);
                }
                else
                {
                    EA_Player_Cast1 cast = Actions[(int)State.Cast1] as EA_Player_Cast1;
                    cast.action = action;
                    cast.castspeed = castspeed;

                    OverrideAction((int)State.Cast1);
                }
            }
        }
    }

    void Rapier(SpellFunction spell, Action<Vector2> action)
    {
        if (canCast1 && Controll && isAlive)
        {
            MP -= spell.MP;

            if (!isGround)
            {
                if (C_Input(Control.Down))
                {
                    Rapier_Function((Spell_Rapier)spell, State.Rapier_AirDown, action);

                }
                else
                {
                    Rapier_Function((Spell_Rapier)spell, State.Rapier_AirUp, action);

                }
            }
            else
            {
                if (Posture_Crouch)
                {
                    Rapier_Function((Spell_Rapier)spell, State.Rapier_Crouch, action);

                }
                else
                {
                    Rapier_Function((Spell_Rapier)spell, State.Rapier, action);
                }
            }
        }
    }


    void Rapier_Function(Spell_Rapier spell, Player.State state, Action<Vector2> action)
    {
        EA_Player_Rapier cast = (EA_Player_Rapier)Actions[(int)state];
        cast.action = action;

        cast.index = spell.index;
        cast.castspeed = spell.castspeed;
        cast.dataset = spell.dataset;

        OverrideAction((int)state);
    }

    void Evade_Function()
    {
        PlayerFlip();
        StopAction();
        EA_Player_Evade evade = (EA_Player_Evade)Actions[(int)State.Evade];
        evade.canMoveBack = true;
        OverrideAction((int)State.Evade);
    }

    void Evade_StopMove()
    {
        EA_Player_Evade evade = (EA_Player_Evade)Actions[(int)State.Evade];
        BaseVelocity = Vector2.zero;
        MoveBuffer.x = 0;
        isEffected = false;
        evade.canMoveBack = false;

    }















    const float Mutekizikan = 1.1f;

    protected override void OnDamageDealt(Element damage, Vector2 pos)
    {
        if (damage.Sum > 16)
        {
            buffManager.ADD(new Buff { contents = new BuffContent[1] { new BuffContent { buffType = BuffType.Invincible, amount = 1 } }, ID = pos.ToString(), timer = Mutekizikan });

            KnockBack();
        }
    }


    void KnockBack()
    {
        if (!isGround)
        {
            OverrideAction((int)State.KnockBack_Air);
        }
        else
        {
            if (Posture_Crouch)
            {
                OverrideAction((int)State.KnockBack_Crouch);
            }
            else
            {
                OverrideAction((int)State.KnockBack);
            }
        }
    }



    protected override IEnumerator DeathCoRoutine()
    {
        StartCoroutine(GM.Game.FadeOut(1.8f));
        DoAction((int)State.Death);
        yield return NowAction;
        GM.Game.StateQueue((int)gamestate.GameOver);
    }




    bool PlayerFlip()
    {
        if ((C_Input(Control.Right) && FrontVector.x < 0) || (C_Input(Control.Left) && FrontVector.x > 0))
        {
            Flip();
            return true;
        }
        else
        {
            return false;
        }
    }

    Coroutine T_Floor_C;
    IEnumerator T_Floor()
    {
        isTranparent = true;
        float timer = 0;
        while (timer < 0.12f)
        {
            timer += OwnDeltaTime;
            yield return null;
        }
        isTranparent = false;

    }


    bool canStandUp()
    {
        return true;
    }

    public enum TerrainBoxType
    {
        Stand, Crouch, Sliding
    }

    public bool canTerrainBoxTypeChange(TerrainBoxType to, int front = 0, int width = 0)
    {
        int fromY = Mathf.RoundToInt(TerrainHitBox.size.y);
        int toY = TerrainBox[(int)to].y;

        if (toY < fromY)
        {
            return true;
        }

        Collider2D hit;

        if (width == 0)
        {
            hit = Physics2D.OverlapBox(Position + FrontVector * front + Vector2.up * fromY / 2 + Vector2.up * (toY - fromY) / 2, new Vector2(TerrainHitBox.size.x, (TerrainBox[(int)to] - TerrainHitBox.size).y), 0, Terrain);

        }
        else
        {
            hit = Physics2D.OverlapBox(Position + FrontVector * front + Vector2.up * fromY / 2 + Vector2.up * (toY - fromY) / 2, new Vector2(width, (TerrainBox[(int)to] - TerrainHitBox.size).y), 0, Terrain);

        }


        return !hit;
    }

    void TerrainBoxTypeChange(TerrainBoxType from, TerrainBoxType to)
    {
        int delta = (TerrainBox[(int)to].y - Mathf.RoundToInt(TerrainHitBox.size.y)) / 2;

        TerrainHitBox.size = TerrainBox[(int)to];

        Position += Vector2.up * delta;
        cameraAdjust -= Vector2.up * delta;
        SpriteOffset -= Vector2.up * delta;

        TerrainDataUpdate(AdjustedPosition);
    }

    [SerializeField] Vector2Int[] TerrainBox;



    [SerializeField] bool canWalk = true;

    [SerializeField]
    bool canEvade = true;
    [SerializeField] bool canSliding = true;

    [SerializeField] bool canJump = true;
    [SerializeField] bool canCast1 = true;

    int AirjumpCounter;




    //Flag_は対応アクションが解放されているかどうか
    bool Flag_Airjump;

    bool Flag_Sliding;

    protected override void OnFaceHole(FaceDirection direction)
    {
        if (PlayerState == State.Evade)
        {
            if (Velocity.x * C_InputVector().x < 0)
                Evade_StopMove();
        }



    }



    int StateTimer;//ステート更新からの経過フレーム数

    State[] StateLog = new State[240];//nフレーム前のstate情報 0ならそのループのstate

    protected override void EntityMove()
    {/*
        Velocity.x = 0;

        if (PM.Controll && Input.GetKey(KeyCode.RightArrow))
        {
            Velocity.x = Speed* (1+buffManager.GetValue(BuffType.Speed));
            faceDirection = FaceDirection.Right;

        }

        if (PM.Controll && Input.GetKey(KeyCode.LeftArrow))
        {
            Velocity.x = -Speed * (1+buffManager.GetValue(BuffType.Speed));
            faceDirection = FaceDirection.Left;
        }
        */
        //SetTransparent();
        /*
        if (isGround)
        {
            AirjumpCounter = 0;
        }

        if (PM.Controll&& INPUT.ButtonDown(Control.Jump)&&!(INPUT.Button("Down")&& isOnTransparent))
        {
            if (isGround)
            {
                Velocity.y = JumpPower;// * (1+buffManager.GetValue(BuffType.Speed, BuffManager.mode.Multipl,1));
            }
            else if (Flag_Airjump && AirjumpCounter == 0)
            {
                Velocity.y = JumpPower;
                AirjumpCounter++;
            }
        }*/
    }

}