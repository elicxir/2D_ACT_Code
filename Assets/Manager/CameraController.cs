using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConsts;
using Managers;

public class CameraController : OwnTimeMonoBehaviour
{
    [SerializeField] Player target;
    [SerializeField] AnimationCurve curve;

    public Camera MainCamera;

    Vector2 offset = new Vector2(0, 36);

    public Vector2Int LeftDown = new Vector2Int(0, 0);
    public Vector2Int RightUp = new Vector2Int(50 *Game.width , 80 * Game.height);


    public bool isFollowing=false;


    public void ResetCamera(Vector2Int Pos, Vector2Int ld, Vector2Int ru)
    {
        //transform.position = Vector3.back * 10 + Vector3.up * Pos.y + Vector3.right * Pos.x+ Vector3.right*offset.x * Define.GM.GAME_SIZE()+Vector3.up* offset.y* Define.GM.GAME_SIZE();

        LeftDown = ld;
        RightUp = ru;

    }




    float timer = 0;

    public void AddShake(int power,float Duration)
    {
        Shake data = new Shake();

    }

    class Shake
    {
        public int power;
        public float Duration;
    }

    List<Shake> shakes = new List<Shake>(); 

    public void SetCamera(Vector2 Pos)
    {

        transform.position = new Vector3(Mathf.Clamp(Pos.x, LeftDown.x, RightUp.x), Mathf.Clamp(Pos.y, LeftDown.y, RightUp.y), -10);
    }

    public void SetExtent(AreaExtent data)
    {
        LeftDown = data.LeftDown;
        RightUp = data.RightUp;
    }

    float T = 0.55f;

    /*
    Vector2 vector;
    Vector2 S;
    Vector2 E;
    private void Update()
    {


        if (vector == Define.PM.Player.FrontVector)
        {
            timer = Mathf.Min(timer + Time.deltaTime, T);

        }
        else
        {
            timer = 0;
            S = Front;
            E = Define.PM.Player.FrontVector;
        }
        vector = Define.PM.Player.FrontVector;

    }



    Vector2 Front
    {
        get
        {
            return  (E* curve.Evaluate(timer / T)+S * (1-curve.Evaluate(timer / T)))/(E+S);
        }
    }*/


    // Update is called once per frame
    void LateUpdate()
    {
        if (isFollowing)
        {

            //transform.position = Vector3.back * 10 + new Vector3(target.Position.x, target.Position.y, 0) + Vector3.right * offset.x * GM.Game.ScreenMult + Vector3.up * offset.y * GM.Game.ScreenMult;

            /*
            if (transform.position.x < LeftDown.x)
            {
                transform.position = new Vector3(LeftDown.x, transform.position.y, -10);
            }
            if (transform.position.x > RightUp.x)
            {
                transform.position = new Vector3(RightUp.x, transform.position.y, -10);
            }
            if (transform.position.y > RightUp.y)
            {
                transform.position = new Vector3(transform.position.x, RightUp.y, -10);
            }
            if (transform.position.y < LeftDown.y)
            {
                transform.position = new Vector3(transform.position.x, LeftDown.y, -10);
            }
            */
        }

            
        
        

    }


}
