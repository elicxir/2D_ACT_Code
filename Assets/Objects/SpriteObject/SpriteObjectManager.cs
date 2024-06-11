using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//SpriteObject‚Í‰æ‘œ‚ğ‚Â‚ªUŒ‚”»’è‚ğ‚à‚½‚È‚¢B
public class SpriteObjectManager : Managers_MainGame
{
    List<SpriteObject> SpriteObjects = new List<SpriteObject>();
    [SerializeField] SpriteObject[] SpriteObjectPrefabs;

    [SerializeField] SpriteObject baseObject;

    public enum DataIndex
    {
        HitEffect,
        Blood,
        BurnFire,
        PoisonEffect,
    }

    public SpriteObjectData[] basicSODs;




    public int GetIndex(string name)
    {
        for (int i = 0; i < SpriteObjectPrefabs.Length; i++)
        {
            if (SpriteObjectPrefabs[i].data.name == name)
            {
                return i;
            }
        }

        Debug.LogWarning("ƒGƒ‰[" + name);
        return -1;
    }
    public override void OnGameStart()
    {
        InitGenerate();
    }


    const int SO_Generate_Num = 12;

    void InitGenerate()
    {
        for (int i = 0; i < SO_Generate_Num; i++)
        {
            AddInstance(baseObject);
        }

        for (int i=0;i< SpriteObjectPrefabs.Length; i++)
        {
            AddInstance(i);
        }

    }

    public SpriteObject GenerateByData(SpriteObjectData data, Vector2 Pos,bool isFlip=false)
    {
        switch (data.generatePrefab)
        {
            case SpriteObjectData.GeneratePrefab.SO:

                foreach (SpriteObject so in SpriteObjects)
                {
                    if (!so.isActive)
                    {
                        if (so.GetType() == typeof(SpriteObject))
                        {
                            {
                                so.gameObject.SetActive(true);
                                so.data = data;
                                so.Generate(Pos, isFlip);
                                return so;
                            }
                        }
                    }
                }

                {
                    SpriteObject SpriteObject = AddInstanceAndList(baseObject);
                    SpriteObject.gameObject.SetActive(true);
                    SpriteObject.data = data;
                    SpriteObject.Generate(Pos, isFlip);
                    return SpriteObject;
                }

            case SpriteObjectData.GeneratePrefab.Specific:

                foreach (SpriteObject so in SpriteObjects)
                {
                    if (!so.isActive)
                    {
                        if (so.data.name == data.name)
                        {
                            so.gameObject.SetActive(true);
                            so.Generate(Pos, isFlip);
                            return so;
                        }

                    }
                }

                if (GetIndex(data.name) != -1)
                {
                    Debug.LogWarning("SpriteObjectPregabError!!");
                    return null;
                }
                else
                {
                    SpriteObject SpriteObject = AddInstance(GetIndex(data.name));
                    SpriteObject.gameObject.SetActive(true);
                    SpriteObject.Generate(Pos, isFlip);
                    return SpriteObject;
                }
        }
        return null;
    }


    /*
    public SpriteObject GenerateByName(string name, Vector2 Pos)
    {


        foreach (SpriteObject so in SpriteObjects)
        {
            if (!so.isActive)
            {
                if (so.data.name == name)
                {
                    so.gameObject.SetActive(true);
                    so.Generate(Vector2.zero);
                    return so;
                }
            }
        }

        SpriteObject SpriteObject = AddInstance(GetIndex(name));
        SpriteObject.gameObject.SetActive(true);
        SpriteObject.Generate(Vector2.zero);
        return SpriteObject;
    }
    */


    public SpriteObject Generate(SpriteObject @object, Vector2 Pos)
    {
        switch (@object.data.generatePrefab)
        {
            case SpriteObjectData.GeneratePrefab.SO:

                foreach (SpriteObject so in SpriteObjects)
                {
                    if (!so.isActive)
                    {
                        if (so.GetType() == typeof(SpriteObject))
                        {
                            {
                                so.gameObject.SetActive(true);
                                so.data = @object.data;
                                so.Generate(Pos);
                                return so;
                            }
                        }
                    }
                }

                {
                    SpriteObject SpriteObject = AddInstance(baseObject);
                    SpriteObject.gameObject.SetActive(true);
                    SpriteObject.data = @object.data;
                    SpriteObject.Generate(Pos);
                    return SpriteObject;
                }

            case SpriteObjectData.GeneratePrefab.Specific:

                foreach (SpriteObject so in SpriteObjects)
                {
                    if (!so.isActive)
                    {
                        if (so.data.name == @object.data.name)
                        {
                            so.gameObject.SetActive(true);
                            so.Generate(Pos);
                            return so;
                        }

                    }
                }

                if (GetIndex(@object.data.name) != -1)
                {
                    SpriteObject SO = AddInstanceAndList(@object);
                    SO.gameObject.SetActive(true);
                    SO.Generate(Pos);
                    return SO;
                }
                else
                {
                    SpriteObject SpriteObject = AddInstance(GetIndex(@object.data.name));
                    SpriteObject.gameObject.SetActive(true);
                    SpriteObject.Generate(Pos);
                    return SpriteObject;
                }
        }

        return null;
    }

    SpriteObject AddInstance(int num)
    {
        SpriteObject newSpriteObject = Instantiate(SpriteObjectPrefabs[num], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), this.transform);
        SpriteObjects.Add(newSpriteObject);
        newSpriteObject.Deactivate();

        return newSpriteObject;
    }

    SpriteObject AddInstance(SpriteObject so)
    {
        SpriteObject newSpriteObject = Instantiate(so, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), this.transform);
        SpriteObjects.Add(newSpriteObject);
        newSpriteObject.Deactivate();

        return newSpriteObject;
    }


    SpriteObject AddInstanceAndList(SpriteObject so)
    {
        Array.Resize(ref SpriteObjectPrefabs, SpriteObjectPrefabs.Length + 1);
        SpriteObjectPrefabs[SpriteObjectPrefabs.Length - 1] = so;

        SpriteObject newSpriteObject = Instantiate(so, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), this.transform);
        SpriteObjects.Add(newSpriteObject);
        newSpriteObject.Deactivate();

        return newSpriteObject;
    }


    public override void ManagerUpdater(MainGame caller)
    {
        List<SpriteObject>list=new List<SpriteObject>(SpriteObjects);

        foreach (SpriteObject so in list)
        {
            if (so.isActive)
            {
                so.Updater();
            }
        }
    }

}
