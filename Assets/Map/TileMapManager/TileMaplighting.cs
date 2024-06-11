using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapLighting : MonoBehaviour
{
    public Vector2 pos
    {
        get
        {
            return transform.position;
        }
    }

    public UnityEngine.Rendering.Universal.Light2D light2D;

    public virtual void Updater(float time)
    {

    }
}
