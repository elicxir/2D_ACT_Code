using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_Lever : EventCommand
{
    [SerializeField] Lever lever;

    [SerializeField] float timer;

    [SerializeField] AnimationCurve buttonMode;
    [SerializeField] AnimationCurve toggleMode;




    public override IEnumerator Command()
    {
        yield return null;

    }


}
