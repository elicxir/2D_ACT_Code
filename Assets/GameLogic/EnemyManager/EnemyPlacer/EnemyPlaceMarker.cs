using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
public class EnemyPlaceMarker : OwnTimeMonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public void SetSprite(EnemyPlaceData data,EnemyManager em)
    {
        spriteRenderer.sprite = em.GetSprite(data.placeStatusData.enemyName, data.placeStatusData.VariantIndex);

        if(data.placeStatusData.face== Entity.FaceDirection.Right)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }

    }

}
