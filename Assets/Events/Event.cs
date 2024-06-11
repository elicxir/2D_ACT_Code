using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using GameConsts;
public class Event : OwnTimeMonoBehaviour
{
    public virtual string UniqueEventID
    {
        get
        {
            return transform.position.ToString();
        }
    }

    public bool isTouching
    {
        get
        {
            return GM.Event.isTouching(this);
        }
    }

    //接触開始時に実行される関数
    public　virtual void onTouch()
    {

    }
    //接触終了時に実行される関数
    public virtual void onDeTouch()
    {

    }

    public SpriteRenderer spriteRenderer
    {
        get
        {
            if (spriteRenderer_var == null)
            {
                spriteRenderer_var = GetComponentInChildren<SpriteRenderer>();
                return spriteRenderer_var;
            }
            else
            {
                return spriteRenderer_var;
            }
        }
    }
    SpriteRenderer spriteRenderer_var;

    [SerializeField] protected SpriteAnimation Animation;
    [SerializeField] EventSettings eventSettings;
    public virtual bool AutoeventFlag
    {
        get
        {
            return true;
        }
    }
    public bool Directional
    {
        get
        {
            return eventSettings.Directional;
        }
    }
    public bool ShowOnDeactivated
    {
        get
        {
            return eventSettings.ShowOnDeactivated;
        }
    }
    public bool AutoEvent
    {
        get
        {
            return eventSettings.AutoEvent;
        }
    }
    public bool BindEvent
    {
        get
        {
            return eventSettings.BindEvent;
        }
    }
    public string message
    {
        get
        {
            return eventSettings.message;
        }
    }
    public Color color
    {
        get
        {
            return eventSettings.color;
        }
    }

    public Rect Hitbox {
        get
        {
            return eventSettings.Hitbox;
            
        }
    }
    public bool EventFireCondition()
    {
        if (!Directional)
        {
            return true;
        }
        else
        {
            Vector2 pos = (player.Position - (Vector2)this.transform.position);

            if (pos.x >= 0)
            {
                if (player.faceDirection == Entity.FaceDirection.Left)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (player.faceDirection == Entity.FaceDirection.Right)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
    }

    protected Player player
    {
        get
        {
            return GM.Player.Player;
        }
    }




    [SerializeField] protected List<EventCommand> eventCommands;


    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.4f);
        Gizmos.DrawCube(Pos + Hitbox.x * Vector2.right + Hitbox.y * Vector2.up, Hitbox.size);
    }

    public virtual IEnumerator DoEvent()
    {
        int index = 0;

        print(this.name + " start");

        if (eventCommands.Count > 0)
        {
            do
            {
                if (eventCommands[index] != null)
                {
                    yield return StartCoroutine(eventCommands[index].Command());
                    index = eventCommands[index].NextCommandIndex(index, eventCommands);

                }
                else
                {
                    ValidateUpdate();


                    yield break;
                }
            }
            while (index != -1);
        }
        else
        {
            ValidateUpdate();

            yield break;
        }
        ValidateUpdate();

    }

    void ValidateUpdate()
    {
        if (!EventFlag() && !ShowOnDeactivated)
        {
            this.gameObject.SetActive(false);
        }
    }

    public virtual bool CheckTouchEvent(Vector2 vector2)
    {
        return EventFlag() && ((Mathf.Abs(vector2.y - Pos.y - Hitbox.y) <= 0.5 * Hitbox.height) && (Mathf.Abs(vector2.x - Pos.x - Hitbox.x) <= 0.5 * Hitbox.width));
    }

    public Vector2 Pos
    {
        get
        {
            return transform.position;
        }
    }

    public override void Init()
    {

    }




    //イベントがアクティブかどうか
    public bool active;

    //イベントが有効になる条件
    public virtual bool EventFlag() { return true; }

    public Vector2Int MapGrid
    {
        get
        {
            if (gameObject != null)
            {
                int mapx = (int)(transform.position.x) / Game.width + 1;
                int mapy = (int)(transform.position.y) / Game.height + 1;

                return new Vector2Int(mapx, mapy);
            }
            else
            {
                Debug.Log("Error");
                return new Vector2Int(0, 0);
            }
        }
    }


    float counter = 0;
    public virtual void Updater()
    {
      
        if (Animation != null)
        {
            if (EventFlag())
            {
                counter += OwnDeltaTime;
                spriteRenderer.sprite = Animation.UpdateSprite(counter);

            }
            else
            {
            }

        }

    }


}
