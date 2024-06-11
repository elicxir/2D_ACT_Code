using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class EA_Player_sliding : PlayerAction
{
    [SerializeField] SpriteObjectData sod;
    [SerializeField] Transform FootMarker;

    Vector2 pos
    {
        get
        {
            return (Vector2)FootMarker.position + player.FrontVector * -7 + Vector2.up * 3;
        }
    }


    [SerializeField] AnimationCurve curve_start;
    [SerializeField] AnimationCurve curve_end;

    [SerializeField] float speed;
    
    int Dis
    {
        get
        {
            return Mathf.CeilToInt(speed * AnimationClips[2].length * 0.5f);
        }
    }

    bool cond
    {
        get
        {
            bool cond1 = player.canTerrainBoxTypeChange(Player.TerrainBoxType.Crouch, Dis);
            bool cond2 = !player.Face_Wall();

            if ((!cond2 || cond1))
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }






    const float T = 0.04f;
    public override IEnumerator Act()
    {
        string Start= GetActionName(0);
        string Slide= GetActionName(1);
        string End= GetActionName(2);

        float generateTimer = T;

        void GT()
        {
            if (generateTimer > 0)
            {
                generateTimer -= player.OwnDeltaTime;

                if (generateTimer <= 0)
                {
                    generateTimer = Random.Range( T,2*T);
                    GM.OBJ.SpriteObjectManager.GenerateByData(sod, pos, player.faceDirection == Entity.FaceDirection.Left);
                }
            }
        }





        entity.BaseVelocity.x = 0;
        entity.BaseAcceleration.x = 0;

        {
            Play(Start);
            while (GetState.normalizedTime < 1)
            {

                GT();
                entity.BaseVelocity.x = player.FrontVector.x * speed * curve_start.Evaluate(GetState.normalizedTime);
                yield return null;
            }
            Stop();
        }
     
        {
            do
            {
                Play(Slide);
                while (GetState.normalizedTime < 1)
                {
                    GT();

                    entity.BaseVelocity.x = player.FrontVector.x * speed;
                    yield return null;
                }
                Stop();

            } while (cond);
        }
        

        {
            Play(End);
            while (GetState.normalizedTime < 1)
            {
                GT();

                entity.BaseVelocity.x = player.FrontVector.x * speed * curve_end.Evaluate(GetState.normalizedTime);

                yield return null;
            }
            Stop();
        }

        entity.BaseVelocity.x = 0;
        entity.BaseAcceleration.x = 0;


    }
}
