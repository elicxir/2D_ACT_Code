using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
public class EnemyManager : Managers_MainGame
{
    List<Enemy> Enemies = new List<Enemy>();
    List<Enemy> ActivatedEnemies = new List<Enemy>();

    void OnValidate()
    {
        GetPrefabs();

        if (EnemyDatas != null)
        {
                    for (int i = 0; i < EnemyDatas.Length; i++)
        {
            EnemyDatas[i].Name = ((EnemyName)i).ToString();
        }
        }


    }

    public enum EnemyName
    {
        Zombie, Bat, Skelton,Boss
    }

    public int EnemyNumInSection
    {
        get
        {
            int count = 0;
            foreach (Enemy enemy in Enemies)
            {
                if (enemy.isActive)
                {

                    count++;

                }
            }
            return count;
        }
    }



    [Header("行動範囲マーカーを表示するか")]
    public bool ShowBehaviorMarker = true;

    private void OnDrawGizmos()
    {
        if (ShowBehaviorMarker)
        {
            foreach (EnemyPlacer EP in EnemyPlacer)
            {
                Gizmos.color = new Color(1, 1, 1, 0.5f);
                //Gizmos.DrawCube(EP.transform.position + (Vector3)EP.BehaviorRect.position, EP.BehaviorRect.size);
            }
        }
    }

    [SerializeField] VariantManager[] EnemyDatas;

    [System.Serializable]
    class VariantManager
    {
        public string Name;
        public Enemy[] Variants;
    }

    public string GetName(EnemyName type, int variant)
    {
        return EnemyDatas[(int)type].Variants[Mathf.Clamp(variant, 0, EnemyDatas[(int)type].Variants.Length - 1)].name;
    }

    public Sprite GetSprite(EnemyName type, int variant)
    {
        return EnemyDatas[(int)type].Variants[Mathf.Clamp(variant, 0, EnemyDatas[(int)type].Variants.Length - 1)].sprite_renderer.sprite;
    }


    [SerializeField] EnemyPlacer[] EnemyPlacer;

    [SerializeField] GameObject PrefabList;
    [SerializeField] GameObject EnemyInstance;
    [SerializeField] GameObject EnemyPlaceData;

    public override void GameExit()
    {
        foreach (Enemy enemy in Enemies)
        {
            enemy.ExitGame();
        }
    }

    public override void GameEnter()
    {
        foreach (Enemy enemy in Enemies)
        {
            enemy.EnterGame();
        }
    }

    public override void OnGameStart()
    {
        print("初期生成");
        for (int i = 0; i < EnemyDatas.Length; i++)
        {
            for (int j = 0; j < EnemyDatas[i].Variants.Length; j++)
            {
                AddInstance((EnemyName)i, j);
            }
        }
    }

    public override void OnSectionChanged(Vector2Int newGrid)
    {

        foreach (Enemy enemy in Enemies)
        {
            if (enemy.isActive)
            {
                enemy.DisAppear();
            }
        }

        GM.UI.BossHP.Boss.Clear();

        foreach (EnemyPlacer placer in EnemyPlacer)
        {
            if (Map.SectionIndex(newGrid) == Map.SectionIndex(placer.MapGrid))
            {
                placer.gameObject.SetActive(true);
                placer.Init();
            }
            else
            {
                if (placer.gameObject.activeSelf)
                {
                    placer.gameObject.SetActive(false);
                }
            }
        }



    }


    void GetPrefabs()
    {
        //EnemyPrefabs = PrefabList.GetComponentsInChildren<Enemy>();
        EnemyPlacer = EnemyPlaceData.GetComponentsInChildren<EnemyPlacer>();
    }



    //すべて抹消する
    public void Clear()
    {
        for (int n = 0; n < Enemies.Count; n++)
        {
            Destroy(Enemies[n].gameObject);
            Enemies.Remove(Enemies[n]);
        }
    }


    public Enemy Appear(EnemyName type, int variant, Vector2 Pos, Entity.FaceDirection direction, Rect rect)
    {

        for (int n = 0; n < Enemies.Count; n++)
        {
            if (!Enemies[n].isActive)
            {
                if (Enemies[n].type == type)
                {
                    if (Enemies[n].variant == variant)
                    {
                        Enemies[n].gameObject.SetActive(true);

                        Enemies[n].Appear(Pos, direction, rect);
                        return Enemies[n];
                    }

                }
            }
        }


        Enemy enemy = AddInstance(type, variant);
        enemy.gameObject.SetActive(true);

        enemy.Appear(Pos, direction, rect);
        return enemy;
    }


    void InitGenerate()
    {


       // for (int i = 0; i < EnemyPrefabs.Length; i++)
        {
            // AddInstance(i);
        }
    }

    public void Generate(int index, int num = 1)
    {
        for (int i = 0; i < num; i++)
        {
            // AddInstance(index);
        }
    }

    Enemy AddInstance(EnemyName type, int variant)
    {
        Enemy newEnemy = Instantiate(EnemyDatas[(int)type].Variants[Mathf.Clamp(variant, 0, EnemyDatas[(int)type].Variants.Length - 1)], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), EnemyInstance.transform);

        newEnemy.type = type;
        newEnemy.variant = variant;

        //newEnemy.DefaultParent = EnemyInstance;

        Enemies.Add(newEnemy);
        newEnemy.DisAppear();

        return newEnemy;
    }

    public void Erase(int index, int num)
    {
        for (int i = 0; i < num; i++)
        {
            for (int n = 0; n < Enemies.Count; n++)
            {
                if (!Enemies[n].isActive)
                {
                    // if (Enemies[n].PrefabIndex == index)
                    {
                        Destroy(Enemies[n].gameObject);
                        Enemies.Remove(Enemies[n]);
                        break;
                    }
                }
            }
        }
    }

    public override void ManagerUpdater(MainGame caller)
    {
        foreach (EnemyPlacer item in EnemyPlacer)
        {
            if (item.gameObject.activeSelf)
            {
                item.Updater();
            }
        }

        foreach (Enemy item in Enemies)
        {
            if (item.isActive)
            {
                item.Updater(caller.gimmickManager.EntityUpdateFlag(item.Position,item.TerrainUpdateDetectRange));
            }
        }

    }

    public override void ManagerLateUpdater(MainGame caller)
    {
        foreach (Enemy item in Enemies)
        {
            if (item.isActive)
            {
                item.LateUpdater();
            }
        }
    }
}

