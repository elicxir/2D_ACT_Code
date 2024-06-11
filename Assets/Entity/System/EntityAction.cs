using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using UnityEditor;

public class EntityAction : MonoBehaviour
{
    public Entity entity;

    AC_set[] ac_Sets
    {
        get
        {
            return EAS.ac_Sets;
        }
    }
    HitBox_set[] hitbox_set {
        get
        {
            return EAS.hitbox_set;
        }
    }

    public EntityActionSetting EAS;

    [ContextMenu("状態のセーブ")]

    void Save()
    {/*
        EAS.ac_Sets = new AC_set[entity.attackColider.Length];
        EAS.hitbox_set = new HitBox_set[entity.attackHitBox.Length];

        //EditorUtility. SetDirty(EAS);
        //AssetDatabase.SaveAssets();

        for (int i = 0; i < entity.attackHitBox.Length; i++)
        {
            //EAS.hitbox_set[i].offset = entity.attackHitBox[i].transform.localPosition;
            //EAS.hitbox_set[i].size = entity.attackHitBox[i].size;
            EAS.hitbox_set[i].HitBoxData = entity.attackHitBox[i].entityHitBoxData;
            EAS.hitbox_set[i].isUse = entity.attackHitBox[i].HitBoxEnable;


        }
        for (int i=0;i< entity.attackColider.Length; i++)
        {
            //EAS.ac_Sets[i].offset = entity.attackColider[i].transform.localPosition;
            //EAS.ac_Sets[i].size = entity.attackColider[i].size;
            EAS.ac_Sets[i].dataSet = entity.attackColider[i].GetDataSet;
            EAS.ac_Sets[i].isUse = entity.attackColider[i].isActive;

        }*/

        //EditorUtility.SetDirty(EAS);
        //AssetDatabase.SaveAssets();
    }



    protected ProjectileManager Projectile
    {
        get
        {
            return GM.Projectile;
        }
    }

    protected SimpleAnimation.State GetState
    {
        get
        {
            return entity.EntityAnimator.GetState(entity.PlayingAnimation);

        }
    }

    protected void Play(string name)
    {
        entity.PlayingAnimation = name;
        entity.EntityAnimator.Play(name);
    }
    protected void Stop()
    {
        entity.PlayingAnimation = string.Empty;
        entity.EntityAnimator.Stop();
    }




    [ContextMenu("setoffset")]
    protected void AC_Test()
    {
        entity.testAC(ac_Sets,true);
        entity.SetHitBox(hitbox_set);
        Direction();
    }



    protected void SettingReset()
    {
        entity.ACReset();
    }


    public string ActionName;

    public AnimationClip animationClip;

    public AnimationClip[] AnimationClips;

    public string GetActionName(int index)
    {
        return ActionName + "_" + index.ToString();
    }



    //右向き時のスプライトの位置設定
    public Vector2Int offset
    {
        get
        {
            return new Vector2Int(Mathf.RoundToInt( EAS.offset.x), Mathf.RoundToInt(EAS.offset.y));
        }
    }




    public virtual IEnumerator Act()
    {
        yield return null;
    }

    public void Direction()
    {
        switch (entity.faceDirection)
        {
            case Entity.FaceDirection.Right:
                entity.SpritePos = offset;
                entity.sprite_renderer.transform.localScale = new Vector3(1, 1, 1);
                break;

            case Entity.FaceDirection.Left:
                entity.SpritePos = Vector2Int.left * offset.x + Vector2Int.up * offset.y;
                entity.sprite_renderer.transform.localScale = new Vector3(-1, 1, 1);
                break;
        }
    }

}
[System.Serializable]
public class AC_set {
    public AC_DataSet dataSet;

    public bool isUse=true;
}

[System.Serializable]
public class HitBox_set
{
    public EntityHitBoxData HitBoxData;

    public bool isUse = true;
}
