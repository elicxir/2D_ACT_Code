using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class orbManager : MonoBehaviour
{
    [SerializeField] int InitNumber = 30;

    List<exp_orb> orbs = new List<exp_orb>();

    [SerializeField] exp_orb[] exp_orb_prefab;

    void Start()
    {
        Clear();
        InitGenerate();
    }

    void InitGenerate()
    {
        for (int i = 0; i < exp_orb_prefab.Length; i++)
        {
            AddInstance(i);
        }
        for (int i = 0; i < InitNumber; i++)
        {
            AddInstance(0);
        }
    }

    exp_orb AddInstance(int num)
    {
        exp_orb newOrb = Instantiate(exp_orb_prefab[num], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), this.transform);
        orbs.Add(newOrb);
        newOrb.Deactivate();

        return newOrb;
    }

    //すべて抹消する
    public void Clear()
    {
        //foreach (var orb in MPorbs.Select((value, index) => new { value, index }))
        foreach (var orb in orbs)
        {
            if (orb.isActive)
            {
                orb.Deactivate();
            }
        }
    }



    public void Generate(int value, Vector2 center, float minDistance = 6.0f)
    {
        print($"EXP{value}");

        int counter = 0;
        while (counter < value)
        {
            int val ;
            if (value - counter>20)
            {
                val = Random.Range(4, 6);
            }else if (value-counter > 10)
            {
                val =Random.Range(2,4);
            }
            else
            {
                val = 1;
            }


            Vector2 dir = Random.insideUnitCircle;
            Fire(0, center + dir * minDistance, dir, Random.Range(36.0f, 60.0f),val);
            counter+=val;
        }



    }

    public exp_orb Fire(int index, Vector2 Pos, Vector2 Direction, float speed,int val)
    {

        for (int n = 0; n < orbs.Count; n++)
        {
            if (!orbs[n].isActive)
            {
                if (orbs[n].PrefabIndex == index)
                {
                    orbs[n].gameObject.SetActive(true);
                    orbs[n].Activate(Pos, Direction.normalized, speed,val);
                    return orbs[n];
                }
            }
        }


        exp_orb orb = AddInstance(index);
        orb.gameObject.SetActive(true);
        orb.Activate(Pos, Direction.normalized, speed,val);
        return orb;
    }




    public void OnSectionChanged()
    {
        for (int n = 0; n < orbs.Count; n++)
        {
            if (orbs[n].isActive)
            {
                orbs[n].Deactivate();
            }
        }

    }

    int FixedTimer = 0;
    int timestep = 2;
    private void Update()
    {
        FixedTimer++;

        if (FixedTimer % timestep == 0)
        {
            for (int n = 0; n < orbs.Count; n++)
            {
                if (orbs[n].isActive)
                {
                    orbs[n].Updater();
                }
            }
        }

        for (int n = 0; n < orbs.Count; n++)
        {
            if (orbs[n].isActive)
            {
                orbs[n].FixedUpdaterEvery();
            }
        }

    }
}
