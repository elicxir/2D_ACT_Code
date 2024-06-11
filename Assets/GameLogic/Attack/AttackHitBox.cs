using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataTypes.Element;
public class AttackHitBox : MonoBehaviour
{
    private void OnValidate()
    {
        Parent = GetComponentInParent<Entity>();

        if (this.gameObject.layer != 13)
        {
            this.gameObject.layer = 13;
        }
    }

    [SerializeField] Entity Parent;

    public Vector2 size;

    /// <summary>
    /// ACがこのヒットボックスにあたったときに使われる防御力
    /// </summary>

    public EntityHitBoxData entityHitBoxData;

    public Element Deffence
    {
        get
        {
            return Cal.Add(entityHitBoxData.BaseDeffence, Parent.Element_Deffence);

        }
    }

    public bool HitBoxEnable = true;

    public void Init(Entity entity)
    {
        Parent = entity;
    }
    protected LayerMask ATK_Layer = 1 << 13;



    public void Updater()
    {

        if (HitBoxEnable&& Parent.GetIsAlive&&Parent.activeAHB)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll((Vector2)transform.position, size,0, ATK_Layer);

            if (colliders.Length > 0)
            {
                for (int e = 0; e < colliders.Length; e++)
                {
                    if (colliders[e].TryGetComponent(out AttackColider ac))
                    {
                        if (ac.isActive)
                        {
                            if (Parent.canHit(ac.GetHostility))
                            {
                                F(ac, colliders[e]);
                            }
                        }
                    }
                }
            }
            else
            {
            }
        }
    }

    private void F(AttackColider ac, Collider2D c)
    {
        if (!Parent.hitTimeManager.CHECK(ac.AC_ID))
        {
            if (Parent.isInvincible)
            {
                if(ac.GetDataSet.ac_type== AC_Type.EveryTick)
                {
                    Parent.buffManager.ADD(ac.GetDataSet.buff);
                    ac.OnHit(this);

                    Vector2 pos = c.ClosestPoint(transform.position);

                    bool toRight = ac.transform.position.x < transform.position.x;

                    Parent.buffManager.ADD(ac.GetDataSet.buff);
                    Parent.Damage(Cal.Take_Damage(ac.Damage, Deffence), pos, toRight);

                    print("ダメージ判定が発生！" + Parent.name);
                }              
            }
            else
            {
                switch (ac.GetDataSet.ac_type)
                {
                    case AC_Type.Once:
                        Parent.hitTimeManager.ADD(ac.AC_ID, 30);
                        break;

                    case AC_Type.SlowTick:
                        Parent.hitTimeManager.ADD(ac.AC_ID, 1);
                        break;

                    case AC_Type.MiddleTick:
                        Parent.hitTimeManager.ADD(ac.AC_ID, 0.5f);
                        break;

                    case AC_Type.HighTick:
                        Parent.hitTimeManager.ADD(ac.AC_ID, 0.25f);
                        break;
                    case AC_Type.EveryTick:

                        Parent.buffManager.ADD(ac.GetDataSet.buff);

                        break;
                }

                ac.OnHit(this);

                Vector2 pos = c.ClosestPoint(transform.position);

                bool toRight = ac.transform.position.x < transform.position.x;

                Parent.buffManager.ADD(ac.GetDataSet.buff);
                Parent.Damage(Cal.Take_Damage(ac.Damage, Deffence), pos, toRight);

                //print("ダメージ判定が発生！" + Parent.name);
            }
        }
    }











}








[System.Serializable]
public class HitBoxData
{
    public Vector2 Position;
    public Vector2 Size;
}
