using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataTypes.Element;

public class DamageTerrain : Gimmick
{
    [SerializeField] AttackColider[] AC;
    [SerializeField] AC_DataSet[] dataset;

    [SerializeField] Hostility hostility;

    [SerializeField] int AC_FixedID;

    [SerializeField] Vector2 size;

    public override void Init()
    {
        Element atk = new Element();
        atk.SetValues(new int[9] { 100, 100, 100, 100, 100, 100, 100, 100, 100 });

        for (int y = 0; y < AC.Length; y++)
        {
            AC[y].SetAttackColider(true, AC_FixedID, atk, true, dataset[y], hostility);

        }

    }
}
