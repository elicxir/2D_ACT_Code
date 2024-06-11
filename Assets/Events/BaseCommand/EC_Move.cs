using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_Move : EventCommand
{
    public Entity Object;
    public float Speed=80;
    public Transform destination;

    public override IEnumerator Command()
    {

        yield return null;

        
    }

}
