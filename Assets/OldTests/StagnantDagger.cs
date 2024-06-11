using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagnantDagger : Projectile
{
    public enum State
    {
        Invisible,
        Appeaar,
        Go,
    }

    public State state = State.Invisible;

    public void GO(float speed)
    {
        state = State.Go;
        //Velocity = Define.PM.Player.FrontVector * speed;
    }

    public void ST()
    {
        state = State.Invisible;
    }

}
