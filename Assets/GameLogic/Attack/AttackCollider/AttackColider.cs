using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataTypes.Element;
using Managers;

public class AttackColider : MonoBehaviour
{
    [SerializeField] BoxCollider2D BoxInstance;

    public OwnTimeMonoBehaviour Parent;


    Element ATK;
    public  AC_DataSet dataset;
    Hostility Hostility;
    public string AC_ID;

    public Vector2 size;

    public Hostility GetHostility
    {
        get
        {
            return Hostility;
        }
    }

    public AC_DataSet GetDataSet
    {
        get
        {
            return dataset;
        }
    }

    private void OnValidate()
    {
        if (this.gameObject.layer != 13)
        {
            this.gameObject.layer = 13;
        }

        if (Parent == null)
        {
            Parent = GetComponentInParent<OwnTimeMonoBehaviour>();
        }



        if (BoxInstance == null)
        {
            BoxInstance = gameObject.AddComponent<BoxCollider2D>();
        }
        BoxInstance.size = size;
        BoxInstance.isTrigger = true;
    }

    public void Updater()
    {
        if (size != Vector2.zero)
        {
            BoxInstance.enabled = true;
            BoxInstance.size = size;
        }
        else
        {
           BoxInstance.enabled = false;
        }

    }


    public void SetAttackColider(bool active, int ID, Element atk, bool defaultActive=false, AC_DataSet ac_DataSet = null, Hostility hostility = Hostility.Both)
    {
        BoxInstance.enabled = defaultActive;
        /*
        if (size != Vector2.zero)
        {
            this.size = size;
            BoxInstance.size = size;
        }
        else
        {
            BoxInstance.enabled = false;
        }
        */
        var = active;
        ATK = atk;

        if (ac_DataSet != null)
        {
            dataset = ac_DataSet;
            AC_ID = ac_DataSet.name + ID.ToString();
            Hostility = hostility;
        }


    }









    public void setAC_Active(bool f)
    {
        var = f;
    }





    public bool isActive
    {
        get
        {
            return var&&size!=Vector2.zero&& BoxInstance.enabled;
        }
        set
        {
            var = value;
        }

    }

    [SerializeField] bool var;

    /// <summary>
    /// このACが持っているダメージ値とバフ
    /// </summary>
    public Element Damage
    {
        get
        {
            return Cal.Deal_Damage(ATK, dataset.AttackRatio);
        }
    }





    public void OnHit(AttackHitBox attackHit)
    {

        if (Parent is Projectile)
        {
            Projectile projectile = (Projectile)Parent;
            StartCoroutine(projectile.OnHitTarget(attackHit));
        }






    }










    public virtual void Init()
    {
        var = false;
        BoxInstance.enabled = false;

    }




}
public enum AC_Type
{
    Once,//同じ対象には一度しか当たらない
    SlowTick,//同じ対象には1秒につき一回命中する
    MiddleTick,//同じ対象には0.50秒につき1回命中する
    HighTick,//同じ対象には0.25秒につき1回命中する
    EveryTick//同じ対象には2*Time.deltaTimeにつき一回命中する(バフ用)
}

[System.Serializable]
public class AC_Damage
{
    public int slash = 0;
    public int hack = 0;
    public int poke = 0;
    public int magic = 0;
    public int fire = 0;
    public int ice = 0;
    public int thunder = 0;
    public int holy = 0;

    public int poison = 0;
    public int hurt = 0;
    public int curse = 0;
}


/*
 
    public void AC_ID_Update()
    {
        AC_ID = GM.Game.GotAttackID;
    }


    private void OnValidate()
    {
        if (this.gameObject.layer != 13)
        {
            this.gameObject.layer = 13;
        }

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

    public Hostility Hostility;

    public List<AC_Data> AC_Data;

    public AC_Type AC_Type = AC_Type.Once;



    /// <summary>
    /// このACが持っているダメージ値とバフ
    /// </summary>
    public Element Damage;
    public Buff buff;




    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1,0,0,0.3f);

        for (int i = 0; i < AC_Data.Count; i++)
        {
            //Gizmos.DrawCube(AC_Data[i].BoxInstance.transform.position, AC_Data[i].BoxInstance.size);
            //Gizmos.DrawCube(new Vector2(this.transform.position.x, this.transform.position.y) + AC_Data[i].Position*transform.localScale.x , AC_Data[i].Size);
        }
    }

    public virtual void OnHit(AttackHitBox attackHit)
    {      

    }

    public virtual void Init()
    {
        isEnable = true;
        AC_ID_Update();
    }



 */