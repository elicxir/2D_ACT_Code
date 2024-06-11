using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skelton_Fixed : Skelton
{

    protected override IEnumerator StateMachine()
    {
        while (true)
        {
           


            yield return null;

        }


        /*
        while (true)
        {

            print("1");
            DoAction("walk");
            yield return NowAction;

            print("2");
            DoAction("throw");
            yield return NowAction;

            print("3");
            DoAction("step");
            yield return NowAction;

            DoAction("throw");
            yield return NowAction;
        }*/
    }
}
