using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using DataTypes.Functions;

//左右に移動→骨を投げる→左右にジャンプの繰り返し
public class Skelton : Enemy
{
    public enum State
    {
        Walk, Turn, Die, Step, Throw
    }

    public State ActionState
    {
        get
        {
            return (State)ActionIndex;
        }
    }
    public State ActionStateBefore
    {
        get
        {
            return (State)BeforeActionIndex;
        }
    }




    public override void Init()
    {
        base.Init();
        ThrowCount = 0;
    }



    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Position, DetectDistance);
    }


    const int DetectDistance = 168;
    const int CloseDistance = 64;


    int[] Power = new int[4] { 130, 170, 150, 190 };


    protected void Throw()
    {
        EA_Skelton_Throw throw_ = Actions[(int)State.Throw] as EA_Skelton_Throw;

        throw_.power = Power[ThrowCount % Power.Length];
        DoAction((int)State.Throw);
    }

    enum Condition
    {
        Normal, Wall, WallBack, Hole, HoleBack
    }
    Condition condition = Condition.Normal;


    int ThrowCount = 0;//骨を投げた回数


    protected override IEnumerator StateMachine()
    {
        while (isAlive)
        {

            switch (condition)
            {
                case Condition.Normal:
                    {
                        
                        if (inRange(0, DetectDistance))
                        {
                            for (int i = 0; i < Rand.Range(2, 3); i++)
                            {
                                Throw();
                                yield return NowAction;

                                {
                                    if (inRange(0, DetectDistance))
                                    {
                                        if (!FacePlayer)
                                        {
                                            DoAction((int)State.Turn);
                                            yield return NowAction;
                                        }
                                    }
                                    else
                                    {
                                        if (FaceOuter && !inExtent_X)
                                        {
                                            DoAction((int)State.Turn);
                                            yield return NowAction;
                                        }
                                    }
                                }


                            }

                            for (int i = 0; i < Rand.Range(1, 2);i++)
                            {
                                DoAction((int)State.Walk);
                                yield return NowAction;
                                if (inRange(0, CloseDistance))
                                {
                                    if (MPConsume(30))
                                    {
                                        DoAction((int)State.Step);
                                        yield return NowAction;
                                    }
                                }
                            }


                        }
                        else
                        {
                            for (int i = 0; i < Rand.Range(1, 3); i++)
                            {
                                DoAction((int)State.Walk);
                                yield return NowAction;

                            }
                        }



                        if (inRange(0, CloseDistance))
                        {
                            if (MPConsume(30))
                            {
                                DoAction((int)State.Step);
                                yield return NowAction;
                            }
                        }
                        if (inRange(CloseDistance, 2000))
                        {
                            DoAction((int)State.Walk);
                            yield return NowAction;
                        }


                    }
                    
                    break;


                case Condition.Wall:
                case Condition.Hole:
                    if(inRange(0, DetectDistance)){
                        Throw();
                        yield return NowAction;
                        Throw();
                        yield return NowAction;
                    }
                    DoAction((int)State.Turn);
                    yield return NowAction;
                    DoAction((int)State.Walk);
                    yield return NowAction;

                    condition = Condition.Normal;

                    break;

                case Condition.HoleBack:
                case Condition.WallBack:
                    if (inRange(0, DetectDistance))
                    {
                        Throw();
                        yield return NowAction;
                        Throw();
                        yield return NowAction;
                    }
                    DoAction((int)State.Walk);
                    yield return NowAction;

                    condition = Condition.Normal;

                    break;

            }



            {
                if (inRange(0, DetectDistance))
                {
                    if (!FacePlayer)
                    {
                        DoAction((int)State.Turn);
                        yield return NowAction;
                    }
                }
                else
                {
                    if (FaceOuter && !inExtent_X)
                    {
                        DoAction((int)State.Turn);
                        yield return NowAction;
                    }
                }
            }

            /*

            {
                if (inRange(0, 50))
                {
                    DoAction((int)State.Step);
                    yield return NowAction;

                }

            }
            */


        }
    }

    protected override void OnFaceWall(FaceDirection direction)
    {
        if (condition != Condition.Wall)
        {
            condition = Condition.Wall;

            StopAction();
        }
    }

    protected override void OnFaceHole(FaceDirection direction)
    {
        if (condition != Condition.Hole && condition != Condition.HoleBack)
        {
            if (direction == faceDirection)
            {
                condition = Condition.Hole;
            }
            else
            {
                condition = Condition.HoleBack;
            }

            StopAction();
        }
    }

    protected override IEnumerator DeathCoRoutine()
    {
        StopCoroutine(NowAction);
        ACReset();

        PartsGenerate();

        DisAppear();
        yield break;
    }

    void PartsGenerate()
    {
        bool toRight = Position.x - Player.Position.x > 0;

        bool isRight = faceDirection == FaceDirection.Right;

       for(int i = 0; i < transforms.Length; i++)
        {
            Vector2 Velocity;

            if (toRight)
            {
                Velocity = Vector2.right * Random.Range(32, 96) + Vector2.up * Random.Range(120, 320);
            }
            else
            {
                Velocity = Vector2.left * Random.Range(32, 96) + Vector2.up * Random.Range(120, 320);
            }

            SpriteObject SO= SOM.GenerateByData(sod, transforms[i].position);
            SO.index = i;

            SO.BaseVelocity = Velocity;

        }

    }

    [SerializeField] SpriteObjectData sod;
    [SerializeField] Transform[] transforms;
}


/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using DataTypes.Element;

