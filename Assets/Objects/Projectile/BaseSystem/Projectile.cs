using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataTypes.Element;
using Managers;
using GameConsts;
using System.Linq;

public class Projectile : OwnTimeMonoBehaviour
{
    public ProjectileData projectileData;
    public bool ShowColider = true;
    public SpriteRenderer spriteRenderer;

    protected Entity User;


    protected virtual void OnSpriteAnimationProgress(int index)
    {

    }





    void SpriteAnimation(float timer)
    {
        if (projectileData.sprites.Length > 1)
        {
            int index = Mathf.FloorToInt(timer * projectileData.AnimationSpeed);

            if(spriteRenderer.sprite!= projectileData.sprites[index % projectileData.sprites.Length])
            {
                OnSpriteAnimationProgress(index);
                spriteRenderer.sprite = projectileData.sprites[index % projectileData.sprites.Length];
            }

        }
    }

    [Header("攻撃判定")]
    public AttackColider[] attackColider = new AttackColider[1];

    [Header("くらい判定")]
    public BoxCollider2D[] colliders;

    public enum TerrainHitType
    {
        Through, Transparent_Through, Hit
    }




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

    protected int ATK_ID
    {
        get
        {
            return GM.Game.GotAttackID;
        }
    }

    public override void Updater(bool UpdateFlag = false)
    {
        SpriteAnimation(OwnTimeSiceStart);


        switch (projectileData. rotateType)
        {
            case RotateType.Fixed:
                break;


            case RotateType.Inertia:
                transform.Rotate(new Vector3(0, 0, InertiaRotateSpeed * OwnDeltaTime));
                break;


            case RotateType.Velocity:

                if (Velocity.sqrMagnitude > 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, VelocityRotation);
                }
                break;
        }

        switch (projectileData.activeMode)
        {
            case ActiveMode.OutOfScreen:
                if (!spriteRenderer.isVisible)
                {
                   DeactivateCollider();
                    Deactivate();
                }

                break;


            case ActiveMode.OutOfSection:
                if ((!spriteRenderer.isVisible && !GM.MAP.InSameSectionForActivate(GM.Player.Player.NowMapGrid,Position)))
                {
                    DeactivateCollider();
                    Deactivate();
                }

                break;
        }








        foreach (AttackColider ac in attackColider)
        {
            ac.Updater();
        }


        HitChacker();

        UpdateFunction();

