using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_AttackColider : AttackColider
{
    Projectile Parent
    {
        get
        {
            return GetComponentInParent<Projectile>();
        }
    }

    Entity ParentInstance
    {
        get
        {
            return GetComponentInParent<Entity>();

        }
    }


}


/*
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataTypes.Element;

public class AttackColider : MonoBehaviour
{
    private void OnValidate()
    {
        if (this.gameObject.layer != 13)
        {
            this.gameObject.layer = 13;
        }
    }


    [SerializeField] AttackType attackType = AttackType.Projectile;

    Projectile Parent
    {
        get
        {
            return GetComponentInParent<Projectile>();
        }
    }

    Entity ParentInstance
    {
        get
        {
            return GetComponentInParent<Entity>();

        }
    }



    public bool isEnable
    {
        set
        {
            for (int i = 0; i < AC_Data.Count; i++)
            {
                AC_Data[i].BoxInstance.enabled = value;
            }
            isEnableVar = value;
        }
        get
        {
            return isEnableVar;
        }
    }

    bool isEnableVar = true;




    public string AC_Name;
    public int AC_ID = -1;

    public int AC_HitCount = 0;

    public Hostility Hostility;

    public List<AC_Data> AC_Data;



    public AC_Type AC_Type = AC_Type.Once;
    /// <summary>
    /// このACが持っているダメージ値
    /// </summary>
    public Element Damage;


    /// <summary>
    /// 0:斬撃
    /// 1:打撃
    /// 2:射撃
    /// 3:熱
    /// 4:冷気
    /// 5:電気
    /// 6:神聖
    /// 7:毒
    /// 8:出血
    /// 9:呪い
    /// </summary>

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1,0,0,0.3f);

        for (int i = 0; i < AC_Data.Count; i++)
        {
            Gizmos.DrawCube(new Vector2(this.transform.position.x, this.transform.position.y) + AC_Data[i].Position, AC_Data[i].Size);
        }
    }

    public virtual void OnHit(AttackHitBox attackHit)
    {
        switch (attackType)
        {
            case AttackType.Melee:

                break;


            case AttackType.Projectile:
                Parent.OnHitTarget(attackHit);

                break;

            case AttackType.Touch:
                break;
        }

    }


    public enum AttackType
    {
        Melee,
        Projectile,
        Touch,
    }

    public virtual void Init(AttackType attacktype)
    {
        attackType = attacktype;


        for (int i = 0; i < AC_Data.Count; i++)
        {
            if (AC_Data[i].BoxInstance == null)
            {
                AC_Data[i].BoxInstance = this.gameObject.AddComponent<BoxCollider2D>();
            }
            AC_Data[i].BoxInstance.size = AC_Data[i].Size;
            AC_Data[i].BoxInstance.isTrigger = true;
            AC_Data[i].BoxInstance.offset = AC_Data[i].Position;
        }
        AC_ID = Define.GM.GotAttackID;
        isEnable = true;
        AC_HitCount = 0;
    }




}

 */