using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class EC_Branch_Flag : Branch
{
    public string Flag;

    protected override bool Condition()
    {
        return GM.Game.PlayData.GetGameFlag(Flag).isTrue;
    }
}
