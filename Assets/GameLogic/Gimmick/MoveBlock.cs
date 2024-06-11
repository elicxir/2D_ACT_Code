using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class MoveBlock : Gimmick
{
    public override int UpdateRange
    {
        get
        {
            return Mathf.CeilToInt((new Vector2(TerrainHitBox.size.x*0.5f, TerrainHitBox.size.y*0.5f)).magnitude);
        }
    }

    [SerializeField] bool forUpdate;

    [SerializeField] protected BoxCollider2D TerrainHitBox;

    [SerializeField] protected GameObject Block;

    [SerializeField] SpriteRenderer spriteRenderer;


    [SerializeField] string FlagName;

    [SerializeField] Sprite notActivated;

    [SerializeField] Sprite Activated;


    public override bool Condition()
    {
        GameFlag GF = GM.Game.PlayData.GetGameFlag(FlagName);
        return GF.isTrue;
    }


    bool isThisTransparent
    {
        get
        {
            return (gameObject.CompareTag("Transparented"));
        }
    }

    // Vector2Int AdjustedPosition;

    public override void Init()
    {
        timer = 0;
        AdjustedPosition = Calculation(timer);
        transform.position = new Vector3(AdjustedPosition.x, AdjustedPosition.y);
    }


    public float timer = 0;

    public override bool Updater()
    {
        Vector3 buffer = transform.position;

        Move(Calculation(timer) - AdjustedPosition);

        if (Condition())
        {
            spriteRenderer.sprite = Activated;
            timer += OwnDeltaTime;
        }
        else
        {
            spriteRenderer.sprite = notActivated;
        }

        return !(buffer == transform.position);

    }

    protected virtual Vector2Int Calculation(float time)
    {
        return Vector2Int.zero;
    }

    int Width
    {

        get
        {
            return (int)TerrainHitBox.size.x / 2;
        }
    }
    int Height
    {

        get
        {
            return (int)TerrainHitBox.size.y / 2;
        }
    }

    [SerializeField]
    bool[,] TerrainData;//true:•Ç false:‹ó”’(ˆÚ“®‰Â”\)

    enum Direction
    {
        Up, Down, Right, Left, Surface
    }

    List<Entity> DetectEntity(Direction dir)
    {
        Vector2 start;
        Vector2 end;

        switch (dir)
        {
            case Direction.Up:
                start = LeftUp + Vector2.up;
                end = RightUp + Vector2.up;
                break;

            case Direction.Down:
                start = LeftDown + Vector2.down;
                end = RightDown + Vector2.down;
                break;

            case Direction.Right:
                start = RightDown + Vector2.right;
                end = RightUp + Vector2.right;
                break;

            case Direction.Left:
                start = LeftUp + Vector2.left;
                end = LeftDown + Vector2.left;
                break;

            case Direction.Surface:
                start = LeftUp;
                end = RightUp;
                break;

            default:
                start = LeftUp + Vector2.left;
                end = LeftDown + Vector2.left;
                break;

        }

        RaycastHit2D[] collider = Physics2D.LinecastAll(start, end, PlayerLayer);
        RaycastHit2D[] collider2 = Physics2D.LinecastAll(start, end, EnemyLayer);

        List<Entity> list = new List<Entity>();
        foreach (RaycastHit2D hit2D in collider)
        {
            Entity q = hit2D.collider.GetComponent<Entity>();
            if (q != null)
            {
                list.Add(q);
            }
        }
        foreach (RaycastHit2D hit2D in collider2)
        {
            Entity q = hit2D.collider.GetComponent<Entity>();
            if (q != null)
            {
                list.Add(q);
            }
        }
        return list;
    }

    Vector2 LeftDown
    {
        get
        {
            return AdjustedPosition + TerrainHitBox.offset + Vector2.left * (Width - 0.5f) + Vector2.down * (Height - 0.5f);
        }
    }
    Vector2 LeftUp
    {
        get
        {
            return AdjustedPosition + TerrainHitBox.offset + Vector2.left * (Width - 0.5f) + Vector2.up * (Height - 0.5f);
        }
    }
    Vector2 RightUp
    {
        get
        {
            return AdjustedPosition + TerrainHitBox.offset + Vector2.right * (Width - 0.5f) + Vector2.up * (Height - 0.5f);
        }
    }
    Vector2 RightDown
    {
        get
        {
            return AdjustedPosition + TerrainHitBox.offset + Vector2.right * (Width - 0.5f) + Vector2.down * (Height - 0.5f);
        }
    }

    void Move(Vector2Int val)
    {
        if (val.x > 0)
        {
            for (int i = 0; i < val.x; i++)
            {
                AdjustedPosition += Vector2Int.right;
                foreach (Entity entity in DetectEntity(Direction.Right))
                {
                    entity.ForcedMove(Vector2Int.right);
                }
                foreach (Entity entity in DetectEntity(Direction.Up))
                {
                    entity.EffectedMove(Vector2Int.right);
                }
            }
        }
        if (val.x < 0)
        {
            for (int i = 0; i < Mathf.Abs(val.x); i++)
            {
                AdjustedPosition += Vector2Int.left;
                foreach (Entity entity in DetectEntity(Direction.Left))
                {
                    entity.ForcedMove(Vector2Int.left);
                }
                foreach (Entity entity in DetectEntity(Direction.Up))
                {
                    entity.EffectedMove(Vector2Int.left);
                }
            }
        }
        if (val.y < 0)
        {
            for (int i = 0; i < Mathf.Abs(val.y); i++)
            {
                List<Entity> upper = DetectEntity(Direction.Up);
                List<Entity> downer = DetectEntity(Direction.Up);

                AdjustedPosition += Vector2Int.down;

                if (upper.Count > 0 || downer.Count > 0)
                {
                    transform.position = new Vector3Int(AdjustedPosition.x, AdjustedPosition.y, 0);
                }

                if (!isThisTransparent)
                {
                    foreach (Entity entity in upper)
                    {
                        entity.ForcedMove(Vector2Int.down);
                    }

                    foreach (Entity entity in downer)
                    {
                        if (!upper.Contains(entity))
                        {
                            entity.ForcedMove(Vector2Int.down);
                        }
                    }
                }
                else
                {
                    foreach (Entity entity in upper)
                    {
                        if (entity.isOnTransparent)
                        {
                            entity.ForcedMove(Vector2Int.down);
                        }
                    }
                }
            }
        }
        if (val.y > 0)
        {
            for (int i = 0; i < val.y; i++)
            {
                List<Entity> e1 = DetectEntity(Direction.Surface);
                List<Entity> e2 = DetectEntity(Direction.Up);

                AdjustedPosition += Vector2Int.up;

                if (e1.Count > 0 || e2.Count > 0)
                {
                    transform.position = new Vector3Int(AdjustedPosition.x, AdjustedPosition.y, 0);
                }

                foreach (Entity entity in e2)
                {
                    if (!e1.Contains(entity))
                    {
                        entity.EffectedMove(Vector2Int.up);
                    }
                }
            }
        }

        if (transform.position != new Vector3Int(AdjustedPosition.x, AdjustedPosition.y, 0))
        {
            transform.position = new Vector3Int(AdjustedPosition.x, AdjustedPosition.y, 0);
        }
    }

}
