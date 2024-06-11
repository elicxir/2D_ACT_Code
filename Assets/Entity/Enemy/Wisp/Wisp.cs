using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp : Enemy
{

    enum Action
    {
        Idle,
        Search,
        WalkRandom,
        Shot3,
    }

    public override void Init()
    {
        base.Init();
        action = Action.Idle;
        nextAction = Action.WalkRandom;
        count = 0;
        actioncount = 0;
    }


    Action action = Action.Idle;
    Action nextAction = Action.WalkRandom;

    [SerializeField]AnimationCurve curve1;

    protected int count = 0;
    int actioncount = 0;
    /*
    public override void Behavior(int timestep)
    {


        if(action == Action.Idle&& isActive&& detected)
        {
            switch (nextAction)
            {
                case Action.WalkRandom:
                    StartCoroutine("WalkRandom");
                    break;
                case Action.Shot3:
                    StartCoroutine("Shot3");

                    break;
            }
        }

    }*/
    /*
    IEnumerator Search()
    {
        action = Action.Search;
        
        for (;;)
        {
            VelocityVector = Vector2.zero;
            Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y),Vector2.one*520, 0);

            if (colliders.Length > 0)
            {
                for (int e = 0; e < colliders.Length; e++)
                {
                    if (colliders[e].tag == "Player")
                    {
                        yield return new WaitForSeconds(0.2f);

                        action = Action.Idle;
                        nextAction = Action.WalkRandom;
                        yield break;


                    }
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    */



    IEnumerator WalkRandom()
    {
        actioncount = Random.Range(400,500);

        Vector2 start = this.Position;
        Vector2 end = Define.PM.Player.Position +Random.insideUnitCircle.normalized * Random.Range(40, 80);



        action = Action.WalkRandom;

        for (float q = 0; q < (float)actioncount/240; q+=Time.deltaTime)
        {
            //this.Rigidbody2D.position = Vector2.Lerp(start,end,curve1.Evaluate(q*240/ (float)actioncount));
            yield return null;
        }

        action = Action.Idle;
        nextAction = Action.Shot3;
        yield break;
    }


    IEnumerator Shot3()
    {
        action = Action.Shot3;

        actioncount = 360;

        action = Action.Shot3;

        Vector2 target = (Define.PM.Player.Position);




        //projectileManager.Fire(0, this.Position, (target + Random.insideUnitCircle *30 - this.Position).normalized, Random.Range(170, 200),Hostility.Player);
        yield return new WaitForSecondsRealtime(0.3f);

        //projectileManager.Fire(0, this.Position, (target + Random.insideUnitCircle * 30 - this.Position).normalized ,Random.Range(170, 200), Hostility.Player);
        yield return new WaitForSecondsRealtime(0.3f);


        //projectileManager.Fire(0, this.Position, (target + Random.insideUnitCircle * 30 - this.Position).normalized, Random.Range(170, 200), Hostility.Player);
        yield return new WaitForSecondsRealtime(0.3f);

        action = Action.Idle;
        nextAction = Action.WalkRandom;
        yield break;
    }


    IEnumerator WalkLeft()
    {
        actioncount = 240;

        for (int q = 0; q < actioncount; q++)
        {
            yield return new WaitForFixedUpdate();
        }

        action = Action.Idle;

    }

    void WalkingLeft()
    {

    }

}