        base.Updater(UpdateFlag);

    }

    protected virtual void UpdateFunction()
    {

    }




    void HitChacker()
    {
        foreach (BoxCollider2D box in colliders)
        {
            if (box.enabled)
            {
                switch (projectileData.hitType)
                {
                    case TerrainHitType.Through:
                        break;

                    case TerrainHitType.Transparent_Through:
                        {
                            Collider2D[] t = Physics2D.OverlapBoxAll(box.transform.position, box.size, box.transform.rotation.eulerAngles.z, Terrain);

                            foreach (Collider2D c2 in t)
                            {
                                if (c2)
                                {
                                    if (!c2.CompareTag("Transparented"))
                                    {
                                        StartCoroutine(OnHitTerrain(c2));
                                        break;
                                    }
                                }
                            }
                        }
                        break;

                    case TerrainHitType.Hit:
                        {
                            Collider2D[] t = Physics2D.OverlapBoxAll(box.transform.position, box.size, box.transform.rotation.eulerAngles.z, Terrain);

                            foreach (Collider2D c2 in t)
                            {
                                if (c2)
                                {
                                    StartCoroutine(OnHitTerrain(c2));

                                    break;
                                }
                            }
                        }
                        break;
                }
            }
        }
    }


    protected virtual IEnumerator OnHitTerrain(Collider2D hit)
    {
        float timer = 0;
        float DeleteTime = 0.4f;



        BaseVelocity = Vector2.zero;
        //BaseAcceleration = Vector2.zero;

        DeactivateCollider();

        while (timer < DeleteTime)
        {
            float progress = timer / DeleteTime;

            spriteRenderer.color = new Color(1, 1, 1, 1 - progress * progress);

            timer += Time.deltaTime;
            yield return null;
        }
        Deactivate();
        print("hitterrain");

    }

    protected virtual void OnHitGround(Collider2D collision)
    {
    }



    public virtual IEnumerator OnHitTarget(AttackHitBox collision)
    {
        if (projectileData.Remain)
        {
            yield break;
        }

        float timer = 0;
        float DeleteTime = 0.4f;



        BaseVelocity = Vector2.zero;
        foreach (AttackColider ac in attackColider)
        {
            ac.setAC_Active(false);
        }

        while (timer < DeleteTime)
        {
            float progress = timer / DeleteTime;

            spriteRenderer.color = new Color(1, 1, 1, 1 - progress * progress);

            timer += Time.deltaTime;
            yield return null;
        }

        Deactivate();
        print("hittarget");
    }




    Vector2 Next(Vector2 start, bool isright)
    {
        RaycastHit2D hit;

        if (isright)
        {
            hit = Physics2D.Raycast(start + Vector2.right + Vector2.up * 2, Vector2.down, 4, Terrain);
        }
        else
        {
            hit = Physics2D.Raycast(start + Vector2.left + Vector2.up * 2, Vector2.down, 4, Terrain);
        }

        if (hit && hit.distance > 0.5f)
        {
            return hit.point;
        }
        return Vector2.zero;

    }




    protected Vector2[] GeneratePos(Vector2 start, int width, int num)
    {
        Vector2 startPos = Physics2D.Raycast(start + Vector2.up * 10, Vector2.down, 16, Terrain).point;

        int length = (num) * width;

        List<Vector2> pos_left = new List<Vector2>();
        List<Vector2> pos_right = new List<Vector2>();

        {
            Vector2 SP = startPos;
            int q = 0;

            while (true)
            {
                if (Next(SP, true) != Vector2.zero)
                {
                    if (q > length)
                    {
                        break;
                    }

                    SP = Next(SP, true);
                    q++;
                    pos_right.Add(SP);
                }
                else
                {
                    break;
                }
            }
        }

        {
            Vector2 SP = startPos;
            int q = 0;

            while (true)
            {
                if (Next(SP, false) != Vector2.zero)
                {
                    if (q > length)
                    {
                        break;
                    }

                    SP = Next(SP, false);
                    q++;
                    pos_left.Add(SP);
                }
                else
                {
                    break;
                }
            }
        }

        List<Vector2> pos_left2 = new List<Vector2>();
        List<Vector2> pos_right2 = new List<Vector2>();

        for (int i = 0; i < pos_left.Count; i++)
        {
            if ((i + 1) % width == 0)
            {
                pos_left2.Add(pos_left[i]);
            }
        }
        for (int i = 0; i < pos_right.Count; i++)
        {
            if ((i + 1) % width == 0)
            {
                pos_right2.Add(pos_right[i]);
            }
        }

        List<Vector2> Result = new List<Vector2>();

        Result.Add(startPos);

        Result.AddRange(pos_left2);
        Result.AddRange(pos_right2);

        Result.OrderBy(value => Mathf.Abs(value.x - startPos.x));

        return Result.ToArray();

    }








    private void OnValidate()
    {

        if (attackColider == null)
        {
            attackColider = GetComponentsInChildren<AttackColider>();
        }

        if (spriteRenderer == null)
        {
            if (GetComponent<SpriteRenderer>())
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
            else
            {
                spriteRenderer = this.gameObject.AddComponent<SpriteRenderer>();
            }
        }

        if (colliders == null)
        {
            if (GetComponent<BoxCollider2D>())
            {
                colliders = GetComponents<BoxCollider2D>();

            }
            else
            {
                colliders = new BoxCollider2D[1] { this.gameObject.AddComponent<BoxCollider2D>() };

            }
        }
    }

    protected void OnDrawGizmos()
    {
        if (ShowColider)
        {
            Gizmos.color = new Color(0, 0, 1, 0.33f);

            foreach (BoxCollider2D c in colliders)
            {
                if (c != null)
                {
                    Gizmos.DrawCube(c.transform.position + (Vector3)c.offset, c.size);
                }
            }

            Gizmos.color = new Color(1, 0, 0, 0.33f);

            foreach (AttackColider c in attackColider)
            {
                if (c != null)
                {
                    Gizmos.DrawCube(c.transform.position, c.size);
                }
            }
        }

    }


    public void DeactivateCollider()
    {
        foreach (BoxCollider2D b in colliders)
        {
            b.enabled = false;
        }

        foreach (AttackColider ac in attackColider)
        {
            ac.setAC_Active(false);
        }
    }


    /// <summary>
    /// 飛翔物を消失させリセットする。
    /// </summary>
    public void Deactivate()
    {
        transform.position = Vector3.zero;
        gameObject.SetActive(false);
    }

    public Hostility Hostility;


    /// <summary>
    /// Fixed    回転しない
    /// Velocity 速度の方向に沿う
    /// Inertia  初速を維持して回る
    /// </summary>
    public enum RotateType
    {
        Fixed,
        Velocity,
        Inertia
    }
    [SerializeField] protected float InertiaRotateSpeed;

    /// <summary>
    /// 右向きのものは0
    /// </summary>
    [SerializeField] float RotationOffset = 0;

    float VelocityRotation
    {
        get
        {
            return -Mathf.Atan2(Velocity.x, Velocity.y) * Mathf.Rad2Deg + 90 + RotationOffset;
        }
    }


    public bool inActiveArea
    {
        get
        {
            return Define.MAP.isActivated(NowMapGrid);
        }
    }

    public void SetUseAC_Projectile(AC_set[] ac_Sets, Element atk, Entity user, int FixedID = 0)
    {
        int ID = 0;
        User = user;
        if (FixedID == 0)
        {
            ID = GM.Game.GotAttackID;
        }
        else
        {
            ID = FixedID;
        }

        for (int i = 0; i < attackColider.Length; i++)
        {

            if (i < ac_Sets.Length && ac_Sets[i].isUse)
            {
                Hostility h = Hostility.None;

                if (user != null)
                {
                    if (user is Enemy)
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
                    else if (user is Player)
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

                }
                

                attackColider[i].SetAttackColider(true, ID, atk, true, ac_Sets[i].dataSet, h);
            }
            else
            {
                attackColider[i].SetAttackColider(false, ID, atk);

            }
        }
    }
    public void ACReset()
    {
        foreach (AttackColider ac in attackColider)
        {
            Element atk = new Element();
            atk.SetValues(new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            ac.SetAttackColider(false, 0, atk);
        }
    }

    public void Fire(Vector2 Pos, Vector2 velocity, Element atk, Entity user, int FixedID = 0)
    {
        Init();

        print("Fire:" + name);

        SetUseAC_Projectile(projectileData.dataset, atk, user, FixedID);
        Position = Pos;
        BaseVelocity = velocity;
        BaseAcceleration = projectileData.acc;
        OnFireInit();

    }

    protected virtual void OnFireInit()
    {

        spriteRenderer.color = Color.white;

        foreach (BoxCollider2D b in colliders)
        {
            b.enabled = true;
        }
    }


    public void SetCondition(float val1, float val2)
    {
        InertiaRotateSpeed = val2;
    }



    bool TouchTerrain = false;
    int DeactiveTimer = 0;








}


/// <summary>
/// 攻撃判定がだれにヒットするのかを定める
/// </summary>
public enum Hostility
{
    None,//効果なし
    Player,//プレイヤーにのみ効果がある
    Enemy,//敵にのみ効果がある
    Both,//敵にも味方にも効果がある
}