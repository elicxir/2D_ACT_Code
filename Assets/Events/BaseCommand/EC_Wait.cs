using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_Wait : EventCommand
{
    [SerializeField] float WaitTime=0;
    public override IEnumerator Command()
    {
        yield return new WaitForSeconds(WaitTime);
    }
}
