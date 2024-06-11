using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Return_Dagger : Projectile
{

    int statetimer=0;

    public int gotimer = 225;
    public int stoptimer = 240;

    [SerializeField] AnimationCurve curve;

    enum State
    {
        Go,
        Stop,
        Back,
    }

    State state = State.Go;

    float gospeed;

    protected override void OnFireInit()
    {
        Direction = Velocity.normalized;
        gospeed = Velocity.magnitude;

        state = State.Go;
        statetimer=0;
    }

    void OnStop()
    {
        //AttackColider.Init();
        //AttackColider.AC_Type = AC_Type.HighTick;
        //AttackColider.AC_Damage.poke =8;


        statetimer = 0;
        state = State.Stop;

        InertiaRotateSpeed = 0;
        BaseVelocity = Vector2.zero;
    }
    void OnBack()
    {
        //AttackColider.Init();
        //AttackColider.AC_Type = AC_Type.Once;
       // AttackColider.AC_Damage.poke =50;


        state = State.Back;
        statetimer = 0;

    }

    Vector2 Direction;

    Vector2 PreDirection;
    /*
    public override void FixedUpdater(int timestep)
    {
        base.FixedUpdater(timestep);

        statetimer += timestep;


        switch (state)
        {
            case State.Go:
                if (statetimer>gotimer)
                {
                    OnStop();
                }
                else
                {
                    Velocity = Direction * gospeed * (1-curve.Evaluate(statetimer / gotimer));

                }


                break;

            case State.Stop:
                if (statetimer > stoptimer)
                {
                    OnBack();
                }
                else
                {
                    InertiaRotateSpeed += 32 * timestep;
                }
                Velocity = Vector2.zero;
                break;

            case State.Back:




                Vector2 direction = (Define.PM.Player.Position - Position).normalized;
                float distance = (Define.PM.Player.Position - Position).magnitude;

                float speed;

                if (statetimer > 240)
                {
                    speed = 1080;
                }
                else if(statetimer > 60)
                {
                    speed = (float)((statetimer - 60))* ((statetimer - 60))/3;
                }
                else
                {
                    
                    speed = 1;
                }



                if (statetimer >20 && Vector2.Angle(PreDirection, direction) > 160)
                {
                    Deactivate();
                }
                Velocity = direction * speed;
                if (statetimer > 20 && distance < 10)
                {
                    Deactivate();

                }

                PreDirection = Velocity.normalized;

                break;
        }


    }

    */

}
