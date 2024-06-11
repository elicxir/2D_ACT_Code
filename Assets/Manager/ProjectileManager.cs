using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Managers;
public class ProjectileManager : Managers_MainGame
{
    public bool ShowColider = true;

    List<Projectile> Projectiles = new List<Projectile>();

    [SerializeField] Projectile[] ProjectilePrefabs;


    public override void OnGameStart()
    {
        InitGenerate();
    }

    int[] PrefabCount;

    public int GetIndex(string name)
    {
        for (int i = 0; i < ProjectilePrefabs.Length; i++)
        {
            if (ProjectilePrefabs[i].projectileData.name == name)
            {
                return i;
            }
        }
        Debug.LogWarning("バレットデータエラー" + name);
        return -1;
    }

    public override void OnSectionChanged(Vector2Int newSection)
    {
        for (int n = 0; n < Projectiles.Count; n++)
        {
            if (Projectiles[n].isActive)
            {
                if (!Projectiles[n].projectileData.DontDestroyOnSectionChange)
                {
                    Projectiles[n].Deactivate();
                }
            }
        }

    }

    //すべて抹消する
    public void Clear()
    {
        for (int n = 0; n < Projectiles.Count; n++)
        {

            Destroy(Projectiles[n].gameObject);
            Projectiles.Remove(Projectiles[n]);

        }
    }




    public Projectile Fire(Projectile P, Vector2 Pos, Vector2 Direction, float speed, Entity user, int FixedID = 0)
    {
        foreach (Projectile pro in Projectiles)
        {
            if (!pro.isActive)
            {
                if (pro.projectileData.name == P.projectileData.name)
                {
                    pro.gameObject.SetActive(true);
                    pro.Fire(Pos, Direction.normalized * speed, user.Element_Attack, user, FixedID);
                    return pro;
                }
            }
        }

        if (GetIndex(P.projectileData.name) != -1)
        {
            Projectile projectile = AddInstance(GetIndex(P.projectileData.name));
            projectile.gameObject.SetActive(true);
            projectile.Fire(Pos, Direction.normalized * speed, user.Element_Attack, user, FixedID);
            return projectile;
        }
        else
        {
            Projectile projectile = AddInstanceAndList(P);
            projectile.gameObject.SetActive(true);
            projectile.Fire(Pos, Direction.normalized * speed, user.Element_Attack, user, FixedID);
            return projectile;
        }


    }


    void InitGenerate()
    {
        foreach (Projectile pro in ProjectilePrefabs)
        {
            if (pro != null)
            {
                int index = GetIndex(pro.projectileData.name);

                AddInstance(index);

            }

        }
    }

    public void Generate(int index, int num = 1)
    {
        for (int i = 0; i < num; i++)
        {
            AddInstance(index);
        }
    }

    Projectile AddInstanceAndList(Projectile projectile)
    {
        Array.Resize(ref ProjectilePrefabs, ProjectilePrefabs.Length + 1);
        ProjectilePrefabs[ProjectilePrefabs.Length - 1] = projectile;

        Projectile newProjectile = Instantiate(projectile, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), this.transform);
        Projectiles.Add(newProjectile);
        newProjectile.Deactivate();

        return newProjectile;
    }

    Projectile AddInstance(int num)
    {
        Projectile newProjectile = Instantiate(ProjectilePrefabs[num], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), this.transform);
        Projectiles.Add(newProjectile);
        newProjectile.Deactivate();

        return newProjectile;
    }

    public void Erase(int index, int num)
    {
        for (int i = 0; i < num; i++)
        {
            for (int n = 0; n < Projectiles.Count; n++)
            {
                if (!Projectiles[n].isActive)
                {
                    /*
                    if (Projectiles[n].PrefabIndex == index)
                    {
                        Destroy(Projectiles[n].gameObject);
                        Projectiles.Remove(Projectiles[n]);
                        break;
                    }*/
                }
            }
        }
    }

    public override void ManagerUpdater(MainGame caller)
    {
        Projectile[] ps = Projectiles.ToArray();

        foreach (Projectile projectile in ps)
        {
            if (projectile.isActive)
            {
                projectile.Updater();
            }

        }
    }



    void Count()
    {
        for (int t = 0; t < ProjectilePrefabs.Length; t++)
        {
            PrefabCount[t] = 0;
            for (int i = 0; i < Projectiles.Count; i++)
            {
                //if (Projectiles[i].PrefabName == ProjectilePrefabs[t].name) {
                //    PrefabCount[t]++;
                //}

            }
        }
    }


    int MaxNum = 20;
    void AutoErase()
    {
        for (int t = 0; t < ProjectilePrefabs.Length; t++)
        {
            if (PrefabCount[t] > MaxNum)
            {
                //Erase(ProjectilePrefabs[t].name, PrefabCount[t] - MaxNum);
            }
        }
    }


    int Search(string name)
    {
        for (int i = 0; i < ProjectilePrefabs.Length; i++)
        {
            if (ProjectilePrefabs[i].name == name)
            {
                return i;
            }
        }


        Debug.LogWarning("バレットデータエラー");
        return 0;
    }

}