//左右に移動→骨を投げる→左右にジャンプの繰り返し
public class Skelton : Enemy
{
    public enum State
    {
        Walk, Turn, Die, Step, Throw
    }
    public State ActionState
    {
        get
        {
            return (State)ActionIndex;
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Position, DetectDistance);
    }

    const int DetectDistance = 140;
    const int CloseDistance = 48;
    const int WalkDistance = 24;

    protected bool Detect()
    {
        return (PlayerPos - Position).sqrMagnitude < DetectDistance * DetectDistance;
    }

    protected bool TooClose()
    {
        return (PlayerPos - Position).sqrMagnitude < CloseDistance * CloseDistance;
    }
    protected bool canWalk()
    {
        return (PlayerPos - Position).sqrMagnitude > WalkDistance * WalkDistance;
    }


    protected void Throw(int power)
    {
        EA_Skelton_Throw throw_ = Actions[(int)State.Throw] as EA_Skelton_Throw;
        throw_.power = power;
        DoAction((int)State.Throw);
    }

    bool extentFlag = false;

    bool walkfrontFlag = false;
    bool stepFlag = false;




    enum Condition
    {

    }



    protected override IEnumerator StateMachine()
    {
        while (true)
        {

















            if (Position.x >= C_Rect.Right(BehaviorRect))
            {
                Position += Vector2.left;

                if (Detect())
                {
                    Throw(130);
                    yield return NowAction;
                    Throw(170);
                    yield return NowAction;

                    if (faceDirection == FaceDirection.Right)
                    {
                        DoAction((int)State.Step);
                    }
                    else
                    {
                        DoAction((int)State.Walk);
                    }
                    yield return NowAction;

                }
                else
                {
                    if (faceDirection == FaceDirection.Right)
                    {
                        DoAction((int)State.Turn);
                    }
                    else
                    {
                        DoAction((int)State.Walk);
                    }
                    yield return NowAction;

                }

            }
            else if (Position.x <= C_Rect.Left(BehaviorRect))
            {
                Position += Vector2.right;


                if (Detect())
                {

                    Throw(130);
                    yield return NowAction;
                    Throw(170);
                    yield return NowAction;

                    if (faceDirection == FaceDirection.Left)
                    {
                        DoAction((int)State.Step);
                    }
                    else
                    {
                        DoAction((int)State.Walk);
                    }
                    yield return NowAction;

                }
                else
                {
                    if (faceDirection == FaceDirection.Left)
                    {
                        DoAction((int)State.Turn);
                    }
                    else
                    {
                        DoAction((int)State.Walk);
                    }
                    yield return NowAction;
                }

            }
            else
            {
                if (Detect())
                {
                    while (true)
                    {
                        if (ActionState== State.Step|| ActionState == State.Walk)
                        {
                            Throw(130);
                            yield return NowAction;
                            Throw(170);
                            yield return NowAction;

                        }
                        else
                        {
                            if (canWalk())
                            {
                                DoAction((int)State.Walk);
                                yield return NowAction;
                            }
                            else
                            {
                                if (MPConsume(40))
                                {
                                    DoAction((int)State.Step);
                                    yield return NowAction;

                                }
                                else
                                {
                                    Throw(160);
                                    yield return NowAction;
                                }
                            }

                        }
                    }
                }
                else
                {
                    DoAction((int)State.Walk);
                    yield return NowAction;
                }
            }


        }
    }

    void AreaExtent()
    {
        if (Position.x >= C_Rect.Right(BehaviorRect))
        {
            StopAction();

        }
        else if (Position.x <= C_Rect.Left(BehaviorRect))
        {

            StopAction();
        }
    }

    protected override void OnFaceHole()
    {
        base.OnFaceHole();
    }




    void F()
    {
        if (Detect())
        {
            if (!FacePlayer()&&(ActionState== State.Walk|| ActionState == State.Step))
            {
                OverrideAction((int)State.Turn);
            }
            else if (TooClose()&& ActionState != State.Step)
            {
                if (MPConsume(80))
                {
                    OverrideAction((int)State.Step);

                }
            }
        }


        else if (ActionState != State.Turn)
        {
            Collider2D collider = Physics2D.OverlapBox(Position + FrontVector * TerrainBoxWidth, new Vector2(TerrainBoxWidth * 2 - 2, TerrainBoxHeight * 2 - 6), 0, Terrain);
            if (collider)
            {
                OverrideAction((int)State.Turn);

            }
        }


    }



    protected override void EntityUpdater()
    {
        base.EntityUpdater();

        AreaExtent();

        F();
    }






    protected override IEnumerator DeathCoRoutine()
    {
        StopCoroutine(NowAction);
        ACReset();

        PartsGenerate();

        DisAppear();
        yield break;
    }

    void PartsGenerate()
    {
        bool toRight = Position.x - Player.Position.x > 0;

        bool isRight = faceDirection == FaceDirection.Right;

        foreach (SpriteRenderer data in datas)
        {
            Vector2 Velocity;

            if (toRight)
            {
                Velocity = Vector2.right * Random.Range(32, 96) + Vector2.up * Random.Range(120, 320);
            }
            else
            {
                Velocity = Vector2.left * Random.Range(32, 96) + Vector2.up * Random.Range(120, 320);

            }

            SO_Parts so = (SO_Parts)SOM.Generate(prefab, data.transform.position);

            so.UniqueInit(data.sprite, data.transform.position, Velocity, isRight);
            so.BaseAcceleration = Vector2.down * 560;
        }
    }

    [SerializeField] SpriteRenderer[] datas;
    [SerializeField] SO_Parts prefab;
}



 
 */