using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Treasureは取ったらマップ上から消滅するオブジェクト
public class Treasure : Event
{
    [SerializeField] int ItemID;

    new string message = "Inspect";
    /*
    public override bool EventFlag()
    {
        return true;
    }

    public override void OnActivated()
    {

    }*/
}
