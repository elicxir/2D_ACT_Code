using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class SpellFunction : SpecialFunction
{
    protected ProjectileManager projectileManager
    {
        get
        {
            return GM.Projectile;
        }
    }
    protected Player player
    {
        get
        {
            return GM.Player.Player;
        }
    }

    public bool useDataSheet = false;

    public string StringType
    {
        get
        {
            switch (spellType)
            {
                case SpellType.Switch:
                    return "[S]";
                case SpellType.Increase:
                    return "[I]";
                case SpellType.Trigger:
                    return "[T]";
            }
            return "report this to the creator";
        }
    }

    public ArtsData data;

    public string spellname
    {
        get
        {
            return data.artsname;
        }
    }
    public Sprite Sprite
    {
        get
        {
            return data.Sprite;
        }
    }
    public SpellType spellType;
    public CastType castType;

    public int MP;//通常攻撃の消費MP
    public int EmpoweredMP;//強化攻撃の消費MP

    public int Empower_Time;//チャージに必要な時間 

    public string description
    {
        get
        {
            return data.desc;
        }
    }

    public float castspeed
    {
        get
        {
            return data.castspeed;
        }
    }

    protected virtual void Start()
    {

    }









    public EntityStats stats
    {
        get
        {
            return Define.PM.Player.entityStats;
        }
    }


    public void Init(SpellType type, int mp, int empoweredmp, int empoweredtime)
    {
        MP = mp;
        EmpoweredMP = empoweredmp;
        spellType = type;
        Empower_Time = empoweredtime;
    }








    bool Toggle = false;
    bool Increase = false;


    public virtual void FixedUpdater()
    {
        switch (spellType)
        {
            case SpellType.Switch:
                if (Toggle)
                {
                    Define.PM.Player.ChangeVital(new float[5] { 0, -MP * Time.fixedDeltaTime, 0, 0, 0 });
                    OnSwitchOn();

                    if (stats.MP < MP * Time.fixedDeltaTime * 0.2f)
                    {
                        SwitchOff();
                        //Define.UM.SHOW_WARNING("-lack of MP-", Color.red);

                    }
                }
                break;

            case SpellType.Increase:
                if (Increase)
                {
                    Increasing();
                }

                break;

        }


    }

    public void Switch()
    {

        if (spellType == SpellType.Switch)
        {
            if (Toggle)
            {
                Toggle = !Toggle;
            }
            else
            {
                if (stats.MP > MP * 0.25f)
                {
                    Toggle = !Toggle;
                }
                else
                {
                    //Define.UM.SHOW_WARNING("-lack of MP-", Color.red);
                    SwitchOff();

                }



            }

        }
        ToggleOnTimer = 0;
    }

    public void SwitchOff()
    {
        Toggle = false;
        ToggleOnTimer = 0;
    }



    protected int ToggleOnTimer = 0;
    public virtual void OnSwitchOn()
    {
        ToggleOnTimer++;

    }



    protected int IncreaseTimer = 0;
    public virtual void IncreaseStart()
    {
        if (spellType == SpellType.Increase)
        {
            if (stats.MP > MP)
            {
                Increase = true;
                IncreaseTimer = 0;
                //Define.PM.isMPreg = false;
            }
            else
            {
                //Define.UM.SHOW_WARNING("-lack of MP-", Color.red);

            }

        }
    }

    public virtual void Increasing()
    {
        if (spellType == SpellType.Increase)
        {
            if (IncreaseTimer < Empower_Time && stats.MP > MP)
            {
                IncreaseTimer++;
                Define.PM.Player.ChangeVital(new float[5] { 0, -(float)EmpoweredMP / Empower_Time, 0, 0, 0 });
            }

        }
    }

    public virtual void IncreaseEnd()
    {
        if (spellType == SpellType.Increase)
        {
            if (Increase)
            {
                Define.PM.Player.ChangeVital(new float[5] { 0, -MP, 0, 0, 0 });
                EmpoweredFire((float)IncreaseTimer / Empower_Time);

            }

            Increase = false;
            IncreaseTimer = 0;
            // Define.PM.isMPreg = true;



        }
    }


    public virtual void Trigger()
    {
        if (spellType == SpellType.Trigger)
        {
            if (stats.MP > MP)
            {
                Define.PM.Player.ChangeVital(new float[5] { 0, -MP, 0, 0, 0 });
                //Fire();
            }
            else
            {
                //Define.UM.SHOW_WARNING("-lack of MP-", Color.red);

            }

        }



    }

    public virtual void Fire(Vector2 pos,Vector2 dir)
    {
        print("fire");
    }

    public virtual void EmpoweredFire(float ratio)
    {

    }
}
