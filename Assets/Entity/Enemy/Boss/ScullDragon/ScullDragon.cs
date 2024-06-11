using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
public class ScullDragon : Enemy
{
    public Projectile projectile;
    public Projectile breath;

    public Transform[] BreathMarker;

    public override void Init()
    {
        base.Init(); mode = 0;

        GM.UI.BossHP.entity = this;
    }

    int counter = 0;
    public void LateUpdate()
    {
        counter++;
        if (counter % 1200 == 0)
        {
            // GM.Projectile.Fire(projectile, Position + Vector2.left * 80 + Vector2.down * 32, Vector2.left, 72, this);

        }

        foreach (var item in necks)
        {
            if (faceDirection == FaceDirection.Left)
            {
                item.flipX = true;
                item.flipY = true;
            }
            else
            {
                item.flipX = false;
                item.flipY = false;

            }

            item.color = sprite_renderer.color;
        }
        foreach (var item in spines)
        {
            if (faceDirection == FaceDirection.Left)
            {
                item.flipX = true;
                item.flipY = true;
            }
            else
            {
                item.flipX = false;
                item.flipY = false;

            }
            item.color = sprite_renderer.color;
        }

    }
    [SerializeField] GameObject spine_GameObject;

    BezierCurve spineCurve;
    [SerializeField] Transform[] spineMarker;
    [SerializeField] SpriteRenderer[] spines;

    BezierCurve curve;
    [SerializeField] Transform[] transforms;

    [SerializeField] GameObject Neck_GameObject;
    [SerializeField] SpriteRenderer[] necks;



    [SerializeField] Sprite[] neck_Sprite;
    [SerializeField] Sprite[] spines_Sprite;

    [ContextMenu("sprite")]
    void setSprite()
    {
        necks = Neck_GameObject.GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < necks.Length; i++)
        {
            necks[i].sprite = neck_Sprite[i % 2];
        }

        spines = spine_GameObject.GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < spines.Length; i++)
        {
            spines[i].sprite = spines_Sprite[i % 2];
        }
    }



    public Vector2 aaaaaa;

    int mode = 0;
    protected override IEnumerator StateMachine()
    {
        DoAction(3);
        yield return NowAction;
        while (isAlive)
        {
            if (mode == 0)
            {
                DoAction(4);
                yield return NowAction;
                DoAction(6);
                yield return NowAction;
                DoAction(5);
                yield return NowAction;
                DoAction(1);
                yield return NowAction;
                DoAction(5);
                yield return NowAction;
                DoAction(1);
                yield return NowAction;
                DoAction(0);
                yield return NowAction;


            }
            else if(mode ==1)
            {
                DoAction(3);
                yield return NowAction;
                DoAction(2);
                yield return NowAction;
                DoAction(5);
                yield return NowAction;
                DoAction(0);
                yield return NowAction;
            }
            else
            {
                DoAction(2);
                yield return NowAction;
                DoAction(1);
                yield return NowAction;
                DoAction(3);
                yield return NowAction;
                DoAction(6);
                yield return NowAction;
                DoAction(2);
                yield return NowAction;
                DoAction(1);
                yield return NowAction;
                DoAction(5);
                yield return NowAction;
                DoAction(0);
                yield return NowAction;
            }

            List<int> list = new List<int> {0,1,2,0,1,2};
            list.Remove(mode);

            mode = Random.Range(0, list.Count);
            
            

        }
    }

    protected override void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Position, aaaaaa);

        base.OnDrawGizmos();
        NeckValidate();
        curve.DrawGizmos();

        spineCurve = new BezierCurve(spineMarker, spines.Length + 2);
        for (int i = 0; i < spines.Length; i++)
        {
            spines[i].transform.position = spineCurve.GetPoint(i + 1);
            spines[i].transform.eulerAngles = new Vector3(0, 0, spineCurve.GetRotationPoint(i + 1));
        }
        spineCurve.DrawGizmos();
    }


    [SerializeField] Transform head;
    [SerializeField] Transform neck_root;


    const float neck_rot_min = 16;
    const float neck_rot_max = 80;

    void NeckValidate()
    {
        curve = new BezierCurve(transforms, necks.Length * 2 + 2);

        for (int i = 0; i < necks.Length; i++)
        {
            necks[i].transform.position = curve.GetPoint(i * 2 + 1);
            necks[i].transform.eulerAngles = new Vector3(0, 0, curve.GetRotationPoint(i * 2 + 1));
        }

        float angle;
        if (faceDirection == FaceDirection.Left)
        {
            angle = Mathf.Clamp(Vector2.SignedAngle(FrontVector, head.position - neck_root.position), -neck_rot_max, -neck_rot_min);

        }
        else
        {
            angle = Mathf.Clamp(Vector2.SignedAngle(FrontVector, head.position - neck_root.position), neck_rot_min, neck_rot_max);

        }

        neck_root.eulerAngles = Vector3.forward * angle;
        ;
    }


    protected override void EntityUpdater()
    {
        NeckValidate();
        base.EntityUpdater();
    }


    protected override IEnumerator DeathCoRoutine()
    {
        StopCoroutine(NowAction);
        NeckValidate();
        ACReset();

        //yield return FireGenerate();

        PartsGenerate();

        DisAppear();
        yield break;
    }

    IEnumerator FireGenerate()
    {
        foreach (var item in additional_renderer)
        {
            Vector2 Velocity;

            if (faceDirection== FaceDirection.Left)
            {
                Velocity = Vector2.right * Random.Range(32, 96) + Vector2.up * Random.Range(120, 320);
            }
            else
            {
                Velocity = Vector2.left * Random.Range(32, 96) + Vector2.up * Random.Range(120, 320);
            }
            SpriteObject SO = SOM.GenerateByData(sod, item.transform.position);
        }
        yield break;

    }
    [SerializeField]SpriteObjectData sod;
    [SerializeField] SpriteObjectData sod2;

    void PartsGenerate()
    {
        foreach (var item in additional_renderer)
        {
            if (item.name == "Jaw" || item.name == "head")
            {

                SpriteObject SO = SOM.GenerateByData(sod2, item.transform.position);
                SO.BaseVelocity = Velocity;
                SO.spriteRenderer.sprite = item.sprite;
                SO.transform.eulerAngles = item.transform.eulerAngles;
                SO.transform.localScale = item.transform.lossyScale;
            }
            else
            {
                Vector2 Velocity;

                if (faceDirection == FaceDirection.Left)
                {
                    Velocity = Vector2.right * Random.Range(4, 32) + Vector2.up * Random.Range(16, 80);
                }
                else
                {
                    Velocity = Vector2.left * Random.Range(4, 32) + Vector2.up * Random.Range(16, 80);
                }

                SpriteObject SO = SOM.GenerateByData(sod, item.transform.position);
                SO.BaseVelocity = Velocity;
                SO.spriteRenderer.sprite = item.sprite;
                SO.transform.eulerAngles = item.transform.eulerAngles;
                SO.transform.localScale = item.transform.lossyScale;
            }

        }

        /*
        bool toRight = Position.x - Player.Position.x > 0;

        bool isRight = faceDirection == FaceDirection.Right;

        for (int i = 0; i < transforms.Length; i++)
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

            //SpriteObject SO = SOM.GenerateByData(sod, transforms[i].position);
            //SO.index = i;

            //SO.BaseVelocity = Velocity;

        }
        */
    }


}



