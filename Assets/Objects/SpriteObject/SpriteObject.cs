using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteObject : OwnTimeMonoBehaviour
{
    public SpriteObjectData data;

    public SpriteRenderer spriteRenderer;

    public int index = 0;//
    public Transform target;

    private void OnValidate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Generate(Vector2 Pos,bool isFlip=false)
    {
        Init();
        if(data.layerMode== SpriteObjectData.LayerMode.FrontPlayer)
        {
            spriteRenderer.sortingLayerName = "BG(front)";
            print("front");
        }
        else
        {
            spriteRenderer.sortingLayerName = "Event(back)";
            print("back");
        }

        spriteRenderer.flipX = isFlip;
        BaseVelocity = Vector2.zero;
        BaseAcceleration = data.Accelerate;
        isActivated = true;
        Position = Pos;
    }

    public void Deactivate()
    {
        BaseVelocity = Vector2.zero;
        transform.position = Vector3.zero;
        gameObject.SetActive(false);
    }



    /// <summary>
    /// èIóπèàóùÇ™äJénÇ≥ÇÍÇƒÇ¢ÇÈÇ∆false
    /// </summary>
    public bool isActivated = true;
    void StartEndCoroutine()
    {
        if (isActivated)
        {
            isActivated = false;
            StartCoroutine(data.EndCoroutine(this));
        }
    }

    public void ChangeSprite(Sprite sprite)
    {
        if (spriteRenderer.sprite!=sprite)
        {
            spriteRenderer.sprite = sprite;
        }
    }



    protected virtual void SO_Function()
    {

    }

    public override void Updater(bool UpdateFlag = false)
    {
        ChangeSprite(data.UpdateSprite(OwnTimeSiceStart, this));
        data.SpecificMove(this);

        SO_Function();
        base.Updater(UpdateFlag);

        {
            switch (data.terrainMode)
            {
                case SpriteObjectData.Terrain.Through:
                    break;
                case SpriteObjectData.Terrain.NotThrough:
                    if(HitTerrain())
                    {
                        StartEndCoroutine();
                    }

                    break;
            }
        }

        if (data.DeactivateFunction(this))
        {
            StartEndCoroutine();
        }
    }

    protected bool HitTerrain()
    {
        Collider2D collider = Physics2D.OverlapBox(Position, Vector2.one*2, 0, Terrain);
        return collider;
    }

}
