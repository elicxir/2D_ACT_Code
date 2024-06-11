using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exp_orb : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;

    public Vector2 Velocity;

    [SerializeField] float gravity;

    [SerializeField] LayerMask layerMask;

    bool isMoving;

    public int PrefabIndex=-1;

    public int exp;

    public bool isActive
    {
        get
        {
            return gameObject.activeSelf;
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

    public void Activate( Vector2 Pos, Vector2 Direction, float Speed,int expval)
    {
        exp = expval;
        isMoving = true;
        Position = Pos;
        Velocity = Direction * Speed;
    }

    public void FixedUpdaterEvery()
    {

    }

    void Recochet(int timestep)
    {
        RaycastHit2D down= Physics2D.Raycast(Position, Vector2.down, Velocity.y * Time.deltaTime * timestep,layerMask);
        RaycastHit2D right= Physics2D.Raycast(Position, Vector2.right, Velocity.x * Time.deltaTime * timestep,layerMask);
        RaycastHit2D left= Physics2D.Raycast(Position, Vector2.left, Velocity.x * Time.deltaTime * timestep,layerMask);
        
        if (down&&Velocity.y<0)
        {
            Velocity = new Vector2(Velocity.x*0.8f, Velocity.y *(-0.72f));
            if (Velocity.y < 9) 
            {
                isMoving = false;
            }
        }
        if ((right && Velocity.y > 0)||(left&&Velocity.x<0))
        {
            Velocity = new Vector2(Velocity.x * 0.8f, Velocity.y);
        }

    }

    void Absorbed()
    {
        if ((Define.PM.Player.Position - Position).magnitude < 48)
        {
            Define.PM.AddEXP(exp);
            Deactivate();
        }

    }

    
    public void Updater()
    {
        float timestep = Time.deltaTime;

        //Recochet(timestep);
        if (isMoving)
        {
            Velocity = Velocity + Vector2.down * gravity * Time.deltaTime * timestep;
            Position = Position + Velocity * Time.deltaTime * timestep;
        }
        Absorbed();
    }

    public void Deactivate()
    {
        Position = new Vector2(0, 0);
        gameObject.SetActive(false);
        isMoving = false;
    }

}
