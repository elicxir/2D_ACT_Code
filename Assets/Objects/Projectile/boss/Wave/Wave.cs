using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
public class Wave : Projectile
{
    [SerializeField] SpriteObjectData data;
    [SerializeField] Transform marker
;
    protected override void UpdateFunction()
    {
        GenerateSmoke();
    }



    void GenerateSmoke()
    {
        //SpriteObject so= GM.OBJ.SpriteObjectManager.GenerateByData(data, marker.position);
        //so.BaseVelocity = new Vector2(Random.Range(-80, 0)+this.Velocity.x, Random.Range(40, 80));

    }

    protected override void OnSpriteAnimationProgress(int index)
    {
        /*
        if (index == 2)
        {
            GenerateWave();
        }
        else if(index == 8)
        {
            DeactivateCollider();
            Deactivate();
        }*/
    }
}
