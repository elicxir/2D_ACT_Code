using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mp_orb : MonoBehaviour
{

    [SerializeField] SpriteRenderer sprite;
    public GameObject GameObject;

    public bool isAlive
    {
        get
        {
            return GameObject.activeSelf;
        }
    }

    public Vector2 Position
    {
        set
        {
            transform.position = new Vector3(value.x, value.y, 0);
        }
        get
        {
            return new Vector2(transform.position.x, transform.position.y);
        }
    }

    float timer=0;

    [SerializeField] float totaltime=5.0f;

    [SerializeField] AnimationCurve Curve1;
    [SerializeField] AnimationCurve Curve2;

    float collectspeed = 21;


    //プレイヤーへ向かうベクトル
    Vector2 forward
    {
        get
        {
            return (Define.PM.Player.Position - Position).normalized;
        }
    }

    //発散の速度
    Vector2 DefusionVector;

    //収束の速度
    Vector2 GatherVector;

    float ratio1
    {
        get
        {
            if (timer > totaltime)
            {
                return 0;

            }
            else
            {
                return Curve1.Evaluate(timer / totaltime);

            }
        }
    }

    float ratio2
    {
        get
        {
            if (timer > totaltime)
            {
                return 1;

            }
            else
            {
                return Curve2.Evaluate(timer / totaltime);

            }
        }
    }

    //全体の速度
    Vector2 Velocity
    {
        get
        {
            return DefusionVector * ratio1 + forward * ratio2*collectspeed;
        }
    }



    float val = 0;
    public void Activate(Sprite image,float mana,Vector2 Pos,Vector2 Direction,float Speed)
    {
        timer = 0;
        sprite.sprite = image;
        val = mana;
        Position = Pos;
        DefusionVector = Direction.normalized*Speed;
    }

    public void Deactivate()
    {
        Position = new Vector2(0, 0);
        GameObject.SetActive(false);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;


        transform.position += new Vector3(Velocity.x, Velocity.y,0);

        /*
        timer -= Time.fixedDeltaTime;

        Vector2 Velocity1;
        Vector2 Velocity2;

        Velocity = Velocity * 0.93f;


        player_pos = Define.PM.Player.Position;
        forward = player_pos - new Vector2(transform.position.x, transform.position.y);

        if (forward.magnitude < 80&&timer<11.6f)
        {
            flag = true;
        }


        if (flag)
        {
            if (vm < 1.0f)
            {
                vm += 0.03f;
            }
            Velocity1 = forward.normalized*vm;
        }
        else
        {
            Velocity1 = Vector2.zero;
        }

        Velocity2 = Velocity+ Velocity1;

        transform.position += new Vector3(Velocity2.x, Velocity2.y, 0);

        if (timer < 0)
        {
        }
        */


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.tag == "Player")
            {
                Define.PM.Player.ChangeVital(new float[5] { 0, val, 0, 0, 0 });
                Deactivate();
            }     
    }


}
